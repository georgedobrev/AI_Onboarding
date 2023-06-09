import React, { useEffect, useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { TextField, Button, InputAdornment, IconButton } from '@mui/material';
import { CredentialResponse, GoogleLogin } from '@react-oauth/google';
import { Visibility, VisibilityOff } from '@mui/icons-material';
import logoImage from '../../assets/blankfactor-logo.jpg';
import './Signup.css';
import config from '../../config.json';
import { FormValues } from './typesLogin.ts';
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

  const deleteCookie = (name: string) => {
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
  };

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

  const handleSessionExtensionPrompt = async () => {
    const confirmExtend = window.confirm('Do you want to extend your session?');
    if (confirmExtend) {
      await handleExtendSession();
    } else {
      deleteCookie('Access-Token');
      deleteCookie('Refresh-Token');
      navigate('/signup');
    }
  };

  const calculateRemainingTime = (accessToken: string) => {
    const tokenParts = accessToken.split('.');
    const tokenPayload = JSON.parse(atob(tokenParts[1]));
    const expirationTime = tokenPayload.exp * 1000;
    const currentTime = new Date().getTime();
    const remainingTime = expirationTime - currentTime;
    return remainingTime;
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
        await handleSuccessfulLogin(response);
      } else {
        throw new Error('Request failed');
      }
    } catch (error) {
      console.error(error);
    }
  };

  const extendSession = (remainingTime: number) => {
    setTimeout(handleSessionExtensionPrompt, remainingTime);
  };

  const handleSuccessfulLogin = async (response: object) => {
    try {
      const accessToken = response.headers.get('Access-Token');
      const refreshToken = response.headers.get('Refresh-Token');
      const expirationDate = new Date();
      expirationDate.setUTCDate(expirationDate.getUTCDate() + 5);
      document.cookie = `Access-Token=${accessToken}; path=/`;
      document.cookie = `Refresh-Token=${refreshToken}; expires=${expirationDate.toUTCString()}; path=/`;

      if (accessToken === null || refreshToken === null) {
        throw new Error('Access or refresh token not found');
      } else {
        const remainingTime = calculateRemainingTime(accessToken);
        extendSession(remainingTime);
        navigate('/home');
      }
    } catch (error) {
      console.error(error);
    }
  };


  const handleExtendSession = async () => {
    const refreshToken = getCookie('Refresh-Token');
    const accessToken = getCookie('Access-Token');
    const url = `${config.baseUrl}${config.refreshTokenEndpoint}`;
    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ refreshToken: refreshToken, token: accessToken }),
    });

    if (response.ok) {
      const newAccessToken = response.headers.get('Access-Token');
      document.cookie = `Access-Token=${newAccessToken}; path=/`;
      const remainingTime = calculateRemainingTime(newAccessToken);
      extendSession(remainingTime);
    } else {
      deleteCookie('Access-Token');
      deleteCookie('Refresh-Token');
      navigate('/signup');
    }
  };

  function getCookie(name: string) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
  }

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
