import React, { useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { TextField, Button } from '@mui/material';
import { authService } from '../../services/authService.ts';
import { successNotification } from '../Notifications/Notifications.tsx';
import logoImage from '../../assets/blankfactor-logo.jpg';
import './EmailConfirmation.css';

const EmailConfirmation: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const email = new URLSearchParams(location.search).get('email');
  const token = new URLSearchParams(location.search).get('token');
  const [emailFieldValue] = useState<string>(email || '');

  const handleContinueClick = async () => {
    await authService.validateConfirmToken({ token, email: emailFieldValue });
    successNotification('Confirmed email successfully. Redirecting to signup page...');
    setTimeout(() => {
      navigate('/signup');
    }, 2000);
  };

  return (
      <div className="email-confirmation-container">
        <div className="email-confirmation-overlay">
          <div className="email-confirmation-header">
            <div className="email-confirmation-header-container">
              <img src={logoImage} alt="blankfactor" className="email-confirmation-logo" />
              <h1 className="email-confirmation-welcome-h1">Email Confirmation</h1>
            </div>
            <div className="email-confirmation-form">
              <TextField
                  label="Your email address"
                  variant="outlined"
                  className="email-field custom-textfield"
                  value={emailFieldValue}
                  disabled
              />
              <Button
                  variant="contained"
                  className="email-confirmation-continue-btn"
                  onClick={handleContinueClick}
              >
                Continue
              </Button>
            </div>
            <span className="email-confirmation-login">
            Already confirmed your email?&nbsp;
              <Link to="/signup" className="email-confirmation-hyperlink">
              Log in
            </Link>
          </span>
          </div>
        </div>
      </div>
  );
};

export default EmailConfirmation;