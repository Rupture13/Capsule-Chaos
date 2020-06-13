// src/App.js

import React from "react";
import NavBar from "./components/NavBar";
import { useAuth0 } from "./react-auth0-spa";
import { Router, Route, Switch } from "react-router-dom";
import Profile from "./components/Profile";
import history from "./utils/history";
import ExternalApi from "./utils/ExternalApi";
import PrivateRoute from "./components/PrivateRoute";

function App() {
	const { loading } = useAuth0();

	if (loading) {
		return <div>Loading...</div>;
	}

	return (
		<div className="App">
			{/* Don't forget to include the history module */}
			<Router history={history}>
				<header>
					<NavBar />
				</header>
				<Switch>
					<Route path="/" exact />
					<PrivateRoute path="/profile" component={Profile} />
					<PrivateRoute path="/scoreboard" component={ExternalApi} />
				</Switch>
			</Router>
		</div>
	);
}

export default App;