import React from 'react';
import { Link } from 'react-router-dom';
import './landing.css';

export default function Landing() {
  return (
    <>
      <div className="page-image">
        <div className="hero-text">
          <h1>Open the Door to Adventure</h1>
          <h2>Explore the world of books and unlock new horizons.</h2>
          <Link to="/login">
            <button className="button">Login</button>
          </Link>
          <Link to="/register">
            <button className="button">Register</button>
          </Link>
        </div>
      </div>
    </>
  );
}
