import React, { useState, useEffect } from 'react';
import axios from 'axios';

export default function MyReservations() {
  const [publicationList, setPublicationList] = useState([]);
  const [userList, setUserList] = useState([]);
  const [selectedPublication, setSelectedPublication] = useState('');
  const [selectedUser, setSelectedUser] = useState('');
  const [reservation, setReservation] = useState({
    id: '',
    userId: '',
    publicationId: ''
  });

  useEffect(() => {
    fetchPublicationList();
    fetchUserList();
  }, []);

  const fetchPublicationList = () => {
    axios
      .get('https://localhost:44389/api/Publication')
      .then(response => {
        setPublicationList(response.data);
      })
      .catch(error => {
        console.log(error);
      });
  };

  const fetchUserList = () => {
    axios
      .get('https://localhost:44389/api/User')
      .then(response => {
        setUserList(response.data);
        console.log(response.data);
      })
      .catch(error => {
        console.log(error);
      });
  };

  const handleSubmit = event => {
    event.preventDefault();

    const reservationData = {
      id: reservation.id,
      userId: reservation.userId,
      publicationId: reservation.publicationId,
    };
    const token = localStorage.getItem("token"); 
      const headers = {
        Authorization: `Bearer ${token}`,
      };

    axios
      .post('https://localhost:44389/api/Reservation', reservationData, { headers })
      .then(response => {
        console.log('Reservation added successfully');
        setSelectedPublication('');
        setSelectedUser('');
      })
      .catch(error => {
        console.log(error);
      });
  };
  
  const handlePublicationChange = event => {
    const publicationId = event.target.value;
    const publication = publicationList.find(pub => pub.id === publicationId);
  
    setReservation(prevReservation => ({
      ...prevReservation,
      publicationId: publication ? publication.id : ''
    }));
    setSelectedPublication(publicationId);
  };
  
  

  const handleUserChange = event => {
    const userId = event.target.value;
    const user = userList.find(usr => usr.id === userId);

    setReservation(prevReservation => ({
      ...prevReservation,
    userId: user ? user.id : ''
    }));
    setSelectedUser(userId);
  };

  return (
    <div className='body add-new-reservation'>
      <div className='add-new-reservation-content'>
        <p>New Reservation</p>

        <select
          className='add-new-reservation-inputs'
          value={selectedPublication}
          onChange={handlePublicationChange}
        >
          <option value=''>Select publication</option>
          {publicationList.map(publication => (
            <option key={publication.id} value={publication.id}>
              {publication.title}
            </option>
          ))}
        </select>
            <br></br>
            <select
  className='add-new-reservation-inputs'
  value={selectedUser || ''}
  onChange={handleUserChange}
>
  <option value=''>Select user</option>
  {userList.map(user => (
    <option key={user.id} value={user.id}>
      {user.firstName} {user.lastName}
    </option>
  ))}
</select>


        <div className='add-new-reservation-buttons-container'>
          <button className='add-new-reservation-buttons' type='submit' onClick={handleSubmit}>
            Confirm
          </button>
        </div>
      </div>
    </div>
  );
}
