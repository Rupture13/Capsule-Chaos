using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ScoreboardService.Models;

namespace ScoreboardService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HighscoresController : ControllerBase
    {
        private const string _username = "guest";
        private const string _password = "guest";
        private const string _hostname = "localhost";
        private const string _queueName = "ScoreboardQueue";

        private readonly ScoreboardContext _context;
        
        private ConnectionFactory factory;
        private IConnection conn;
        private IModel channel;

        public HighscoresController(ScoreboardContext context)
        {
            _context = context;

            factory = new ConnectionFactory { 
                UserName = _username,
                Password = _password,
                HostName = _hostname };
            conn = factory.CreateConnection();
            channel = conn.CreateModel();

            channel.BasicQos(0, 1, false);

            channel.QueueDeclare(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                Console.WriteLine($"[MessageQueue] Received message '{message}'");

                var messageParts = message.Split(":");

                var result = 0;
                if (messageParts[0] == "UPDATE")
                {
                    result = await UpdateRecordsOfPlayerId(Int32.Parse(messageParts[1]), messageParts[2]);
                    Console.WriteLine($"[MessageQueue] Processed message '{message}' and updated {result} records.");
                }
                else if (messageParts[0] == "DELETE")
                {
                    result = await DeleteRecordsOfPlayerId(Int32.Parse(messageParts[1]));
                    Console.WriteLine($"[MessageQueue] Processed message '{message}' and deleted {result} records.");
                }

                channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }

        //Destructor
        ~HighscoresController()
        {
            //Connection and Channels are meant to be long-lived
            //So we don't open and close them for each operation

            channel.Close();
            conn.Close();
        }

        // GET: api/Highscores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Highscore>>> GetHighscores()
        {
            return await _context.Highscores.ToListAsync();
        }

        // GET: api/Highscores/Level/1
        [HttpGet("Level/{levelId}")]
        public async Task<ActionResult<IEnumerable<Highscore>>> GetHighscoresOfLevel(int levelId)
        {
            return await _context.Highscores.Where(highscore => highscore.LevelId == levelId).ToListAsync();
        }

        // POST: api/Highscores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Highscore>> PostHighscore(Highscore highscore)
        {
            var oldHighScore = GetPlayerHighScoreForLevel(highscore.PlayerId, highscore.LevelId);

            if (oldHighScore == null)
            {
                //There is no previous highscore for this player in this level, creating one

                _context.Highscores.Add(highscore);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetHighscoresOfLevel", new { id = highscore.Id, levelId = highscore.LevelId }, highscore);       //201
            }
            else if (IsHigherScore(highscore, oldHighScore))
            {
                //There is a previous highscore, new highscore is higher and will overwrite old highscore
                oldHighScore.CollectedScore = highscore.CollectedScore;
                oldHighScore.FinishedTime = highscore.FinishedTime;
                oldHighScore.CalculatedTotal = highscore.CalculatedTotal;

                _context.Entry(oldHighScore).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HighscoreExists(oldHighScore.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok();            //200
            }
            else
            {
                //There is a previous highscore, new highscore is lower and nothing will happen

                return NoContent();     //204
            }
        }

        // DELETE: api/Highscores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Highscore>> DeleteHighscore(int id)
        {
            var highscore = await _context.Highscores.FindAsync(id);
            if (highscore == null)
            {
                return NotFound();
            }

            _context.Highscores.Remove(highscore);
            await _context.SaveChangesAsync();

            return highscore;
        }

        private bool HighscoreExists(int id)
        {
            return _context.Highscores.Any(e => e.Id == id);
        }

        private Highscore GetPlayerHighScoreForLevel(int playerId, int levelId)
        {
            return _context.Highscores
                .Where(highscore => highscore.PlayerId == playerId && highscore.LevelId == levelId)
                .FirstOrDefault();
        }

        private bool IsHigherScore(Highscore newScore, Highscore oldScore)
        {
            return newScore.FinishedTime < oldScore.FinishedTime;
        }

        private async Task<int> DeleteRecordsOfPlayerId(int playerId)
        {
            var deletedAmount = 0;

            var optionsBuilder = new DbContextOptionsBuilder<ScoreboardContext>();
            optionsBuilder.UseInMemoryDatabase("HighscoreList");
            using (var tempContext = new ScoreboardContext(optionsBuilder.Options))
            {
                var playerHighScores = await tempContext.Highscores.Where(h => h.PlayerId == playerId).ToListAsync();

                deletedAmount = playerHighScores.Count;

                if (deletedAmount == 0) { return 0; }

                tempContext.Highscores.RemoveRange(playerHighScores);
                await tempContext.SaveChangesAsync();
            }

            return deletedAmount;
        }

        private async Task<int> UpdateRecordsOfPlayerId(int playerId, string newUsername)
        {
            var updatedRecords = 0;

            var optionsBuilder = new DbContextOptionsBuilder<ScoreboardContext>();
            optionsBuilder.UseInMemoryDatabase("HighscoreList");
            using (var tempContext = new ScoreboardContext(optionsBuilder.Options))
            {
                var playerHighScores = await tempContext.Highscores
                    .Where(h => h.PlayerId == playerId)
                    .ToListAsync();

                updatedRecords = playerHighScores.Count;

                if (updatedRecords == 0) { return 0; }

                foreach (var highScore in playerHighScores)
                {
                    highScore.Playername = newUsername;
                    tempContext.Entry(highScore).State = EntityState.Modified;
                }

                await tempContext.SaveChangesAsync();
            }

            return updatedRecords;
        }
    }
}
