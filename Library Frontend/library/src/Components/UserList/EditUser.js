import React, { useEffect, useState } from "react";
import axios from "axios";
import { useLocation, useNavigate} from "react-router-dom";
import "./userList.css";
import UserService from '../../services/UserService';

export default function EditUser() {
  const location = useLocation();
  const userId = location?.state?.userId;
  const [userData, setUserData] = useState({
    firstName: "",
    lastName: "",
    dateOfBirth: "",
    phoneNumber: "",
    email: "",
  });
  const navigate = useNavigate();
  const [userExists, setUserExists] = useState(false);
  useEffect(() => {
    const fetchUser = async () => {
      try {
        const response = await UserService.getSpecificUser(userId);
        const user = response.data[0];
        
        if (user) {
          setUserData(user);
          setUserExists(true);
          console.log(user);
        } else {
          setUserExists(false);
        }
      } catch (error) {
        console.log(error);
      }
    };

    if (userId) {
      fetchUser();
    }
  }, [userId]);

  const handleChange = (e) => {
    setUserData({ ...userData, [e.target.name]: e.target.value });
  };

  const handleUpdate = async () => {
    try {
      await UserService.editUser(userId, userData);
      console.log("User updated successfully!");
    } catch (error) {
      console.log(error);
    }
  };

  const handleDelete = async () => {
    try {
      await UserService.deleteUser(userId);
      console.log("User deleted successfully!");
      setUserExists(false);
      navigate("/home/users");
    } catch (error) {
      console.log(error);
    }
  };

  if (!userExists) {
    return <h1>User doesn't exist.</h1>;
  }

  return (
    <div className="edit-user-container">
      <h2>Edit User</h2>
      <input
        type="text"
        name="firstName"
        value={userData.firstName}
        onChange={handleChange}
        placeholder="First Name"
      />
      <input
        type="text"
        name="lastName"
        value={userData.lastName}
        onChange={handleChange}
        placeholder="Last Name"
      />
      <input
        type="date"
        name="dateOfBirth"
        value={userData.dateOfBirth}
        onChange={handleChange}
        placeholder="Date of Birth"
      />
      <input
        type="text"
        name="phoneNumber"
        value={userData.phoneNumber}
        onChange={handleChange}
        placeholder="Phone Number"
      />
      <input
        type="email"
        name="email"
        value={userData.email}
        onChange={handleChange}
        placeholder="Email"
      />
      <button onClick={handleUpdate}>Update</button>
      <button onClick={handleDelete}>Delete</button>
    </div>
  );
  }  
