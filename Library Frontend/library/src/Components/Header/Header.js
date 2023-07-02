import React from 'react';
import './Header.css';
import { Link } from 'react-router-dom';
import LogoutButton from '../Login/LogoutButton';

export default function Header() {
  const role = localStorage.getItem('role');

  return (
    <div className="header-container">
      <header className="header">
        <div className="logo-container">
          <Link to="/">
            <img className="logo" src={'/open-book.png'} alt="Logo" />
          </Link>
        </div>

        <div className="header-buttons">
          {role === 'User' && (
            <>
              <div className="header-item">
                <Link className="btn" to="/myreservations">
                  My Reservations
                </Link>
              </div>
              <div className="header-item">
                <LogoutButton className="btn" />
              </div>
            </>
          )}

          {role === 'Admin' && (
            <>
              <div className="header-item">
                <Link className="btn" to="/add">
                  Add
                </Link>
              </div>
              <div className="header-item">
                <Link className="btn" to="/home/users">
                  User Administration
                </Link>
              </div>
              <div className="header-item">
                <Link className="btn" to="/myreservations">
                  My Reservations
                </Link>
              </div>
              <div className="header-item">
                <Link className="btn" to="/home/allreservations">
                  All Reservations
                </Link>
              </div>
              <div className="header-item">
                <LogoutButton className="btn" />
              </div>
            </>
          )}
        </div>
      </header>
    </div>
  );
}
