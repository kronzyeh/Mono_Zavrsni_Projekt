import React from 'react';
import './App.css';
import { Routes, Route, Navigate } from 'react-router-dom';
import RegisterPage from './pages/RegisterPage';
import LoginPage from './pages/LoginPage';
import LandingPage from './pages/LandingPage';
import MissingPage from './pages/MissingPage';
import MyReservations from './pages/MyReservations/MyReservations';
import ListUsersPage from './pages/ListUsersPage';
import EditUser from './Components/UserList/EditUser';
import PublicationDetails from './pages/PublicationDetails/PublicationDetails';
import AuthorDetails from './pages/AuthorDetails/AuthorDetails';
import Add from './pages/Add/Add';
import AllReservations from './pages/AllReservations/AllReservations';
import AddAuthor from './pages/Add/AddAuthor/AddAuthor';
import AddGenre from './pages/Add/AddGenre/AddGenre';
import AddPublication from './pages/Add/AddPublication/AddPublication';
import AddPublisher from './pages/Add/AddPublisher/AddPublisher';
import HomePage from './pages/HomePage';
import EditPublication from './pages/EditPublication/EditPublication';
import AddNewReservation from './pages/AddNewReservation/AddNewReservation';
import AddPublicationPage from './pages/AddPublicationPage';
import GetAuthorByIdPage from './pages/GetAuthorByIdPage';
import GetPublicationByIdPage from './pages/GetPublicationById';


// HOC to check if the user is authenticated and has the admin role
const ProtectedAdminRoute = ({ element: Component, ...props }) => {
  const isAuthenticated = localStorage.getItem('token');
  const userRole = localStorage.getItem('role');

  if (isAuthenticated && userRole === 'Admin') {
    return <Component {...props} />;
  } else {
    // Redirect to a different page if not authenticated or not an admin
    return <Navigate to="/userhome" />;
  }
};


function App() {
  return (
    <div>
      <div>
        <Routes>
          <Route path="*" element={<MissingPage />} />
          {/* //<Route path="users/:/*" element={<MissingPage />} /> */}
          <Route path="/" element={<LandingPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/myreservations" element={<MyReservations />} />
          <Route path="/publication/id" element={<PublicationDetails />} /> 
          <Route path="/home/users" element={<ListUsersPage />} />
          <Route path="/users/:id" element={<EditUser />} />
          
          <Route path="/publication" element={<PublicationDetails />} />
          <Route path="/author" element={<AuthorDetails />} />
          <Route path="/add" element={<Add />} />
          <Route path="/home/allreservations" element={<AllReservations />} />
          <Route path="/add/author" element={<AddAuthor />} />
          <Route path="/add/genre" element={<AddGenre />} />
          <Route path="/add/publication" element={<AddPublication />} />
          <Route path="/add/publisher" element={<AddPublisher />} />
          <Route path="/add/reservation" element = {<AddNewReservation/>} />
          <Route path="/publication/edit/id" element={<EditPublication />} />
          <Route path="/home" element={<HomePage />} />
          <Route path="/add/publication" element={<AddPublicationPage />} />
          <Route path="/author/:id" element={<GetAuthorByIdPage/>} />
          <Route path="/publication/:id" element={<GetPublicationByIdPage/>} />
        </Routes>
      </div>
    </div>

  );
}

export default App;
