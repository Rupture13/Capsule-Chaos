// src/App.js

import React from "react";
import NavBar from "./components/NavBar";
import { useAuth0 } from "./react-auth0-spa";
import { Router, Route, Switch } from "react-router-dom";
import Profile from "./components/Profile";
import history from "./utils/history";
import Scoreboard from "./components/Scoreboard";
import PrivateRoute from "./components/PrivateRoute";
import { Layout } from 'antd';
import "antd/dist/antd.css";
import logo from "./Logo.png";
import background from "./Background.jpg";
import Home from "./components/Home";

const { Header, Content } = Layout;

function App() {
	const { loading } = useAuth0();

	if (loading) {
		return <div>Loading...</div>;
	}

	return (
		<div className="App">
			{/* Don't forget to include the history module */}
			<Router history={history}>
				<Layout>
					<Header style={{ backgroundColor: '#2191C9', textAlign: 'center', height: '10vh' }}>
						<img src={logo} alt='Logo' style={{ height: '90%', marginTop: '0.25%' }} />
					</Header>
					<Layout style={{ height: '90vh' }}>
						<Header style={{ backgroundColor: '#A6ACB5' }}>
							<NavBar />
						</Header>
						<Content style={{ padding: '0 50px', backgroundImage: `url(${background})`, backgroundSize: 'cover' }}>
							<div style={{ background: 'rgba(255,255,255,0.8)', padding: '24px', height: '83vh' }}>
								<Switch>
									<Route path="/" exact component={Home} />
									<PrivateRoute path="/scoreboard" component={Scoreboard} />
									<PrivateRoute path="/profile" component={Profile} />
								</Switch>
							</div>
						</Content>
					</Layout>
				</Layout>
			</Router>
		</div>
	);
}

export default App;