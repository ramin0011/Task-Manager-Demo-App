import { Button, Col, Divider, MenuProps, Popconfirm, Row } from 'antd';
import { Checkbox, Form, Input } from 'antd';
import { Console } from 'console';
import React, { useEffect, useState } from 'react';
import api from '../Services/Api';
import { Avatar, List } from 'antd';
import DateHelper from '../Helpers/DateHelper';
import { useNavigate } from 'react-router-dom';

const Tasks: React.FC = () => {
    let navigate = useNavigate();
    const [tasks, setTasks] = useState([]);
    const [loading, setLoading] = useState(false);
    const onFinish = (values: any) => {
        api.post('Authentication', { username: values[0], password: values[1] }).then((data) => {
            console.log(data);
        });
    };
    useEffect(() => {
        setLoading(true);
        api.get('Tasks/GetTasks').then((data) => {
            setTasks(data.data);
            setLoading(false);
        });
    }, []);

    const onAssign = (id: string) => {
        setLoading(true);
        api.get(`Tasks/ClaimTask?taskId=${id}`).then((data) => {
            setLoading(false);
            navigate("/mytasks");
        });
    };


    return (
        <>
            <Divider>Tasks</Divider>
            <List
                loading={loading}
                itemLayout="horizontal"
                dataSource={tasks}
                renderItem={item => (
                    <>
                        <List.Item actions={item['claimedUser'] == null ? [<Popconfirm
                            title="Are you sure to assign this task to your self?"
                            onConfirm={() => onAssign(item['id'])}
                            okText="Yes"
                            cancelText="No"
                        ><Button type='primary' key="list-loadmore-more" loading={loading}>Claim</Button>
                        </Popconfirm>] : [<span>claimed  by: {item['claimedUserName']}</span>]}>

                            <List.Item.Meta
                                avatar={<Avatar src="https://joeschmoe.io/api/v1/random" />}
                                title={<a href="https://ant.design">{item["name"]}</a>}
                                description={item['description']}
                            />

                        </List.Item>
                        <span>Deadline : {DateHelper.formatDate(new Date(item['deadline']))}</span>
                        <br/>
                        <span>Create at : {DateHelper.formatDate(new Date(item['createdAt']))}</span>
                        <Divider></Divider>
                    </>

                )}
            />
        </>
    );
};


export default Tasks;