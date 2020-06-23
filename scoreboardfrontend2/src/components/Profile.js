// src/components/Profile.js

import React, { useState } from "react";
import { useAuth0 } from "../react-auth0-spa";
import "antd/dist/antd.css";
import { Divider, Row, Col, Form, Input, Button, Modal, Spin, Tooltip, Typography } from 'antd';
import { LoadingOutlined } from '@ant-design/icons';
import { UserOutlined, WarningFilled } from '@ant-design/icons';
import { CapsuleChaosUser, EmailProvider } from "../Models";

const antIcon = <LoadingOutlined style={{ fontSize: 48 }} spin />;
const { Text } = Typography;

const Profile = () => {
	const [actualLoading, setActualLoading] = useState(true);
	const [actualUser, setActualUser] = useState(new CapsuleChaosUser());

	const callGetUserApi = async () => {
		if (!actualLoading) {
			return;
		}

		try {
			//Get email
			let emailprov = new EmailProvider();

			//Send request with token
			let response;
			response = await fetch(`http://localhost:5010/api/accounting/accounts/Find/${emailprov.email}`, {
				method: 'GET'
			});

			setActualUser(new CapsuleChaosUser(await response.json()));
			setActualLoading(false);

		} catch (error) {
			console.error(error);
		}
	};

	const callUpdateApi = async (newUsername) => {

		try {

			console.log(`[API] Updating username '${actualUser.username}' to '${newUsername}'.`);

			//Send request with token
			const response = await fetch(`http://localhost:5010/api/accounting/accounts/${actualUser.accountId}`, {
				method: 'PUT',
				headers: { 'Content-Type': 'application/json', 'Access-Control-Allow-Origin': 'http://localhost:3000', 'Access-Control-Allow-Credentials': 'true' },
				body: JSON.stringify({ accountId: actualUser.accountId, email: actualUser.email, username: newUsername })
			});

			let responseData = await response.status;
			if (responseData === 204) {
				alert(`Username successfully changed to ${newUsername}.`);
				setActualLoading(true);
			}
			else {
				alert("Request could not be completed at this time. Please try again later.");
			}
		} catch (error) {
			console.error(error);
		}
	};

	const callDeleteApi = async () => {
		try {

			console.log(`[API] Deleting user with ID ${actualUser.accountId}`);

			//Send request with token
			const response = await fetch(`http://localhost:5010/api/accounting/accounts/${actualUser.accountId}`, {
				method: 'DELETE'
			});

			let responseData = await response.status;
			if (responseData === 200) {
				alert(`User account successfully deleted.`);
				window.location.href = 'https://www.google.com';
			}
			else {
				alert("Request could not be completed at this time. Please try again later.");
			}
		} catch (error) {
			console.error(error);
		}
	};

	if (actualLoading) {
		callGetUserApi();
		return <div style={{ textAlign: 'center', fontSize: '24px', fontWeight: 'bolder' }}>
			<Spin size='large' indicator={antIcon} tip="Loading user data..." style={{ marginTop: '100px' }} />
		</div>;
	} //else if (actualUser.accountId != undefined) { console.log(actualUser.username) }

	const onFinish = newUsername => {
		console.log('Success:', newUsername);
		callUpdateApi(newUsername);
	};

	const onFinishFailed = errorInfo => {
		console.log('Failed:', errorInfo);
	};

	function warning() {
		Modal.confirm({
			icon: <WarningFilled />,
			title: 'Are you sure you want to delete your account?',
			content: 'This action cannot be reversed. All your scores will forever be lost.',
			onOk() {
				callDeleteApi();
			},
			onCancel() {
				console.log('Cancel');
			}
		});
	}

	function infoPersonalData() {
		Modal.info({
			title: '------------------------------',
			content: (
				<div>
					<h3 style={{ userSelect: 'none' }}>Statement on storing personal data</h3>
					<p style={{ userSelect: 'none' }}>In our system, we only save your username, email and password.</p>
					<p style={{ userSelect: 'none' }}>No other personal data is collected or stored.</p>
					<br />
					<h3 style={{ userSelect: 'none' }}>Your personal data</h3>
					<h6 style={{ userSelect: 'none' }}>Username</h6>
					<p style={{ userSelect: 'none' }}>{actualUser.username}</p>
					<h6 style={{ userSelect: 'none' }}>Email</h6>
					<p style={{ userSelect: 'none' }}>{actualUser.email}</p>
					<h6 style={{ userSelect: 'none' }}>Password</h6>
					<Tooltip title={actualUser.password} placement="bottom" color='green' key='green'>
						<Text keyboard style={{ userSelect: 'none' }}>Hover here to view your saved password</Text>
					</Tooltip>
				</div>
			),
			onOk() { },
		});
	}

	return (
		<Row>
			<Col span={8} />
			<Col span={8} style={{ textAlign: 'center' }}>
				<h1 style={{ fontSize: '48px', color: '#2191C9', margin: 0 }}>Profile</h1>
				<UserOutlined style={{ fontSize: '100px', color: '#9399A0' }} /><br />
				<p style={{ fontWeight: 'bolder', fontSize: '18px', color: '#A6ACB5', marginBottom: '-10px' }}>Username:</p>
				<h2 style={{ fontSize: '24px', color: '#000000a6' }}>{actualUser.username}</h2>
				<p style={{ fontWeight: 'bolder', fontSize: '18px', color: '#A6ACB5', marginBottom: '-10px' }}>Email:</p>
				<h2 style={{ fontSize: '24px', color: '#000000a6' }}>{actualUser.email}</h2>

				<Divider style={{ borderTopColor: '#b5b5b5' }} />

				<h3 style={{ fontSize: '24px', color: '#FFA203', margin: 0 }}>Change your username</h3>
				<Form
					name="basic"
					initialValues={{ remember: true }}
					onFinish={(values) => { onFinish(values.username) }}
					onFinishFailed={onFinishFailed}
					layout='vertical'
				>
					<Form.Item
						label="New username"
						style={{ fontWeight: 'bolder' }}
						name="username"
						rules={[{ required: true, message: 'Please input your new username!' }]}
					>
						<Input placeholder={actualUser.username} />
					</Form.Item>

					<Form.Item>
						<Button type="primary" htmlType="submit" style={{ backgroundColor: '#FFA203', borderColor: '#B5AB00' }} >
							Change username
        			</Button>
					</Form.Item>
				</Form>
				<Divider style={{ borderTopColor: '#b5b5b5' }} />
				<br />
				<h3 style={{ fontSize: '24px', color: 'red', margin: 0, marginTop: '-10px' }}>Personal data</h3>
				<br />
				<Tooltip title="We handle your personal data with care. Click here to see what personal data we save from you into our system.">
					<Button
						style={{ width: '250px', height: '36px', fontSize: '14px', backgroundColor: 'rgba(255,255,255,0.33)', fontWeight: 'bolder' }}
						onClick={infoPersonalData}
					>View your saved personal data</Button>
				</Tooltip>
				<br /><br />
				<Tooltip title="If you wish to delete your personal data, you can do so here. This will delete your personal data and all records referencing your account.">
					<Button
						danger style={{ width: '250px', height: '36px', fontSize: '16px', backgroundColor: 'rgba(255,215,215,0.5)', fontWeight: 'bolder' }}
						onClick={warning}
					>Delete account</Button>
				</Tooltip>
			</Col>
			<Col span={8} />
		</Row>
	);
};

export default Profile;