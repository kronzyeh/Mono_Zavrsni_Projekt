import React from 'react';
import Header from '../../Components/Header/Header';
import Footer from '../../Components/Footer/Footer';
import Reservations from '../../Components/MyReservations/MyReservations';
import { Link } from 'react-router-dom';
import './MyReservations.css';
import '../Body.css';

export default function MyReservations (){

    return(

        <div>

            <header>
                <Header />
            </header>
            <body>
                <Reservations/>
            </body>
            <footer>
                <Footer />
            </footer>
        </div>

    )

}