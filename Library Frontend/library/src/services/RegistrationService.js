import React from 'react';
import axios from 'axios';

const postLogin = (email, password) => {
    return axios.post(`https://localhost:44389/api/Registration?email=${email}&password=${password}`);
  };

const postRegister = (user) => {
    return axios.post('https://localhost:44389/api/Registration', user)
}
  const RegistrationService = {
    postLogin,
    postRegister
  };
export default RegistrationService;