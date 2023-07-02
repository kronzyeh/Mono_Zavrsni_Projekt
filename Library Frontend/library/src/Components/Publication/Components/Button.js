import React from 'react';

export default function Button({label, handleClick}){
    return(
    
        <div>
        <button className="btn" onClick={handleClick}>
            {label}
        </button>
       </div>

        
    );

}
