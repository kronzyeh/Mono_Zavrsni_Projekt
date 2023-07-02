import React from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './login.css';

function LogoutForm() {
  const navigate = useNavigate();

  const handleLogout = async () => {
    try {
      //await axios.get('https://localhost:44389/api/logout');
      localStorage.removeItem('token');
      localStorage.removeItem('role');
      navigate('/');
      

      
    } catch (error) {
  
      console.error('Logout error:', error);
    }
  };

  return (
    <form onSubmit={handleLogout}>
      <button className="btn" type="submit">Logout</button>
    </form>
  );
}

export default LogoutForm;
