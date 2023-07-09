import React from 'react';
import { useNavigate } from 'react-router-dom';
import Button from '@mui/material/Button';
import LogoutIcon from '@mui/icons-material/Logout';
import Navbar from '../Navbar/Navbar.tsx';
import blankfactorLogo from '../../assets/blankfactor-logo.jpg';
import './Account.css';
const Account: React.FC = () => {
  const navigate = useNavigate();
  const handleSignOut = () => {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('userRole');
    localStorage.removeItem('fullName');

    navigate('/signup');
  };
  return (
    <>
      <Navbar />
      <div className="content-container">
        <div className="header-container">
          <div className="header-logo-title">
            <img src={blankfactorLogo} alt="Blankfactor Logo" className="header-logo" />
            <h1 className="header-title">Chat Bot - Blankfactor</h1>
          </div>
          <div className="header-line"></div>
        </div>
        <div className="main-container">
          <div className="account-container">
            <Button variant="contained" onClick={handleSignOut} className="logout-button">
              <LogoutIcon className="logout-icon"/>
              Sign Out
            </Button>
          </div>
        </div>
      </div>
    </>
  );
};

export default Account;
