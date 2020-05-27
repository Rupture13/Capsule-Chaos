export interface Highscore {
	id: number;
	playerId: number;
	playername: string;
	levelId: number;
	collectedScore: number;
	finishedTime: number;
	calculatedTotal: number;
}