
import React from 'react';
import { useState } from 'react';

export default function Dropdown({options, label, selectedOption, handleChange}){
    
    return(
        <div>
        <label htmlFor="dropdown"> {label}:</label>
        <select id="dropdown" value={selectedOption}  onChange={handleChange}>
          <option id='' key= 'defaultSelectedValue' value='defaultSelectedValue' label='<select>'></option>
          {options.map((option) => (
            <option  key={option.id} value={option.value} label={option.label} name = {option.name}>
              {option.label}
            </option>
          ))}
        </select>
      </div>
    );
}