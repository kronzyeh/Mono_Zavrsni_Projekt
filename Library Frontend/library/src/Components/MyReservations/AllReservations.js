import React, { useEffect, useState } from "react";
import axios from "axios";
import "./MyReservations.css";

export default function ReservationList() {
  const [reservations, setReservations] = useState([]);
  const [filteredReservations, setFilteredReservations] = useState([]);
  const [selectedUser, setSelectedUser] = useState("");
  const [userList, setUserList] = useState([]);

  const [startTime, setStartTime] = useState("");
  const role = localStorage.getItem("role");

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
        console.log("User List:", response.data);
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
      .get("https://localhost:44389/api/Reservation", { headers })
      .then((response) => {
        setReservations(response.data);
        console.log("Reservations:", response.data);
        const startTimeTemp = new Date(response.data.StartDate);
        setStartTime(startTimeTemp.toLocaleDateString());
        console.log("Start Time:", startTime);
      })
      .catch((error) => {
        console.error(error);
      });
  }, []);

  useEffect(() => {
    if (selectedUser !== "") {
      const filteredData = reservations.filter(
        (reservation) =>
          reservation.userFirstName.toLowerCase() ===
            selectedUser.split(" ")[0].toLowerCase() ||
          reservation.userLastName.toLowerCase() ===
            selectedUser.split(" ")[1].toLowerCase()
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

  console.log("Filtered Reservations:", filteredReservations);

  return (
    <div className="reservation-list">
      <h2>Reservation List</h2>

      {role === "Admin" && (
        <div className="filter-section">
          <label htmlFor="userFilter">Filter by User:</label>
          <select
            id="userFilter"
            value={selectedUser}
            onChange={(e) => setSelectedUser(e.target.value)}
          >
            <option value="">All Users</option>
            {userList.map((user) => (
              <option key={user.id} value={`${user.firstName} ${user.lastName}`}>
                {`${user.firstName} ${user.lastName}`}
              </option>
            ))}
          </select>
        </div>
      )}

      <table>
        <thead>
          <tr>
            <th>Publication Title:</th>
            <th>Reservation First Name:</th>
            <th>Reservation Last Name:</th>
            <th>Reservation Status:</th>
            <th>Reservation Start Date:</th>
            <th>Reservation End Date:</th>
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
                    <button className="btn" onClick={() => handleUpdate(reservation.id)}>
                      Extend Reservation
                    </button>
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
