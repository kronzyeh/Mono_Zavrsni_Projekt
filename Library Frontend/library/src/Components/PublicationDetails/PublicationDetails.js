import React from 'react';
import Footer from '../Footer/Footer';
import './PublicationDetails.css';
import Header from '../Header/Header';

export default function PublicationDetails(){

    return(

        <div>

            <Header/>

            <div className='publication-details'>

                <ul>
                    <li>detail 1</li>
                    <li>detail 2</li>
                    <li>detail 3</li>
                </ul>

            </div>

            <Footer />

        </div>

    )

}