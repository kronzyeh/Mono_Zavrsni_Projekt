import React, { useState, useEffect } from 'react';
import './login.css'
import { Link } from 'react-router-dom';
import { useNavigate } from 'react-router';
import RegistrationService from '../../services/RegistrationService';


function LoginForm() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const navigate = useNavigate();

  const handleEmailChange = (event) => {
    setEmail(event.target.value);
  };

  const handlePasswordChange = (event) => {
    setPassword(event.target.value);
  };

  const handleLogin = async (event) => {
    event.preventDefault();
  
    try {
      const response = await RegistrationService.postLogin(email, password);
  
      console.log('Login successful:', response.data);
      localStorage.setItem('token', response.data.accessToken);
      localStorage.setItem('role', response.data.role);
      console.log(localStorage.getItem('token'));
      const userRole = response.data.Role;
  
      setEmail('');
      setPassword('');

      if (localStorage.getItem("token") != null){
        navigate("/home");
      }
    } catch (error) {
      console.error('Login failed:', error);
    }
  };
  
  useEffect(() => {
    // Check if the user is already logged in
    const token = localStorage.getItem('token');
    const userRole = localStorage.getItem('role');

    if (token) {
      if (userRole === 'User') {
        navigate('/home');
      } else if (userRole === 'Admin') {
        navigate('/home'); 
      }
    }
  }, [navigate]);

  return (
    <div className="page-image1">
      <div className="centered-container">
        <div className="login-container">
          <form className="login-div" onSubmit={handleLogin}>
            <label htmlFor="email">Email:</label>
            <input
              type="text"
              id="email"
              value={email}
              onChange={handleEmailChange}
            />
            <label htmlFor="password">Password:</label>
            <input
              type="password"
              id="password"
              value={password}
              onChange={handlePasswordChange}
            />
            <button type="submit">Login</button>
           </form>
            <div className="register-link">
              Don't have an account? <Link to="/register">Register here.</Link>
            </div>
        </div>
      </div>
    </div>
  );
}

export default LoginForm;
