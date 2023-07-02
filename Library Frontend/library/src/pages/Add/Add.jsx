import React from 'react';
import '../Body.css';
import './Add.css';
import Header from '../../Components/Header/Header';
import Footer from '../../Components/Footer/Footer';
import { Link } from 'react-router-dom';

export default function Add (){

    return(

        <div>

            <Header />

            <div className='body add'>

                <div className='add-content'>

                    <p>HERE YOU CAN DECIDE WHAT TO ADD</p>
                    
                    <Link className="add-buttons" to="/add/reservation">
                        RESERVATION
                    </Link>

                    <Link className="add-buttons" to="/add/author">
                        AUTHOR
                    </Link>
                    
                    <Link className="add-buttons" to="/add/publisher">
                        PUBLISHER
                    </Link>

                    <Link className="add-buttons" to="/add/genre">
                        GENRE
                    </Link>

                    <Link className="add-buttons" to="/add/publication">
                        PUBLICATION
                    </Link>

                </div>

            </div>

            <Footer />

        </div>


    )

}