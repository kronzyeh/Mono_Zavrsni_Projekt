import React, { useState } from 'react';
import axios from 'axios';
import './author.css';

export default function NewAuthor() {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [dateOfBirth, setDateOfBirth] = useState('');
  const [dateOfDeath, setDateOfDeath] = useState('');
  const [nationality, setNationality] = useState('');

  const handleAddAuthor = () => {
    const newAuthor = {
      firstName,
      lastName,
      dateOfBirth,
      dateOfDeath,
      nationality,
    };

    axios
      .post('https://localhost:44389/api/library/add/author', newAuthor)
      .then((response) => {
        // Handle successful response here, if needed
        console.log('Author added successfully:', response.data);
      })
      .catch((error) => {
        // Handle error here
        console.error('Error adding author:', error);
      });
  };

  return (
    <div className="add-author-content edit-inputs">
      <p>Add new author</p>

      <input
        className="add-author-inputs"
        type="text"
        placeholder="First Name"
        value={firstName}
        onChange={(e) => setFirstName(e.target.value)}
      />

      <input
        className="add-author-inputs"
        type="text"
        placeholder="Last Name"
        value={lastName}
        onChange={(e) => setLastName(e.target.value)}
      />

      <label className="add-author-labels" htmlFor="dateofbirth">
        Date of Birth:
      </label>
      <input
        className="add-author-inputs"
        type="date"
        id="dateofbirth"
        name="dateofbirth"
        placeholder="Date of Birth"
        value={dateOfBirth}
        onChange={(e) => setDateOfBirth(e.target.value)}
      />

      <label className="add-author-labels" htmlFor="dateofdeath">
        Date of Death:
      </label>
      <input
        className="add-author-inputs"
        type="date"
        id="dateofdeath"
        name="dateofdeath"
        placeholder="Date of Death"
        value={dateOfDeath}
        onChange={(e) => setDateOfDeath(e.target.value)}
      />

      <input
        className="add-author-inputs"
        type="text"
        placeholder="Nationality"
        value={nationality}
        onChange={(e) => setNationality(e.target.value)}
      />

      <div className="add-author-buttons-container edit-buttons">
        <button className="add-author-buttons" onClick={handleAddAuthor}>
          Add
        </button>
      </div>
    </div>
  );
}
