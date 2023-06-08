import React, { useEffect, useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { TextField, Button, InputAdornment, IconButton } from '@mui/material';
import { GoogleLogin, CredentialResponse } from '@react-oauth/google';
import logoImage from '../../assets/blankfactor-logo.jpg';
import './Signup.css';
import { Visibility, VisibilityOff } from '@mui/icons-material';
import { FormValues } from './types.ts';
import { authService } from '../../services/authService.ts';

const Signup: React.FC = () => {
  const location = useLocation();
  const formDataFromRegister = location.state?.formData || {};

  const navigate = useNavigate();
  const [isSignupSuccess, setSignupSuccess] = useState(false);
  const [showPassword, setShowPassword] = useState(false);

  const [formData, setFormData] = useState<FormValues>({
    email: formDataFromRegister.email || '',
    password: formDataFromRegister.password || '',
  });

  useEffect(() => {
    const storedSuccess = localStorage.getItem('signupSuccess');
    if (storedSuccess) {
      setSignupSuccess(true);
    }
  }, []);

  const handleGoogleSignupSuccess = (credentialResponse: CredentialResponse) => {
    setSignupSuccess(true);
    localStorage.setItem('signupSuccess', 'true');
    navigate('/home');
  };

  const handleGoogleSignupError = () => {
    console.log('Login Failed');
  };

  const handleContinueClick = async () => {
    try {
      const response = await authService.login(formData);
      navigate('/home', { state: { postData: response } });
    } catch (error) {
      console.error(error);
    }
  };

  const handleTogglePasswordVisibility = () => {
    setShowPassword((prevShowPassword) => !prevShowPassword);
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
              value={formData.email}
              onChange={(e) =>
                  setFormData((prevFormData) => ({
                    ...prevFormData,
                    email: e.target.value,
                  }))
              }
            />
            <TextField
              label="Password"
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
              className="password-field"
              value={formData.password}
              onChange={(e) =>
                  setFormData((prevFormData) => ({
                    ...prevFormData,
                    password: e.target.value,
                  }))
              }
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
                hosted_domain="blankfactor.com"
              />
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Signup;
