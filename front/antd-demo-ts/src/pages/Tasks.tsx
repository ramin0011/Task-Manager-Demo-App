import { Button, Col, Divider, MenuProps, message, Popconfirm, Row } from 'antd';
import { Checkbox, Form, Input } from 'antd';
import { Console } from 'console';
import React, { useEffect, useState } from 'react';
import api from '../Services/Api';
import { Avatar, List } from 'antd';
import DateHelper from '../Helpers/DateHelper';
import { useNavigate } from 'react-router-dom';
import { HubConnectionBuilder } from '@microsoft/signalr';

const Tasks: React.FC = () => {
    let navigate = useNavigate();
    const [tasks, setTasks] = useState([]);
    const [loading, setLoading] = useState(false);
    const [connection, setConnection] = useState(null);

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
        }).catch((data) => {
            message.error(data.response.data);
            setLoading(false);
        });
    };

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('https://app-ch.iran.liara.run/hubs/update-tasks')
            .withAutomaticReconnect()
            .build();
        setConnection(newConnection as any);
    }, []);

    useEffect(() => {
        if (connection) {
            (connection as any).start()
                .then(() => {
                    message.info('tasks are updated');
                    (connection as any).on('ReceiveMessage', () => {
                        setLoading(true);
                        api.get('Tasks/GetTasks').then((data) => {
                            setTasks(data.data);
                            setLoading(false);
                        });
                    });
                })
        }
    }, [connection]);

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
                        <br />
                        <span>Create at : {DateHelper.formatDate(new Date(item['createdAt']))}</span>
                        <Divider></Divider>
                    </>

                )}
            />
        </>
    );
};


export default Tasks;