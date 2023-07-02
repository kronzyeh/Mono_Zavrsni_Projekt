import React from 'react';

import UserList from '../Components/UserList/UserList';
import Header from '../Components/Header/Header';
import Footer from '../Components/Footer/Footer';
import "../Components/Landing/landing.css";

export default function pages (){

    return(
        <div>
            <header>
                <Header/>
            </header>
            <body>
                <div className="body-container">
                    <UserList/>
                </div>
            </body>
            <footer>
                <Footer/>
            </footer>
        </div>

    )

}