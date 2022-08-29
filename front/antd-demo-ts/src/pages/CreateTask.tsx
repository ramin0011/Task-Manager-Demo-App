import { Button, Col, DatePicker, Divider, MenuProps, message, Row } from 'antd';
import { Checkbox, Form, Input } from 'antd';
import { RangePickerProps } from 'antd/lib/date-picker';
import { Console } from 'console';
import moment from 'moment';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../Services/Api';


const CreateTask: React.FC = () => {
    let navigate = useNavigate();
    const [loading, setLoading] = useState(false);

    const disabledDate: RangePickerProps['disabledDate'] = current => {
        // Can not select days before today and today
        return current && current < moment().endOf('day');
      };
      
    const onFinish = (values: any) => {
        setLoading(true);
        api.post('Tasks/CreateTask', { 'name': values.name, 'description': values.description , 'deadline':values.deadline }).then((data) => {
            setLoading(false);
            localStorage.clear();
            localStorage.setItem('token', data.data);
            message.success('You are loged in!');
            navigate("/tasks");
        }).catch((data) => {
            message.error(data.response.data.title);
        });

    };


    return (
        <>
            <Divider >Create Task</Divider>
            <Form

                name="basic"
                labelCol={{ span: 2 }}
                wrapperCol={{ span: 16 }}
                initialValues={{ remember: true }}
                onFinish={onFinish}
                autoComplete="off"
            >
                <Form.Item
                    label="Task Name"
                    name="name"
                    rules={[{ required: true, message: 'Please input your task name' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Task Description"
                    name="description"
                    rules={[{ required: true, message: 'Please input your task desc.' }]}
                >
                    <Input />
                </Form.Item>
                <Form.Item label="Task Deadline" name="deadline"   rules={[{ required: true, message: 'Please input your task deadline.' }]}>
                    <DatePicker disabledDate={disabledDate}/>
                </Form.Item>

                <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
                    <Button type="dashed" htmlType="submit" loading={loading}>
                        Create
                    </Button>
                </Form.Item>
            </Form>
        </>
    );
};


export default CreateTask;