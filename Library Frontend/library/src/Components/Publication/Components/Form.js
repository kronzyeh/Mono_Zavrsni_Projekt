import React from 'react';


export default function Form({children, formHeader})
{
    return(
    <div className="centered-container">
    <header> <b>{formHeader} </b> </header>
    <form className="home-div">
      {children}
    </form>
    </div>
    );
};