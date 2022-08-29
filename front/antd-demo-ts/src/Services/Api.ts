import axios, { HeadersDefaults } from 'axios';


export default axios.create({
  baseURL: `https://app-ch.iran.liara.run/`,headers:{
    Authorization: `Bearer ${localStorage.getItem('token')}`
  } as any
}); 