import React, { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import axios from "axios";
import "./MyReservations.css";

export default function MyReservations() {
  const [reservations, setReservations] = useState([]);
  const [filteredReservations, setFilteredReservations] = useState([]);
  const [selectedUser, setSelectedUser] = useState("");
  const [userList, setUserList] = useState([]);

  const [startTime, setStartTime] = useState("");
  const role = localStorage.getItem("role");

  const location = useLocation();
  useEffect(() => {
    const token = localStorage.getItem("token");
    const headers = {
      Authorization: `Bearer ${token}`,
    };
  
    // Fetch the list of users
    axios
      .get("https://localhost:44389/api/User", { headers })
      .then((response) => {
        setUserList(response.data);
      })
      .catch((error) => {
        console.error(error);
      });
  }, []);

  useEffect(() => {
    const token = localStorage.getItem("token");
    const headers = {
      Authorization: `Bearer ${token}`,
    };

    axios
      .get("https://localhost:44389/api/Reservation", { headers})
      .then((response) => {
        setReservations(response.data);
        const startTimeTemp = new Date(response.data.StartDate);
        setStartTime(startTimeTemp.toLocaleDateString());
        console.log(startTime);
      })
      .catch((error) => {
        console.error(error);
      });
  }, []);

  useEffect(() => {
    if (selectedUser !== "") {
      const filteredData = reservations.filter(
        (reservation) =>
          reservation.UserFirstName.toLowerCase() +
            " " +
            reservation.UserLastName.toLowerCase() ===
          selectedUser.toLowerCase()
      );
      setFilteredReservations(filteredData);
    } else {
      setFilteredReservations(reservations);
    }
  }, [selectedUser, reservations]);

  const handleUpdate = async (reservationId) => {
    try {
      const token = localStorage.getItem("token"); 
      const headers = {
        Authorization: `Bearer ${token}`,
      };
  
      await axios.put(
        `https://localhost:44389/api/Reservation/${reservationId}`,
        null,
        { headers }
      );
      console.log("Reservation updated successfully!");
    } catch (error) {
      console.log(error);
    }
  };
  
  

  return (
    <div className="reservation-list">
      <h2>Reservation List</h2>
      <table>
        <thead>
          <tr>
            <th>Publication Title: </th>
            <th>Reservation First Name: </th>
            <th>Reservation Last Name: </th>
            <th>Reservation Status: </th>
            <th>Reservation Start Date: </th>
            <th>Reservation End Date: </th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
  {filteredReservations.length === 0 ? (
    <tr>
      <td colSpan={7}>No reservations found for the selected user.</td>
    </tr>
  ) : (
    filteredReservations.map((reservation) => {
      const startTimeTemp = new Date(reservation.startDate);
      const startTime = startTimeTemp.toLocaleDateString();

      const endTimeTemp = new Date(reservation.endDate);
      const endTime = endTimeTemp.toLocaleDateString();

      return (
        <tr key={reservation.id}>
          <td>{reservation.publicationTitle}</td>
          <td>{reservation.userFirstName}</td>
          <td>{reservation.userLastName}</td>
          <td>{reservation.isReturned ? "Returned" : "In process"}</td>
          <td>{startTime}</td>
          <td>{endTime}</td>
          <td>
            <button className="btn" onClick={() => handleUpdate(reservation.id)}>Extend reservation</button>
          </td>
        </tr>
      );
    })
  )}
</tbody>

      </table>
    </div>
  );
}
