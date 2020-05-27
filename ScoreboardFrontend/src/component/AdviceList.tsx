// import React, { Component, CSSProperties } from "react";
// import { Advice, User } from "../model/Highscore";
// import Container from "./template/Container";
// import Utils from "../Utils";
// import { List, Button, Modal, Typography, Input, Select, Avatar, Row, Col } from "antd";
// import ApiCaller from "../api/ApiCaller";

// const { Search, Group } = Input;
// const { Title, Paragraph, Text } = Typography;

// class AdviceList extends Component<{ studentAdvices: Advice[] }>{
// 	state = {
// 		data: [],
// 		userData: [],
// 		permissionModalSelection: 'Both',
// 		permissionModalQuery: "",
// 		permissionModalVisibility: false,
// 		permissionAdviceId: -1,
// 		adviceModalContents: [],
// 		adviceDetailsVisibility: false,
// 		adviceDetailsId: -1
// 	}

// 	private canvasAPI = new ApiCaller("44300");

// 	private fullyLoaded = false;
// 	private totalDataLength = this.props.studentAdvices.length;
// 	private retrieveCount = 3;
// 	private index = 0;

// 	getMoreData(): Advice[] {
// 		if (this.fullyLoaded) {
// 			return Utils.GetArrayCopy(this.props.studentAdvices).reverse();
// 		}
// 		else {
// 			if ((this.index + this.retrieveCount) >= this.totalDataLength) {
// 				this.fullyLoaded = true;
// 				this.index = this.totalDataLength;
// 			}
// 			else {
// 				this.index += this.retrieveCount;
// 			}

// 			return Utils.GetArrayCopy(this.props.studentAdvices).reverse().slice(0, this.index);
// 		}
// 	}

// 	componentDidMount() {
// 		this.setState({ data: this.getMoreData() });
// 		window.dispatchEvent(new Event('resize'));
// 	}

// 	onLoadMore = () => {
// 		this.setState({ data: this.getMoreData() });
// 		window.dispatchEvent(new Event('resize'));
// 	};

// 	async openPermissionModal(adviceId: number): Promise<void> {
// 		let component = this;

// 		this.canvasAPI.get('users').then(function (response) {
// 			component.setState({
// 				userData: (response.data as User[]),
// 				adviceDetailsVisibility: false,
// 				permissionModalVisibility: true,
// 				permissionAdviceId: adviceId
// 			});
// 		});
// 	};

// 	async searchForUsers(name: string, role: string): Promise<void> {
// 		let component = this;

// 		let nameQuery = name.length > 0 ? `/${name}` : "";
// 		let roleQuery = (role.length > 0 && role != "Both") ? `?role=${role}` : "";
// 		let totalQuery = `users${nameQuery}${roleQuery}`;

// 		this.canvasAPI.get(totalQuery).then(function (response) {
// 			component.setState({
// 				permissionModalQuery: name,
// 				userData: (response.data as User[])
// 			});
// 		});
// 	}

// 	checkUserPermission(userId, adviceId): boolean {
// 		let currentAdvice = this.state.data[adviceId] as Advice;
// 		let result = false;

// 		currentAdvice.viewPermissions.forEach(viewPermission => {
// 			if (viewPermission.userId == userId) {
// 				result = true;
// 			}
// 		});

// 		return result;
// 	}

// 	addUserPermission(userId: number, adviceId: number, name: string, role: string) {
// 		//TODO: API call
// 		alert(`Viewing Permission given to user ${userId}`);

// 		//Refresh list
// 		this.searchForUsers(name, role);
// 	}

// 	revokeUserPermission(userId: number, adviceId: number, name: string, role: string) {
// 		//TODO: API call
// 		alert(`Viewing Permission revoked from user ${userId}`);

// 		//Refresh list
// 		this.searchForUsers(name, role);
// 	}

// 	openDetailsModal(adviceId: number) {
// 		let newContents = [];
// 		let currentAdvice = this.state.data[adviceId];
// 		newContents.push([
// 			<Text code>{Utils.FormatDate(currentAdvice.date)}</Text>,
// 			<Title level={4} style={{ color: currentAdvice.adviceText ? '#0065D8' : '#e0a0a0' }}>
// 				{currentAdvice.adviceText ? currentAdvice.adviceText : "<Dit advies heeft geen beschrijving>"}
// 			</Title>,
// 			<br />
// 		]);
// 		currentAdvice.contents.forEach(categoryAdvice => {
// 			newContents.push([
// 				<Text strong>• {categoryAdvice.categoryLabel}: {categoryAdvice.srlScore}</Text>,
// 				<Paragraph>({categoryAdvice.description})</Paragraph>,
// 				<br />
// 			]);
// 		});
// 		newContents.push(<Button size='large' type='ghost' style={{ color: '#5CA53A', fontWeight: 600 }} onClick={() => { this.openPermissionModal(adviceId) }}>Beheer toegang</Button>);


// 		this.setState({
// 			adviceModalContents: newContents,
// 			adviceDetailsVisibility: true,
// 			adviceDetailsId: adviceId
// 		});
// 	};

// 	closeModal = e => {
// 		this.setState({
// 			permissionModalVisibility: false,
// 			adviceDetailsVisibility: false,
// 		});
// 	};

// 	render() {
// 		const {
// 			data,
// 			userData,
// 			permissionModalSelection,
// 			permissionModalQuery,
// 			permissionModalVisibility,
// 			permissionAdviceId,
// 			adviceModalContents,
// 			adviceDetailsVisibility,
// 			adviceDetailsId
// 		} = this.state;

// 		const loadMore =
// 			!this.fullyLoaded ? (
// 				<div
// 					style={{
// 						textAlign: 'center',
// 						margin: 12
// 					}}
// 				>
// 					<Button style={{ borderRadius: 10, fontWeight: 600 }} onClick={this.onLoadMore}>Laad oudere adviezen</Button>
// 				</div>
// 			) : null;
// 		return <Container title="Adviezen">
// 			<List
// 				style={{ margin: -5 }}
// 				loadMore={loadMore}
// 				dataSource={data}
// 				renderItem={(item, index) =>
// 					<List.Item
// 						actions={[<Button size='small' type='ghost' style={{ color: '#5CA53A' }} onClick={() => { this.openPermissionModal(index) }}>Beheer toegang</Button>]}
// 						style={{ margin: 1, backgroundColor: (index % 2) ? '#f6f6f8' : null, borderRadius: 10, paddingLeft: 10 }}
// 					>
// 						[<i>{Utils.FormatDate(item.date)}</i>]{" "}
// 						<a onClick={() => { this.openDetailsModal(index) }} style={{ color: 'grey' }}>
// 							<b>{item.adviceText ? item.adviceText : "Geen beschrijving"}</b>
// 						</a>
// 					</List.Item>}
// 			/>
// 			{/* Advice details modal */}
// 			<Modal
// 				title="Details"
// 				visible={adviceDetailsVisibility}
// 				onOk={this.closeModal} onCancel={this.closeModal}
// 				closable={true} keyboard={true} maskClosable={true}
// 				footer={null}
// 			>
// 				{adviceModalContents}
// 			</Modal>

// 			{/* ViewPermission modal */}
// 			<Modal
// 				title="Beheer toegang tot advies"
// 				visible={permissionModalVisibility}
// 				onOk={this.closeModal} onCancel={this.closeModal}
// 				closable={true} keyboard={true} maskClosable={true}
// 				footer={<Button key="submit" type="primary" onClick={this.closeModal}>Klaar</Button>}
// 				width={660}
// 			>
// 				<Group compact>
// 					<Select style={{ width: '24%', fontWeight: 600 }} defaultValue="Both" onChange={value => this.setState({ permissionModalSelection: value })}>
// 						<Select.Option value="Both">Alle gebruikers</Select.Option>
// 						<Select.Option value="Teacher">Docent</Select.Option>
// 						<Select.Option value="Student">Student</Select.Option>
// 					</Select>
// 					<p style={{ width: '1%' }} />
// 					<Search
// 						style={{ width: '75%' }} placeholder="Zoek gebruikers" enterButton allowClear
// 						onSearch={value => this.searchForUsers(value, permissionModalSelection)}
// 					/>
// 				</Group>
// 				<br />
// 				<List
// 					style={{ margin: -5 }}
// 					dataSource={userData as User[]}
// 					renderItem={(user, index) =>
// 						<List.Item
// 							actions={
// 								!this.checkUserPermission(user.id, permissionAdviceId) ?
// 									[<Button size='small' style={{ color: '#008CFF', fontWeight: 600, width: 120 }}
// 										onClick={() => { this.addUserPermission(user.id, permissionAdviceId, permissionModalQuery, permissionModalSelection) }}
// 									>Geef toegang</Button>]
// 									:
// 									[<Button size='small' danger style={{ fontWeight: 600, width: 120 }}
// 										onClick={() => { this.revokeUserPermission(user.id, permissionAdviceId, permissionModalQuery, permissionModalSelection) }}
// 									>Trek toegang in</Button>]
// 							}
// 							style={{ margin: 1, backgroundColor: (index % 2) ? '#f6f6f8' : null, borderRadius: 10, paddingLeft: 10 }}
// 						>
// 							<Row style={{ width: '100%' }} align='middle'>
// 								<Col span={3}><Avatar src={user.pictureUrl} style={{ marginLeft: 5 }} /></Col>
// 								<Col span={17}><Text strong>{user.name}</Text></Col>
// 								<Col span={4}><Text>{user.role}</Text></Col>
// 							</Row>
// 						</List.Item>
// 					}
// 				/>
// 			</Modal >
// 		</Container >
// 	}
// }
// export default AdviceList;