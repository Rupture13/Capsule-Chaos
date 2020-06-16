// src/components/NavBar.js

import React from "react";
import { useAuth0 } from "../react-auth0-spa";
import { Link } from "react-router-dom";
import { Menu, Button } from "antd";
import "antd/dist/antd.css";

const NavBar = () => {
	const { isAuthenticated, loginWithRedirect, logout } = useAuth0();

	return (
		<div>
			{!isAuthenticated && (
				<Button
					onClick={() => loginWithRedirect({})} type='primary'
					style={{ fontWeight: 'bolder', float: 'right', transform: 'translateY(50%)' }}
				>
					Log in
				</Button>
			)}

			{isAuthenticated && (
				<>

					<Menu
						theme='dark'
						mode="horizontal"
						defaultSelectedKeys={['1']}
						style={{ backgroundColor: '#A6ACB5' }}
					>
						<Menu.Item key="1"><Link style={{ fontWeight: 'bold' }} to="/">Home</Link></Menu.Item>
						<Menu.Item key="2"><Link style={{ fontWeight: 'bold' }} to="/scoreboard">Scoreboard</Link></Menu.Item>
						<Menu.Item key="3"><Link style={{ fontWeight: 'bold' }} to="/profile">Profile</Link></Menu.Item>
						<Button
							style={{ backgroundColor: 'rgba(255,230,230,0.5)', color: 'red', fontWeight: 'bolder', float: 'right', transform: 'translateY(50%)' }}
							danger onClick={() => logout()}
						>
							Log out
						</Button>
					</Menu>

				</>
			)}
		</div>
	);
};

export default NavBar;