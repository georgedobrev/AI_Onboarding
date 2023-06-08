import React, { useEffect, useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { TextField, Button, InputAdornment, IconButton } from '@mui/material';
import { GoogleLogin } from '@react-oauth/google';
import logoImage from '../../assets/blankfactor-logo.jpg';
import './Signup.css';
import { Visibility, VisibilityOff } from '@mui/icons-material';
import config from '../../config.json';
import { FormValues } from './typesLogin.ts';

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

  const handleGoogleSignupSuccess = (credentialResponse: any) => {
    const { credential } = credentialResponse;
    console.log(credential);
    setSignupSuccess(true);
    localStorage.setItem('signupSuccess', 'true');
    navigate('/home');
  };

  const handleGoogleSignupError = () => {
    console.log('Login Failed');
  };

  const handleContinueClick = async () => {
    try {
      const postData = JSON.stringify(formData, null, 2);
      const url = `${config.baseUrl}${config.loginEndpoint}`;
      const response = await fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: postData,
      });

      if (response.ok) {
        const accessToken = response.headers.get('Access-Token');
        const refreshToken = response.headers.get('Refresh-Token');
        const expirationDate = new Date();
        expirationDate.setUTCDate(expirationDate.getUTCDate() + 5);
        document.cookie = `Access-Token=${accessToken}; path=/`;
        document.cookie = `Refresh-Token=${refreshToken}; expires=${expirationDate.toUTCString()}; path=/`;

        if (accessToken === null || refreshToken === null) {
          throw new Error('Access or refresh token not found');
        } else {
          const tokenParts = accessToken.split('.');
          const tokenPayload = JSON.parse(atob(tokenParts[1]));
          const expirationTime = tokenPayload.exp * 1000;
          const currentTime = new Date().getTime();
          const remainingTime = expirationTime - currentTime;

          setTimeout(() => {
            console.log('Token expired');
          }, remainingTime);

          const responseData = await response.text();
          console.log(responseData);
          navigate('/home');
        }
      } else {
        throw new Error('Request failed');
      }
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
              onChange={(e) => setFormData({ ...formData, email: e.target.value })}
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
              onChange={(e) => setFormData({ ...formData, password: e.target.value })}
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
