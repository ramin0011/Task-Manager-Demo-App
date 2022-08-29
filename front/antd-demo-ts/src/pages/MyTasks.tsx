import { Button, Col, Divider, MenuProps, Row } from 'antd';
import { Checkbox, Form, Input } from 'antd';
import { Console } from 'console';
import React, { useEffect, useState } from 'react';
import api from '../Services/Api';
import { Avatar, List } from 'antd';
import DateHelper from '../Helpers/DateHelper';

const MyTasks: React.FC = () => {
    const [tasks, setTasks] = useState([]);
    const [loading, setLoading] = useState(false);
        const onFinish = (values: any) => {
        api.post('Authentication', { username: values[0], password: values[1] }).then((data) => {
            console.log(data);
        });
    };
    useEffect(() => {
        setLoading(true);
        api.get('Tasks/GetMyTasks').then((data) => {
            setTasks(data.data);
            setLoading(false);
        });
    }, []);



    return (
        <>
            <Divider>tasks</Divider>
            <List 
                loading={loading}
                itemLayout="horizontal"
                dataSource={tasks}
                renderItem={item => (
                  <>
                    <List.Item  actions={[ <Button type='primary' key="list-loadmore-more">Claim</Button>]}>
                       
                      <List.Item.Meta
                          avatar={<Avatar src="https://joeschmoe.io/api/v1/random" />}
                          title={<a href="https://ant.design">{item["name"]}</a>}
                          description={item['description']}
                      />
                    
                  </List.Item>
                  <span>Deadline : { DateHelper.formatDate(new Date(item['deadline']))}</span>
                  <Divider></Divider>
                  </>  
                  
                )}
            />
        </>
    );
};


export default MyTasks;