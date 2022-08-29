import { Button, Col, Divider, MenuProps, message, Row } from 'antd';
import { Checkbox, Form, Input } from 'antd';
import { Console } from 'console';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../Services/Api';


const Login: React.FC = () => {
    let navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const onFinish = (values: any) => {
        setLoading(true);
        api.post('Authentication', { 'username': values.username, 'password': values.password }).then((data) => {
            setLoading(false);
            localStorage.setItem('token',data.data);
            message.success('You are loged in!');
            navigate("/tasks");
        }).catch((data)=>{
          
            message.error(data.response.data.title);
        });

    };

   
    return (
        <>
            <Divider></Divider>
            <Form
               
                name="basic"
                labelCol={{ span: 2 }}
                wrapperCol={{ span: 16 }}
                initialValues={{ remember: true }}
                onFinish={onFinish}
                autoComplete="off"
            >
                <Form.Item
                    label="Username"
                    name="username"
                    rules={[{ required: true, message: 'Please input your username!' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Password"
                    name="password"
                    rules={[{ required: true, message: 'Please input your password!' }]}
                >
                    <Input.Password />
                </Form.Item>

                <Form.Item name="remember" valuePropName="checked" wrapperCol={{ offset: 8, span: 16 }}>
                    <Checkbox>Remember me</Checkbox>
                </Form.Item>

                <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
                    <Button type="primary" htmlType="submit"  loading={loading}>
                        Submit
                    </Button>
                </Form.Item>
            </Form>
        </>
    );
};


export default Login;