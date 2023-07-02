import React from 'react';
import '../../Body.css';
import './AddPublisher.css';
import Header from '../../../Components/Header/Header';
import Footer from '../../../Components/Footer/Footer';

export default function AddPublisher (){

    return(

        <div>

            <Header />

            <div className='body add-publisher'>

                <div className='add-publisher-content'>

                    <p>Add new publisher</p>

                    <input className='add-publisher-inputs' type="text" placeholder='Name' />
                    <input className='add-publisher-inputs' type="number" placeholder='Contact Number' />
                        
                    <div className='add-publisher-buttons-container edit-buttons'>
                        <button className='add-publisher-buttons'>Edit</button>
                        <button className='add-publisher-buttons'>Save Changes</button>
                        <button className='add-publisher-buttons'>Add</button>
                    </div>

                </div>

            </div>

            <Footer />

        </div>

    )

}