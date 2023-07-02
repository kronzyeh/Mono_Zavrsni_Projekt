import React, { useState } from 'react';
import Header from '../../Components/Header/Header';
import Footer from '../../Components/Footer/Footer';
import { Link } from 'react-router-dom';
import './HomePage.css';
import '../Body.css';

export default function HomePage() {
  const options = [
    { label: 'Newest Publications', value: 'Newest Publications' },
    { label: 'Oldest Publications', value: 'Oldest Publications' },
    { label: 'Genre', value: 'Genre' }
  ];

  const [value, setValue] = useState('Newest Publications');

  const handleChange = (event) => {
    setValue(event.target.value);
  };

  const Dropdown = ({ label, value, options, onChange }) => {
    return (
      <label>
        {label}
        <select className='select-filter' value={value} onChange={onChange}>
          {options.map((option, index) => (
            <option key={index} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>
      </label>
    );
  };

  return (
    <div>
      <Header />
      <div className='home-body'>
        <div className='home-content'>
          <div className='search-filter-publications'>
            <div className='input-field'>
              <input className='search-input' placeholder='Filter Publications' />
              <button className='search-button'>Search</button>
            </div>
            <Dropdown label='What are we searching for?' options={options} value={value} onChange={handleChange} />
            <p>Displaying by {value}</p>
          </div>

          <div className='publication'>
            <Link to='/publication'>
              <p className='publication-item'>{value}</p>
            </Link>
            <div className='publication-options'>
              <Link to={'/publication/edit/id'}>
                <p>Edit</p>
              </Link>
              <button className='remove-publication'>
                <p>Remove</p>
              </button>
            </div>
          </div>

          <div className='publication'>
            <Link to='/publication'>
              <p className='publication-item'>{value}</p>
            </Link>
            <div className='publication-options'>
              <Link to={'/publication/edit/id'}>
                <p>Edit</p>
              </Link>
              <button className='remove-publication'>
                <p>Remove</p>
              </button>
            </div>
          </div>

          <div className='publication'>
            <Link to='/publication'>
              <p className='publication-item'>{value}</p>
            </Link>
            <div className='publication-options'>
              <Link to={'/publication/edit/id'}>
                <p>Edit</p>
              </Link>
              <button className='remove-publication'>
                <p>Remove</p>
              </button>
            </div>
          </div>

          <div className='publication'>
            <Link to='/publication'>
              <p className='publication-item'>{value}</p>
            </Link>
            <div className='publication-options'>
              <Link to={'/publication/edit/id'}>
                <p>Edit</p>
              </Link>
              <button className='remove-publication'>
                <p>Remove</p>
              </button>
            </div>
          </div>
        </div>
      </div>
      <Footer />
    </div>
  );
}
