import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { TextField } from '@mui/material';
import PasswordField from '../SignIn/PasswordField';
import Button from '@mui/material/Button';
import './Register.css';
import logoImage from '../../assets/blankfactor-logo.jpg';

const Register: React.FC = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');

  const handleRegister = () => {
    if (password !== confirmPassword) {
      setError('Passwords do not match');
      return;
    }
    setError('');
    // Perform registration logic here
    // You can validate the form fields and handle registration submission
    // For simplicity, this example does not include the actual registration logic
    console.log('Registering user...');
  };

  return (
    <div className="signup-container">
      <div className="signup-overlay">
        <div className="signup-header">
          <div className="signup-header-container">
            <img src={logoImage} alt="blankfactor" className="google-signup-logo" />
            <h1 className="signup-welcome-h1">
              Welcome to <br />
            </h1>
            <span className="signup-welcome-span">Blankfactor ChatBot!</span>
          </div>
          <div className="register-signup-header-forms">
            <TextField
              id="first-name"
              label="First Name"
              variant="outlined"
              className="register-name-field"
              value={firstName}
              onChange={(e) => setFirstName(e.target.value)}
            />
            <TextField
              id="last-name"
              label="Last Name"
              variant="outlined"
              className="register-name-field"
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
            />
            <TextField
              id="email"
              label="Email address"
              variant="outlined"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className="register-email-field"
            />
            <PasswordField
              label="Password"
              passwordValue={password}
              onPasswordChange={(e) => setPassword(e.target.value)}
              className="register-password-field"
            />
            <PasswordField
              label="Confirm Password"
              passwordValue={confirmPassword}
              onPasswordChange={(e) => setConfirmPassword(e.target.value)}
              className="confirm-register-password-field"
            />
            <Button
              variant="contained"
              className="register-signup-continue-btn"
              onClick={handleRegister}
            >
              Continue
            </Button>
            {error && <div className="password-error">{error}</div>}
          </div>
          <span className="register-signin-account">
            Already have an account?&nbsp;
            <Link to="/signup" className="signup-hyperlink">
              Sign in
            </Link>
          </span>
        </div>
      </div>
    </div>
  );
};

export default Register;
