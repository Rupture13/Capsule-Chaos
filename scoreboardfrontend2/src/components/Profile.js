// src/components/Profile.js

import React from "react";
import { useAuth0 } from "../react-auth0-spa";
import "antd/dist/antd.css";
import { Divider, Row, Col } from "antd";
import { Form, Input, Button, Modal } from 'antd';
import { UserOutlined, WarningFilled } from '@ant-design/icons';

const Profile = () => {
	const { loading, user } = useAuth0();

	if (loading || !user) {
		return <div>Loading...</div>;
	}

	const onFinish = values => {
		console.log('Success:', values);
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
					onFinish={onFinish}
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