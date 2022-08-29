import { Button, Col, Divider, MenuProps, Row } from 'antd';
import { Checkbox, Form, Input } from 'antd';
import { Console } from 'console';
import React, { useState } from 'react';
import api from '../Services/Api';


const CreateTask: React.FC = () => {
    const onFinish = (values: any) => {
        api.post('Authentication', { username: values[0], password: values[1] }).then((data) => {
            console.log(data);
        });
    };

   
    return (
        <>
            <Divider >create task</Divider>
         
        </>
    );
};


export default CreateTask;