import axios, { HeadersDefaults } from 'axios';


export default axios.create({
  baseURL: `https://localhost:7114/`,headers:{
    Authorization: `Bearer ${localStorage.getItem('token')}`
  } as any
}); 