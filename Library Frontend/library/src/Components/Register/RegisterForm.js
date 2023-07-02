import React, { useState } from 'react';
import axios from 'axios';
import '../Login/login.css';
import { Link } from 'react-router-dom';
import { useNavigate } from 'react-router';
import RegistrationService from '../../services/RegistrationService';


function RegisterForm() {

  const handleClick = (e) => {
    e.target.classList.add('button-animation');
  };
  const [user, setUser] = useState({
    firstName: '',
    lastName: '',
    phoneNumber: '',
    address: '',
    email: '',
    password: '',
    DOB: ''
  });

  const navigate = useNavigate();

  const handleEmailChange = (event) => {
    setUser({ ...user, email: event.target.value });
  };

  const handlePasswordChange = (event) => {
    setUser({ ...user, password: event.target.value });
  };

  const handleFirstNameChange = (event) => {
    setUser({ ...user, firstName: event.target.value });
  };

  const handleLastNameChange = (event) => {
    setUser({ ...user, lastName: event.target.value });
  };

  const handlePhoneNumberChange = (event) => {
    setUser({ ...user, phoneNumber: event.target.value });
  };

  const handleAddressChange = (event) => {
    setUser({ ...user, address: event.target.value });
  };

  const handleDOBChange = (event) => {
    setUser({ ...user, DOB: event.target.value });
  };

  const handleRegister = async (event) => {
    event.preventDefault();

    try {
      // Make an API request to register user
      const response = RegistrationService.postRegister(user);

      // Handle successful registration
      console.log('Registration successful:', response.data);

      // Reset form fields
      setUser({
        firstName: '',
        lastName: '',
        phoneNumber: '',
        address: '',
        email: '',
        password: '',
        DOB: ''
      });

      // Redirect to login page
      navigate("/");
    } catch (error) {
      // Handle registration error
      console.error('Registration failed:', error);
    }
  };

  return (
    <div className="page-image1">
      <div className="centered-container">
    <div className="register-container">
      <form className="login-div" onSubmit={handleRegister}>
        <label htmlFor="firstName">First Name:</label>
        <input
          type="text"
          id="firstName"
          value={user.firstName}
          onChange={handleFirstNameChange}
        />
        <label htmlFor="lastName">Last Name:</label>
        <input
          type="text"
          id="lastName"
          value={user.lastName}
          onChange={handleLastNameChange}
        />
        <label htmlFor="email">Email:</label>
        <input
          type="text"
          id="email"
          value={user.email}
          onChange={handleEmailChange}
        />
        <label htmlFor="DOB">Date of Birth:</label>
        <input
          type="date"
          id="DOB"
          value={user.DOB}
          onChange={handleDOBChange}
        />
        <label htmlFor="phoneNumber">Phone Number:</label>
        <input
          type="text"
          id="phoneNumber"
          value={user.phoneNumber}
          onChange={handlePhoneNumberChange}
        />
        <label htmlFor="address">Address:</label>
        <input
          type="text"
          id="address"
          value={user.address}
          onChange={handleAddressChange}
        />
        <label htmlFor="password">Password:</label>
        <input
          type="password"
          id="password"
          value={user.password}
          onChange={handlePasswordChange}
        />
        <button type="submit">Register</button>
        <div onClick={handleClick} className="register-link">Already have an account? <Link to="/login">Login</Link></div>
      </form>
    </div>
    </div>
    </div>
  );
}

export default RegisterForm;
