import React from 'react';
import LogoutButton from "../Components/Login/LogoutButton";
import Home from '../Components/Home/Home';

export default function UserHomePage (){

    return(

        <div>
            <Home />
            <LogoutButton />
        </div>

    )

}