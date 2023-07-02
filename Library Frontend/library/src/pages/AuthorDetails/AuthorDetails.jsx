import React from 'react';
import Header from '../../Components/Header/Header';
import Footer from '../../Components/Footer/Footer';
import './AuthorDetails.css';
import '../Body.css';

export default function AuthorDetails (){

    return(

        <div>

            <Header />

            <div className='body author-details'>

                <div className='author-details-content'>

                <p>HERE SHOULD BE THE DETAILS ABOUT THE AUTHOR</p>

                    <p>test</p>
                    <p>test</p>
                    <p>test</p>
                    <p>test</p>

                    <button>EDIT</button>
                    <button>SAVE CHANGES</button>
                    <button>REMOVE</button>
                </div>

            </div>

            <Footer />

        </div>

    )

}