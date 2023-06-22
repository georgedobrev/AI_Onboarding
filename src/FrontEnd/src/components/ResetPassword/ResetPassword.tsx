import React, { useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { TextField, Button, InputAdornment, IconButton } from '@mui/material';
import logoImage from '../../assets/blankfactor-logo.jpg';
import { authService } from '../../services/authService.ts';
import './ResetPassword.css';
import { Visibility, VisibilityOff } from '@mui/icons-material';
import { errorNotifications, successNotification } from '../Notifications/Notifications.tsx';

const ResetPassword: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const [email, setEmail] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const handleTogglePasswordVisibility = () => {
    setShowPassword((prevShowPassword) => !prevShowPassword);
  };

  const handleToggleConfirmPasswordVisibility = () => {
    setShowConfirmPassword((prevShowConfirmPassword) => !prevShowConfirmPassword);
  };

  const handleContinueClick = async () => {
    await authService.forgotPassword({ email });
    successNotification('Check your email for link');
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

      if (newPassword !== confirmPassword) {
        errorNotifications('Passwords do not match');
        return;
      }

      const resetPasswordData = {
        email: params.email,
        token: params.token,
        newPassword: newPassword,
        confirmPassword: confirmPassword,
      };

      await authService.resetPassword(resetPasswordData);
      successNotification('Password reset');
      navigate('/signup');
    } catch (error) {
      console.error(error);
      errorNotifications('Invalid token');
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
                type={showPassword ? 'text' : 'password'}
                InputProps={{
                  endAdornment: (
                    <InputAdornment position="end">
                      <IconButton onClick={handleTogglePasswordVisibility} edge="end">
                        {showPassword ? <Visibility /> : <VisibilityOff />}
                      </IconButton>
                    </InputAdornment>
                  ),
                }}
                value={newPassword}
                onChange={(e) => setNewPassword(e.target.value)}
              />
              <TextField
                id="confirm-password"
                label="Confirm Password"
                variant="outlined"
                className="confirm-password-field"
                type={showConfirmPassword ? 'text' : 'password'}
                InputProps={{
                  endAdornment: (
                    <InputAdornment position="end">
                      <IconButton onClick={handleToggleConfirmPasswordVisibility} edge="end">
                        {showConfirmPassword ? <Visibility /> : <VisibilityOff />}
                      </IconButton>
                    </InputAdornment>
                  ),
                }}
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
