import React from 'react';
import '../../Body.css';
import './AddPublication.css';
import Header from '../../../Components/Header/Header';
import Footer from '../../../Components/Footer/Footer';

export default function AddPublication (){

    return(

        <div>

            <Header />

            <div className='body add-publication'>

                <div className='add-publication-content'>
                <p>Add a new publication</p>

                <input className='add-publication-inputs' type="text" placeholder='Title' />
                <input className='add-publication-inputs' type="text" placeholder='Description' />
                <input className='add-publication-inputs' type="text" placeholder='Edition' />
                <input className='add-publication-inputs' type="text" placeholder='Date Published' />
                <input className='add-publication-inputs' type="text" placeholder='Quantity' />
                <input className='add-publication-inputs' type="text" placeholder='Number of Pages' />

                    
                <div className='add-publication-buttons-container edit-buttons'>
                    <button className='add-publication-buttons'>Edit</button>
                    <button className='add-publication-buttons'>Save Changes</button>
                    <button className='add-publication-buttons'>Add</button>
                </div>

            </div>

            </div>

            <Footer />

        </div>

    )

}