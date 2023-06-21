import React, { useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { TextField, Button } from '@mui/material';
import logoImage from '../../assets/blankfactor-logo.jpg';
import { authService } from '../../services/authService.ts';
import './ResetPassword.css';
import { toast } from 'react-toastify';

const ResetPassword: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const [email, setEmail] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');

  const handleContinueClick = async () => {
    await authService.forgotPassword({ email });
    toast.success('Check your email for link', {
      position: 'top-right',
      autoClose: 1000,
      hideProgressBar: true,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: 'dark',
    });
  };

  const handleContinueClickReset = async () => {
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());

    const validateTokenData = {
      token: params.token,
      email: params.email,
    };

    try {
      await authService.validateResetToken(validateTokenData);

      toast.success('Token validated', {
        position: 'top-right',
        autoClose: 1000,
        hideProgressBar: true,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: 'dark',
      });

      // Rest of your code
      if (newPassword !== confirmPassword) {
        toast.error('Passwords do not match', {
          position: 'top-right',
          autoClose: 1000,
          hideProgressBar: true,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          theme: 'dark',
        });
        return;
      }

      const resetPasswordData = {
        email: params.email,
        token: params.token,
        newPassword: newPassword,
        confirmPassword: confirmPassword,
      };

      await authService.resetPassword(resetPasswordData);
      toast.success('Password reset', {
        position: 'top-right',
        autoClose: 1000,
        hideProgressBar: true,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: 'dark',
      });
      navigate('/signup');
    } catch (error) {
      console.error(error);
      toast.error('Invalid token', {
        position: 'top-right',
        autoClose: 1000,
        hideProgressBar: true,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: 'dark',
      });
      navigate('/signup');
      return;
    }
  };

  return (
    <div className="reset-password-container">
      <div className="reset-password-overlay">
        <div className="reset-password-header">
          <div className="reset-password-header-container">
            <img src={logoImage} alt="blankfactor" className="reset-password-logo" />
            <h1 className="reset-password-welcome-h1">Reset Password</h1>
          </div>
          {location.pathname === '/account/reset-password' ? (
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
                onClick={handleContinueClickReset}
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
