// src/views/ExternalApi.js

import React, { useState } from "react";
import { useAuth0 } from "../react-auth0-spa";

const ExternalApi = () => {
	const [showResult, setShowResult] = useState(false);
	const [apiMessage, setApiMessage] = useState("");
	const { getIdTokenClaims } = useAuth0();

	const callApi = async () => {
		try {
			const token = await getIdTokenClaims();
			console.log(token.__raw);

			const response = await fetch("http://localhost:5010/api/scoreboard/highscores", {
				headers: {
					Authorization: `Bearer ${token.__raw}`
				}
			});

			const responseData = await response.json();

			setShowResult(true);
			setApiMessage(responseData);
		} catch (error) {
			console.error(error);
		}
	};

	return (
		<>
			<h1>Scoreboard</h1>
			<button onClick={callApi}>Ping API</button>
			{showResult && <code>{JSON.stringify(apiMessage, null, 2)}</code>}
		</>
	);
};

export default ExternalApi;