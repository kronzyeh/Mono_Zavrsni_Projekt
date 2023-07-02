import React from 'react';
import '../Body.css';
import './EditPublication.css';
import Header from '../../Components/Header/Header';
import Footer from '../../Components/Footer/Footer';

export default function EditPublication (){

    return(

        <div>

            <Header />

            <div className='body edit-publication'>

                <div className='edit-publication-content'>

                    
                    <input className='edit-publication-inputs' type="text" placeholder='Book Name' />
                    <input className='edit-publication-inputs' type="text" placeholder='Author Name' />
                    <input className='edit-publication-inputs' type="text" placeholder='Edition' />
                    <input className='edit-publication-inputs' type="text" placeholder='Type' />
                    <input className='edit-publication-inputs' type="text" placeholder='Genre' />
                    <input className='edit-publication-inputs' type="text" placeholder='Date Published' />
                    <input className='edit-publication-inputs' type="text" placeholder='Number Of Pages' />
                    

                    <div className='edit-buttons'>
                        <button className='edit-publication-buttons'>Edit</button>
                        <button className='edit-publication-buttons'>Save Changes</button>
                        <button className='edit-publication-buttons'>Remove Publication</button>
                    </div>


                </div>

           

            </div>

            <Footer />

        </div>

    )

}