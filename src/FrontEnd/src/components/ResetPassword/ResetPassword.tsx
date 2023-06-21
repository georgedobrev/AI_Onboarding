import React, { useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { TextField, Button } from '@mui/material';
import logoImage from '../../assets/blankfactor-logo.jpg';
import './ResetPassword.css';

const ResetPassword: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const [email, setEmail] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');

  const handleContinueClick = () => {
    console.log(`Password reset email sent to ${email}`);
    navigate('/account/reset-password/success');
  };

  return (
    <div className="reset-password-container">
      <div className="reset-password-overlay">
        <div className="reset-password-header">
          <div className="reset-password-header-container">
            <img src={logoImage} alt="blankfactor" className="reset-password-logo" />
            <h1 className="reset-password-welcome-h1">Reset Password</h1>
          </div>
          {location.pathname === '/account/reset-password/success' ? (
            <div className="reset-password-form">
              <TextField
                id="new-password"
                label="New Password"
                variant="outlined"
                className="new-password-field"
                type="password"
                value={newPassword}
                onChange={(e) => setNewPassword(e.target.value)}
              />
              <TextField
                id="confirm-password"
                label="Confirm Password"
                variant="outlined"
                className="confirm-password-field"
                type="password"
                value={confirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
              />
              <Button
                variant="contained"
                className="reset-password-continue-btn"
                onClick={handleContinueClick}
              >
                Reset Password
              </Button>
            </div>
          ) : (
            <div className="reset-password-form">
              <TextField
                id="email"
                label="Enter your email address"
                variant="outlined"
                className="email-field"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
              <Button
                variant="contained"
                className="reset-password-continue-btn"
                onClick={handleContinueClick}
              >
                Continue
              </Button>
            </div>
          )}
          <span className="reset-password-login">
            Remember your password?&nbsp;
            <Link to="/signup" className="reset-password-hyperlink">
              Log in
            </Link>
          </span>
        </div>
      </div>
    </div>
  );
};

export default ResetPassword;
