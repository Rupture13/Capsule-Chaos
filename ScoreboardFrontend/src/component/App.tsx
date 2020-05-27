import React,{Component} from "react";
import { Row, Col } from 'antd';
class App  extends Component{
    render(){
    return <Row style={{marginTop:24}}>
        <Col span={1} />
        <Col span={22} >{this.props.children}</Col>
        <Col span={1}/>
        </Row>
    }
}

export default App;