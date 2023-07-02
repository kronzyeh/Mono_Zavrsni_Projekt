import React from 'react';
import '../../Body.css';
import './AddGenre.css';
import Header from '../../../Components/Header/Header';
import Footer from '../../../Components/Footer/Footer';

export default function AddGenre (){

    return(

        <div>

            <Header />

            <div className='body add-genre'>

                <div className='add-genre-content'>
                    <p>Add new genre</p>

                    <input className='add-genre-inputs' type="text" placeholder='Title' />
                        
                    <div className='add-genre-buttons-container edit-buttons'>
                        <button className='add-genre-buttons'>Edit</button>
                        <button className='add-genre-buttons'>Save Changes</button>
                        <button className='add-genre-buttons'>Add</button>
                    </div>

                </div>

            </div>

            <Footer />

        </div>

    )

}