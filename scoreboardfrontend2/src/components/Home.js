import React, { useState } from "react";
import { useAuth0 } from "../react-auth0-spa";
import { Link } from "react-router-dom";
import { Button, Divider, Col, Row, Spin } from "antd";
import "antd/dist/antd.css";
import { BarChartOutlined, UserOutlined, LoadingOutlined } from '@ant-design/icons';
import { CapsuleChaosUser } from "../Models";

const antIcon = <LoadingOutlined style={{ fontSize: 48 }} spin />;

const Home = () => {
	const { isAuthenticated, loginWithRedirect, loading, user } = useAuth0();
	const [actualLoading, setActualLoading] = useState(true);
	const [actualUser, setActualUser] = useState(new CapsuleChaosUser());

	const callGetUserApi = async () => {
		if (loading || !actualLoading) {
			return;
		}

		try {
			//Get token
			//const token = await getIdTokenClaims();

			//Send request with token
			let response;
			response = await fetch(`http://localhost:5010/api/accounting/accounts/Find/${user.email}`, {
				method: 'GET'
			});

			setActualUser(new CapsuleChaosUser(await response.json()));
			setActualLoading(false);

		} catch (error) {
			console.error(error);
		}
	};

	if (isAuthenticated && actualLoading) {
		callGetUserApi();
		return <div style={{ textAlign: 'center', fontSize: '24px', fontWeight: 'bolder' }}>
			<Spin size='large' indicator={antIcon} tip="Loading user data..." style={{ marginTop: '100px' }} />
		</div>;
	}

	return (
		<div>
			{!isAuthenticated && (
				<div style={{ textAlign: 'center' }}>
					<br /><br />
					<h1 style={{ fontSize: '48px', color: '#2191C9' }}>Welcome to Capsule Chaos!</h1>
					<h2 style={{ fontSize: '36px', color: '#A6ACB5' }}>Please log in</h2>
					<br /><br /><br /><br /><br />

					<Button
						type='primary' style={{ width: '15vw', height: '9vh', fontSize: '32px', fontWeight: 'bolder' }}
						onClick={() => loginWithRedirect({})}
					>
						Log in
					</Button>
				</div>
			)}

			{isAuthenticated && (
				<div style={{ textAlign: 'center' }}>
					<br /><br />
					<h1 style={{ fontSize: '48px', color: '#2191C9' }}>Welcome, {actualUser.username}!</h1>
					<br /><br /><br /><br /><br /><br />
					<Row>
						<Col span={5} />
						<Col span={5}>
							<BarChartOutlined style={{ fontSize: '100px', color: '#9399A0' }} /><br />
							<Link style={{ fontSize: '28px', fontWeight: 'bold' }} to="/scoreboard">Go to Scoreboard</Link>
						</Col>
						<Col span={4}><Divider type='vertical' dashed style={{ borderLeftColor: '#b1b1b1', height: '120px' }} /></Col>
						<Col span={5}>
							<UserOutlined style={{ fontSize: '100px', color: '#9399A0' }} /><br />
							<Link style={{ fontSize: '28px', fontWeight: 'bold' }} to="/profile">Go to Profile</Link>
						</Col>
						<Col span={5} />
					</Row>
				</div>
			)
			}
		</div >
	);
};

export default Home;