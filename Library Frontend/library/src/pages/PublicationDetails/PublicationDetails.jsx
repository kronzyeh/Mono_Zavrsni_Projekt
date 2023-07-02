import React from 'react';
import Header from '../../Components/Header/Header';
import Footer from '../../Components/Footer/Footer';
import { Link } from 'react-router-dom';
import './PublicationDetails.css';
import '../Body.css';

export default function PublicationDetails (){

    return(

        <div>

            <Header />

            <div className='body publication-details'>

                <div className='publication-details-content'>

                <p>HERE SHOULD BE THE DETAILS ABOUT BOOK/PUBLICATION, AND FROM HERE YOU HAVE AN OPTION TO PRESS AUTHOR NAME AND GET TAKEN TO AUTHOR DETAILS/INFO</p>

                    <Link to='/author'>
                        <p>author - publication</p>
                        <p>author - publication</p>
                        <p>author - publication</p>
                        <p>author - publication</p>
                    </Link>

                    <div className='publication-details-buttons-container'>
                        <Link to={'/publication/edit/id'}>
                            <button className='publication-details-buttons'>EDIT</button>
                        </Link>

                        <button className='publication-details-buttons'>REMOVE</button>
                    </div>

                </div>

            </div>

            <Footer />

        </div>

    )

}