import React, { useEffect, useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { TextField, Button, InputAdornment, IconButton } from '@mui/material';
import { CredentialResponse, GoogleLogin } from '@react-oauth/google';
import { Visibility, VisibilityOff } from '@mui/icons-material';
import logoImage from '../../assets/blankfactor-logo.jpg';
import { FormValues, ExtendSessionFormValues } from './types.ts';
import { authService } from '../../services/authService.ts';
import { toast } from 'react-toastify';
import config from '../../config.json';
import 'react-toastify/dist/ReactToastify.css';
import './Signup.css';
import { successNotification } from '../Notifications/Notifications.tsx';
import jwt_decode from 'jwt-decode';

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

  const handleGoogleSignupSuccess = async (credentialResponse: CredentialResponse) => {
    const response = await authService.googleLogin(credentialResponse.credential);
    await handleSuccessfulLogin(response);
    successNotification('Google login successful');

    const accessToken = localStorage.getItem('accessToken');
    const tokenPayload = jwt_decode(accessToken);
    const userRole = tokenPayload[config.JWTUserRole];
    const fullName = tokenPayload[config.Name];
    localStorage.setItem('fullName', fullName);
    localStorage.setItem('userRole', userRole);
    localStorage.removeItem('conversationId');
    return navigate('/home');
  };

  const handleGoogleSignupError = () => {
    console.error('Login Failed');
  };

  const handleSessionExtensionPrompt = async () => {
    if (!localStorage.getItem('refreshToken')) {
      localStorage.removeItem('accessToken');
      return navigate('/signup');
    }
    const confirmExtend = window.confirm('Do you want to extend your session?');
    if (confirmExtend) {
      await handleExtendSession();
    } else {
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');
      return navigate('/signup');
    }
  };

  const calculateRemainingTime = (token: string) => {
    const tokenPayload = jwt_decode(token);
    const expirationTime = tokenPayload.exp * 1000;
    const currentTime = new Date().getTime();
    const remainingTime = expirationTime - currentTime;
    return remainingTime;
  };

  const handleContinueClick = async () => {
    try {
      const response = await authService.login(formData);
      if (response) {
        await handleSuccessfulLogin(response);
        localStorage.removeItem('conversationId');
        navigate('/home');
        successNotification('Login Successful');
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
      const accessToken = response.headers.get('access-token');
      const refreshToken = response.headers.get('refresh-token');
      const remainingTime = calculateRemainingTime(refreshToken);
      const tokenPayload = jwt_decode(accessToken);
      const fullName = tokenPayload[config.Name];
      localStorage.setItem('fullName', fullName);

      setTimeout(() => {
        localStorage.removeItem('refreshToken');
        navigate('/signup');
      }, remainingTime);

      if (accessToken === null || refreshToken === null) {
        throw new Error('Access or refresh token not found');
      } else {
        const remainingTime = calculateRemainingTime(accessToken);
        extendSession(remainingTime);
        localStorage.removeItem('conversationId');
        navigate('/home');
      }
    } catch (error) {
      console.error(error);
    }
  };

  const handleExtendSession = async () => {
    const refreshToken = localStorage.getItem('refreshToken') || '';
    const accessToken = localStorage.getItem('accessToken') || '';

    const formData: ExtendSessionFormValues = {
      token: accessToken,
      refreshToken: refreshToken,
    };
    const response = await authService.extendSession(formData);

    if (response) {
      const newAccessToken = response.headers.get('access-token');
      localStorage.setItem('accessToken', newAccessToken);
      const remainingTime = calculateRemainingTime(newAccessToken);
      extendSession(remainingTime);
    } else {
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');
      return navigate('/signup');
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
            <span className="forgot-password">
              <Link to="/account/change-password" className="forgot-password-hyperlink">
                Forgot Password?
              </Link>
            </span>
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
