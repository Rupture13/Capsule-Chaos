import React, { Component } from "react"
import App from "../src/component/App";
import { Row, Col, Button } from 'antd';
import { Highscore } from "../src/model/Highscore";
import ApiCaller from "../src/api/ApiCaller";

class Home extends Component {
	state = {
		highScores: []
	};


	render() {
		let newContents = [];
		let highScores = this.state.highScores as Highscore[]
		highScores.forEach(highScore => {
			newContents.push(<p>{highScore.playername} completed level {highScore.levelId} in {highScore.calculatedTotal}.</p>);
			newContents.push(<br />);
		});

		return <App>
			<h1>Scoreboard - Capsule Chaos</h1><br />
			{newContents}
		</App>
	}

	async componentDidMount(): Promise<void> {
		let component = this;
		let adviceAPI = new ApiCaller("5003");

		adviceAPI.get('highscores').then(function (response) {
			component.setState({ highScores: response.data });
		});
	}


}
export default Home;