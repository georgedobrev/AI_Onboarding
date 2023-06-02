import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { TextField, Button } from '@mui/material';
import { GoogleLogin } from '@react-oauth/google';
import logoImage from '../../assets/blankfactor-logo.jpg';
import PasswordField from './PasswordField';
import './Signup.css';

const Signup: React.FC = () => {
  const navigate = useNavigate();
  const [isSignupSuccess, setSignupSuccess] = useState(false);
  const [password, setPassword] = useState('');

  useEffect(() => {
    const storedSuccess = localStorage.getItem('signupSuccess');
    if (storedSuccess) {
      setSignupSuccess(true);
    }
  }, []);

  const handleGoogleSignupSuccess = () => {
    setSignupSuccess(true);
    localStorage.setItem('signupSuccess', 'true');
    navigate('/home');
  };

  const handleGoogleSignupError = () => {
    console.log('Login Failed');
  };

  const handleContinueClick = () => {
    // Do something with the password value
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
          <div className="signup-header-forms">
            <TextField
              id="outlined-basic"
              label="Email address"
              variant="outlined"
              className="email-field"
            />
            <PasswordField
              label="Password"
              passwordValue={password}
              onPasswordChange={(e) => setPassword(e.target.value)}
              className="password-field"
            />
            <Button
              variant="contained"
              className="signup-continue-btn"
              onClick={handleContinueClick}
            >
              Continue
            </Button>
          </div>
          <span className="signup-noaccount">
            Don't have an account?&nbsp;
            <Link to="/register" className="signup-hyperlink">
              Sign up
            </Link>
          </span>
        </div>
        <div className="signup-body">
          <span className="signup-or">OR</span>
          <div className="signup-button-container">
            {!isSignupSuccess && (
              <GoogleLogin
                onSuccess={handleGoogleSignupSuccess}
                onError={handleGoogleSignupError}
                text="signup_with"
                size="large"
              />
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Signup;
