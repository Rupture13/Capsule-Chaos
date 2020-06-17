// src/components/Profile.js

import React from "react";
import { useAuth0 } from "../react-auth0-spa";
import "antd/dist/antd.css";
import { Divider, Row, Col } from "antd";
import { Form, Input, Button, Modal } from 'antd';
import { UserOutlined, WarningFilled } from '@ant-design/icons';

const Profile = () => {
	const { loading, user, getIdTokenClaims } = useAuth0();

	const callUpdateApi = async (newUsername) => {
		try {
			//Get token
			const token = await getIdTokenClaims();

			console.log('dubmbm ' + newUsername);
			console.log(user.accountId);
			user.accountId = 1;

			//Send request with token
			const response = await fetch(`http://localhost:5010/api/accountingplus/accounts/${user.accountId}`, {
				method: 'PUT',
				headers: { Authorization: `Bearer ${token.__raw}`, 'Content-Type': 'application/json' },
				body: JSON.stringify({ accountId: 1, email: user.email, username: newUsername })
			});

			let responseData = await response.status;
			if (responseData === 204) {
				alert(`Username successfully changed to ${newUsername}.`);
			}
			else {
				alert("Request could not be completed at this time. Please try again later.");
			}
		} catch (error) {
			console.error(error);
		}
	};

	if (loading || !user) {
		return <div>Loading...</div>;
	}

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
			content: 'This action cannot be reversed.',
			onOk() {
				console.log('OK');
			},
			onCancel() {
				console.log('Cancel');
			}
		});
	}

	return (
		<Row>
			<Col span={8} />
			<Col span={8} style={{ textAlign: 'center' }}>
				<h1 style={{ fontSize: '48px', color: '#2191C9', margin: 0 }}>Profile</h1>
				<UserOutlined style={{ fontSize: '100px', color: '#9399A0' }} /><br />
				<p style={{ fontWeight: 'bolder', fontSize: '18px', marginBottom: '-10px' }}>Username:</p>
				<h2 style={{ fontSize: '24px', color: '#A6ACB5' }}>{user.name}</h2>
				<p style={{ fontWeight: 'bolder', fontSize: '18px', marginBottom: '-10px' }}>Email:</p>
				<h2 style={{ fontSize: '24px', color: '#A6ACB5' }}>{user.email}</h2>

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
						<Input placeholder={user.name} />
					</Form.Item>

					<Form.Item>
						<Button type="primary" htmlType="submit" style={{ backgroundColor: '#FFA203', borderColor: '#B5AB00' }} >
							Change username
        			</Button>
					</Form.Item>
				</Form>
				<Divider style={{ borderTopColor: '#b5b5b5' }} />
				<br />
				<h3 style={{ fontSize: '24px', color: 'red', margin: 0, marginTop: '-10px' }}>Delete your account</h3>
				<br />
				<Button
					danger style={{ width: '200px', height: '36px', fontSize: '16px', backgroundColor: 'rgba(255,215,215,0.5)', fontWeight: 'bolder' }}
					onClick={warning}
				>Delete account</Button>
			</Col>
			<Col span={8} />
		</Row>
	);
};

export default Profile;