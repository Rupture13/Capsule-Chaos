﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerPerformanceService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PlayerPerformanceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerPerformancesController : ControllerBase
    {
        private const string _username = "guest";
        private const string _password = "guest";
        private const string _hostname = "localhost";
        private const string _queueName = "GhostQueue";

        private readonly PlayerPerformanceContext _context;

        private ConnectionFactory factory;
        private IConnection conn;
        private IModel channel;

        public PlayerPerformancesController(PlayerPerformanceContext context)
        {
            _context = context;

            factory = new ConnectionFactory
            {
                UserName = _username,
                Password = _password,
                HostName = _hostname
            };
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
                if (messageParts[0] == "DELETE")
                {
                    result = await DeleteRecordsOfPlayerId(Int32.Parse(messageParts[1]));
                    Console.WriteLine($"[MessageQueue] Processed message '{message}' and deleted {result} records.");
                }
                else if (messageParts[0] == "UPDATE")
                {
                    Console.WriteLine($"[MessageQueue] Message is not relevant for this service.");
                }
                
                channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }

        //Destructor
        ~PlayerPerformancesController()
        {
            //Connection and Channels are meant to be long-lived
            //So we don't open and close them for each operation

            channel.Close();
            conn.Close();
        }

        // GET: api/PlayerPerformances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerPerformance>>> GetPlayerPerformances()
        {
            return await _context.PlayerPerformances
                .Include(p => p.Snapshots)
                .ToListAsync();
        }

        // GET: api/PlayerPerformances/Level/1/Player/2
        [HttpGet("Level/{levelId}/Player{playerId}")]
        public async Task<ActionResult<PlayerPerformance>> GetPlayerPerformance(int levelId, int playerId)
        {
            var playerPerformance = await _context.PlayerPerformances
                                                    .Include(p => p.Snapshots)
                                                    .Where(p => (p.LevelId == levelId && p.PlayerId == playerId))
                                                    .FirstOrDefaultAsync();

            if (playerPerformance == null)
            {
                return NotFound();
            }

            return playerPerformance;
        }

        // POST: api/PlayerPerformances
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlayerPerformance>> PostPlayerPerformance(PlayerPerformance playerPerformance)
        {
            var oldPerformance = GetPlayerPerformanceFromLevel(playerPerformance.PlayerId, playerPerformance.LevelId);

            if (oldPerformance == null)
            {
                //There is no previous performance for this player in this level, creating one

                _context.PlayerPerformances.Add(playerPerformance);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPlayerPerformance", new { levelId = playerPerformance.LevelId, playerId = playerPerformance.PlayerId }, playerPerformance);       //201
            }
            else
            {
                //There is a previous performance, new performance will overwrite old performance
                oldPerformance.Snapshots = playerPerformance.Snapshots;

                _context.Entry(oldPerformance).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerPerformanceExists(oldPerformance.Id))
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
        }

        // DELETE: api/PlayerPerformances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlayerPerformance>> DeletePlayerPerformance(int id)
        {
            var playerPerformance = await _context.PlayerPerformances.FindAsync(id);
            if (playerPerformance == null)
            {
                return NotFound();
            }

            _context.PlayerPerformances.Remove(playerPerformance);
            await _context.SaveChangesAsync();

            return playerPerformance;
        }

        private bool PlayerPerformanceExists(int id)
        {
            return _context.PlayerPerformances.Any(e => e.Id == id);
        }

        private PlayerPerformance GetPlayerPerformanceFromLevel(int playerId, int levelId)
        {
            return _context.PlayerPerformances
                .Where(performance => performance.PlayerId == playerId && performance.LevelId == levelId)
                .FirstOrDefault();
        }

        private async Task<int> DeleteRecordsOfPlayerId(int playerId)
        {
            var deletedAmount = 0;

            var optionsBuilder = new DbContextOptionsBuilder<PlayerPerformanceContext>();
            optionsBuilder.UseInMemoryDatabase("PlayerPerformanceList");
            using (var tempContext = new PlayerPerformanceContext(optionsBuilder.Options))
            {
                var playerPerformances = await tempContext.PlayerPerformances
                    .Where(p => p.PlayerId == playerId)
                    .Include(p => p.Snapshots)
                    .ToListAsync();

                deletedAmount = playerPerformances.Count;

                if (deletedAmount == 0) { return 0; }

                foreach (var performance in playerPerformances)
                {
                    tempContext.PerformanceSnapshots.RemoveRange(performance.Snapshots);
                    performance.Snapshots = null;
                }

                tempContext.PlayerPerformances.RemoveRange(playerPerformances);
                await tempContext.SaveChangesAsync();
            }

            return deletedAmount;
        }
    }
}
