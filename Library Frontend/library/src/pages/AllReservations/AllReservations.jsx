import React from 'react';
import ReservationList from '../../Components/MyReservations/AllReservations';
import Header from '../../Components/Header/Header';
import Footer from '../../Components/Footer/Footer';
import '../Body.css';


export default function AllReservations (){

    return(

        <div>
            <header>
                <Header />
            </header>
            <body>
                <ReservationList/>
            </body>
            <footer>
                <Footer />
            </footer>
        </div>

    )

}