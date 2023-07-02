import React from 'react';


export default function FormInputElement({name, type, label, value, handleChange}){
   return(
   <>
    <label>
        {label}
        <input value= {value} name={name} type={type}  placeholder='Enter text here.' onChange = {handleChange} />
    </label>
    <br></br>
    </>
   );
};