export class CapsuleChaosUser {
	constructor(obj) {
		obj && Object.assign(this, obj);
	}

	accountId;
	email;
	username;
	password;
}

export class Highscore {
	constructor(obj) {
		obj && Object.assign(this, obj);
	}

	calculatedTotal;
	collectedScore;
	finishedTime;
	id;
	levelId;
	playerId;
	playername;
}