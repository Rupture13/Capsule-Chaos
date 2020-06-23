// src/views/ExternalApi.js

import React, { useState } from "react";
import { useAuth0 } from "../react-auth0-spa";
import { Spin, Row, Col, Table, InputNumber } from 'antd';
import { LoadingOutlined } from '@ant-design/icons';
import Column from "antd/lib/table/Column";
import "../components/Scoreboard.css"
import { Highscore } from "../Models";

const antIcon = <LoadingOutlined style={{ fontSize: 48 }} spin />;

const Scoreboard = () => {
	const [levelId, setLevelId] = useState(1);
	const [showResult, setShowResult] = useState(false);
	const [apiMessage, setApiMessage] = useState();

	const callApi = async (searchLevelId) => {
		try {
			console.log(`Now searching for highscores of level ${searchLevelId}`)

			//Send request with token
			const response = await fetch(`http://localhost:5010/api/scoreboard/highscores/level/${searchLevelId}`, {
			});

			//Fetch data in correct class format
			let responseData = [];
			(await response.json()).forEach(obj => {
				responseData.push(new Highscore(obj));
			});

			//Set state
			setShowResult(true);
			setApiMessage(responseData);
		} catch (error) {
			console.error(error);
		}
	};

	function FormatTime(rawTime) {
		let minutes = Math.floor((rawTime / 100) / 60);
		let seconds = Math.floor((rawTime / 100) % 60);
		let milliseconds = Math.floor((rawTime - Math.floor(rawTime)) * 100);
		if (minutes < 10) { minutes = `0${minutes}`; }
		if (seconds < 10) { seconds = `0${seconds}`; }
		if (milliseconds < 10) { milliseconds = `0${milliseconds}`; }
		return `${minutes}:${seconds}:${milliseconds}`;
	}

	async function onChange(value) {
		console.log('changed', value);
		setShowResult(false);
		setLevelId(value);
		await callApi(value);
	}

	const tableLoading = {
		spinning: !showResult,
		indicator: <Spin size='large' indicator={antIcon} />,
	}

	//Call Scoreboard API on page load
	if (!showResult) {
		callApi(levelId);
	}

	return (
		<Row>
			<Col span={6} />
			<Col span={12} style={{ textAlign: 'center' }}>
				<h1 style={{ fontSize: '48px', color: '#2191C9', margin: 0 }}>Scoreboard</h1><br />

				<p style={{ fontWeight: 'bolder', fontSize: '18px', marginBottom: 0 }}>Select level:</p>
				<InputNumber
					min={1} max={20} defaultValue={levelId} onChange={onChange}
					style={{ fontWeight: 'bold', fontSize: '32px', color: '#2191C9', textAlign: 'center', width: '100px' }}
				/>
				<br /><br />

				{(!showResult || !apiMessage) &&
					<Table
						//columns={columns}
						dataSource={apiMessage}
						pagination={false}
						loading={tableLoading}
					>
						<Column
							title={<div style={{ fontWeight: 'bolder', fontSize: '20px', color: '#f2cd11' }}>Username</div>}
							dataIndex='playername'
							key='playername'
							render={(text) => { return <div style={{ fontWeight: 'bolder', fontSize: '18px' }}>{text}</div> }}
						/>
						<Column
							title={<div style={{ fontWeight: 'bolder', fontSize: '20px', color: '#f2cd11' }}>Username</div>}
							dataIndex='finishedTime'
							key='finishedTime'
							render={(text) => { return <div style={{ fontSize: '18px' }}>{FormatTime(text)}</div> }}
						/>
					</Table>


				}
				{(showResult && apiMessage) &&
					<Table
						//columns={columns}
						dataSource={apiMessage}
						pagination={false}
					//className='table-dark'
					>
						<Column
							title={<div style={{ fontWeight: 'bolder', fontSize: '20px', color: '#f2cd11' }}>Username</div>}
							dataIndex='playername'
							key='playername'
							render={(text) => { return <div style={{ fontWeight: 'bolder', fontSize: '18px' }}>{text}</div> }}
						/>
						<Column
							title={<div style={{ fontWeight: 'bolder', fontSize: '20px', color: '#f2cd11' }}>Username</div>}
							dataIndex='finishedTime'
							key='finishedTime'
							render={(text) => { return <div style={{ fontSize: '18px' }}>{FormatTime(text)}</div> }}
						/>
					</Table>
				}
			</Col>
			<Col span={6} />
		</Row>
	);
};
export default Scoreboard;