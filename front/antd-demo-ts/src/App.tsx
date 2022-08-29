
import {
  useParams,
  useNavigate,
  useLocation,
} from "react-router-dom";
import type { MenuProps } from 'antd';
import { Breadcrumb, Layout, Menu } from 'antd';
import React, { useEffect, useState } from 'react';
import { Link, Outlet, Route, Router } from 'react-router-dom';
import './App.css';

const { Header, Content, Footer, Sider } = Layout;

type MenuItem = Required<MenuProps>['items'][number];

function getItem(
  label: React.ReactNode,
  key: React.Key,
  icon?: React.ReactNode,
  children?: MenuItem[],
): MenuItem {
  return {
    key,
    icon,
    children,
    label,
  } as MenuItem;
}


const App: React.FC = () => {
  const [collapsed, setCollapsed] = useState(false);
  let navigate = useNavigate();
  let location = useLocation();
  useEffect(
    () => {
      if(localStorage.getItem('token')==null){
        navigate("/login");
      }
    },
    [location]
  )

 
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Sider collapsible collapsed={collapsed} onCollapse={value => setCollapsed(value)}>
        <div className="logo" />
        <Menu theme="dark"  >
          <Menu.Item key="0"><Link to="/tasks"></Link></Menu.Item>
          <Menu.Item key="0"><Link to="/tasks"></Link></Menu.Item>
          <Menu.Item key="2">
            <Link to="/login">
              <span>Login</span>
            </Link>
          </Menu.Item>
          <Menu.Item key="3">
            <Link to="/create-task">
              <span>Create Task</span>
            </Link>
          </Menu.Item>
          <Menu.Item key="4">
            <Link to="/tasks">
              <span>Tasks</span>
            </Link>
          </Menu.Item>
          <Menu.Item key="5">
            <Link to="/mytasks">
              <span>My Tasks</span>
            </Link>
          </Menu.Item>
        </Menu>
      </Sider>
      <Layout className="site-layout">
        <Header className="site-layout-background" style={{ padding: 0 }} />
        <Content style={{ margin: '0 16px' }}>
          <Breadcrumb style={{ margin: '16px 0' }}>
            <Breadcrumb.Item>User</Breadcrumb.Item>
            <Breadcrumb.Item>task management</Breadcrumb.Item>
          </Breadcrumb>
          <Outlet />
        </Content>
        <Footer style={{ textAlign: 'center' }}>Ant Design ©2018 Created by Ant UED</Footer>
      </Layout>



    </Layout>
  );
};

export default App;
