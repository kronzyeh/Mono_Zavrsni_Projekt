import React, { useState } from 'react';
import './Home.css';
import Header from '../Header/Header';
import Footer from '../Footer/Footer';

export default function Home (){

    
    const options = 
        [

            { label: 'Newest Publications', value: 'newest publications' },
        
            { label: 'Oldest Publications', value: 'oldest publications' },
        
            { label: 'Genre', value: 'genre' },
     
        ];
     

      const [value, setValue] = useState('newest publications');
     
      const handleChange = (event) => {
     
        setValue(event.target.value);
     
      };


      const Dropdown = ({ label, value, options, onChange }) => {

        return (
       
          <label>
       
            {label}
       
            <select value={value} onChange={onChange}>
       
              {options.map((option) => (
       
                <option value={option.value}>{option.label}</option>
       
              ))}
       
            </select>
       
          </label>
       
        );
       
       };



    return(

      <div className='home-container'>


            <Header />


            <div className='user-home'>

              <div className='search-filter-publications'> 

                  <input placeholder="Filter Publications" />
                  <button>SEARCH</button>


                  <Dropdown
                      label="What are we searching for?"
                      options={options}
                      value={value}
                      onChange={handleChange}
                  />

                  <p>Displaying by {value}</p>

              </div>


              <div className='publications'>

                <a className='publication' href="/myreservations">{value}</a>
                <a className='publication' href="/myreservations">{value}</a>
                <a className='publication' href="/myreservations">{value}</a>

              </div>

            </div>


            <Footer />

        </div>

    )

}




















    




            








   
      

   

   
