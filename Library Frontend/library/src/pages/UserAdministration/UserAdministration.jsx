import React from 'react';
import Header from '../../Components/Header/Header';
import Footer from '../../Components/Footer/Footer';
import { Link } from 'react-router-dom';
import './UserAdministration.css';
import '../Body.css';

export default function UserAdministration (){

    return(

        <div>

            <Header />

            <div className='body user-administration'>

              

                <div className='user-administration-content'>

                    <div className='add-search'>
 
                        <div className='add-user'>
                            <Link to={'/register'}>
                                <p>add new user</p>
                            </Link>
                        </div>
                        
                        <div>
                            <input type="search" placeholder='search for a user'/> {/*add remove method that removes user when button is clicked */}
                            <button type='submit'>Submit</button>
                        </div>

                    </div>

                    <div className='user'>
                        <p>user administration</p>
                        <div className='user-options'>
                            <Link to={'/useradministration/edit/id'}>
                                <p>Edit</p>
                            </Link>
                            <button className='remove-user'> {/*add remove method that removes user when button is clicked */}
                                <p>Remove</p>
                            </button>
                        </div>
                    </div>
                    
                    <div className='user'>
                        <p>user administration</p>
                        <div className='user-options'>
                            <Link to={'/useradministration/edit/id'}>
                                <p>Edit</p>
                            </Link>
                            <button className='remove-user'> {/*add remove method that removes user when button is clicked */}
                                <p>Remove</p>
                            </button>
                        </div>
                    </div>

                    <div className='user'>
                        <p>user administration</p>
                        <div className='user-options'>
                            <Link to={'/useradministration/edit/id'}>
                                <p>Edit</p>
                            </Link>
                            <button className='remove-user'> {/*add remove method that removes user when button is clicked */}
                                <p>Remove</p>
                            </button>
                        </div>
                    </div>

                    <div className='user'>
                        <p>user administration</p>
                        <div className='user-options'>
                            <Link to={'/useradministration/edit/id'}>
                                <p>Edit</p>
                            </Link>
                            <button className='remove-user'> {/*add remove method that removes user when button is clicked */}
                                <p>Remove</p>
                            </button>
                        </div>
                    </div>
                </div>

            </div>

            <Footer />

        </div>

    )

}