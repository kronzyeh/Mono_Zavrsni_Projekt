import React, { useEffect, useState } from 'react';
import Landing from '../Components/Landing/LandingButtons';
import Footer from '../Components/Footer/Footer';
import { useNavigate, useLocation } from 'react-router-dom';
import LandingButton from "../Components/Landing/LandingButtons";

export default function LandingPage() {
  const navigate = useNavigate();
  const token = localStorage.getItem('token');
  const location = useLocation();
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {debugger;
    if (token && location.pathname === '/') {debugger;
      setTimeout(() => {
        navigate('/home'); // Redirect to /home after a brief delay
      }, 50); // Adjust the delay duration as needed
    } else {
      setIsLoading(false);
    }
  }, [navigate, token, location]);

  return (
    <div>
      {isLoading ? (
        <div>Loading...</div> // Show a loading state while redirecting
      ) : (
        <>
          <body>
            <LandingButton/>
          </body>
        </>
      )}
    </div>
  );
}
