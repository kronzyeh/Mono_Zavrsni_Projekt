import React from 'react';
import axios from 'axios';

const getAllUsers = () => {
    return axios.get(`https://localhost:44389/api/User`);
  };

const getSpecificUser = (userId) => {
    return axios.get(`https://localhost:44389/api/User/${userId}`)
}
const deleteUser = (userId) => {
    return axios.delete(`https://localhost:44389/api/User/${userId}`)
}
const editUser = (userId, user) => {
    return axios.put(`https://localhost:44389/api/User/${userId}`, user)
}

  const RegistrationService = {
    getAllUsers,
    getSpecificUser,
    deleteUser,
    editUser
  };
export default RegistrationService;