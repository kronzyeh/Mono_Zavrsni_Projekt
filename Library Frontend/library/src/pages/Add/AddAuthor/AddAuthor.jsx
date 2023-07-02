import React from 'react';
import '../../Body.css';
import NewAuthor from "../../../Components/AuthorDetails/NewAuthor";
import Header from '../../../Components/Header/Header';
import Footer from '../../../Components/Footer/Footer';

export default function AddAuthor (){

    return(

        <div>
            <header>
            <Header />
            </header>
            <body>
                <NewAuthor/>
            </body>
            <footer>
            <Footer />
            </footer>

        </div>

    )

}