
import React, { useEffect, useState } from "react";
import axios from "axios";
import { Link } from "react-router-dom";
//import EditUser from "./EditUser";
import "./userList.css";

export default function UserList() {
  const [users, setUsers] = React.useState([]);

  useEffect(() => {
    axios
      .get("https://localhost:44389/api/User") 
      .then((response) => {
        setUsers(response.data);
        console.log(response.data);
      })
      .catch((error) => {
        console.error(error);
      });
  }, []);

  return (
    <div className="user-list">
      <h2>User List</h2>
      <table>
        <thead>
          <tr>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Date of Birth</th>
            <th>Phone Number</th>
            <th>Email</th>
            {/* <th>Start Date</th>
            <th>End Date</th> */}
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr key={user.id}>
              <td>{user.firstName}</td>
              <td>{user.lastName}</td>
              <td>{user.dateOfBirth}</td>
              <td>{user.phoneNumber}</td>
              <td>{user.email}</td>
              {/* <td>{user.StartDate}</td>
              <td>{user.EndDate}</td> */}
              <td>
              <Link to={`/users/${user.id}`} state={{ userId: user.id }}>
                    Edit
              </Link>

              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
