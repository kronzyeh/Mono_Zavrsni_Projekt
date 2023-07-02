import React from 'react';
import '../Body.css';
import './AddNewReservation.css';
import Header from '../../Components/Header/Header';
import Footer from '../../Components/Footer/Footer';
import AddReservation from '../../Components/MyReservations/AddReservation';

export default function AddNewReservation (){

    return(

        <div>
            <header>
                <Header />
            </header>
            <body>
                <div>
                <AddReservation/>
                </div>
            </body>
            <footer>
                <Footer/>
            </footer>
        </div>

    )

}