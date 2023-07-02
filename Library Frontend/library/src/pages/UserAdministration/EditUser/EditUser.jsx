import React from 'react';
import '../../Body.css';
import './EditUser.css';
import Header from '../../../Components/Header/Header';
import Footer from '../../../Components/Footer/Footer';

export default function EditUser (){

    return(

        <div>

            <Header />

            <div className='body edit-user'>

                <div className='edit-user-content'>
                    <p>EDIT USER</p>
                    
                    <label className='labels' for="fname">First name:</label>
                    <input className='edit-user-inputs' type="text" id="fname" name="fname" placeholder='Last Name' />

                    <label className='labels' for="lname">Last name:</label>
                    <input className='edit-user-inputs' type="text" id="lname" name="lname" placeholder='Last Name' />

                    <label className='labels' for="lname">Subscription Status:</label>
                    <input className='edit-user-inputs' type="text" id="subscription" name="subscription" placeholder='Subscription Status' />

                    <label className='labels' for="dateofbirth">Date of Birth:</label>
                    <input className='edit-user-inputs' type="date" id="dateofbirth" name="dateofbirth" placeholder='Date Of Birth' />

                    <label className='labels' for="phonenumber">Phone Number:</label>
                    <input className='edit-user-inputs' type="number" id='phonenumber' name='phonenumber' placeholder='Phone Number' />

                    <label className='labels' for="password">Password:</label>
                    <input className='edit-user-inputs' type="password" id='password' name='password' placeholder='Password' />

                    <div>
                        <button>Extend Subscription</button>
                        <button>Edit</button>
                        <button>Save</button>
                        <button>Delete User</button>
                    </div>

                </div>

            </div>

            <Footer />

        </div>

    )

}