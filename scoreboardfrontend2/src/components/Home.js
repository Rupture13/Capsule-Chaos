import React from "react";
import { useAuth0 } from "../react-auth0-spa";
import { Link } from "react-router-dom";
import { Button, Divider, Col, Row } from "antd";
import "antd/dist/antd.css";
import { BarChartOutlined, UserOutlined } from '@ant-design/icons';

const Home = () => {
	const { isAuthenticated, loginWithRedirect, user } = useAuth0();

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
					<h1 style={{ fontSize: '48px', color: '#2191C9' }}>Welcome, {user.name}!</h1>
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