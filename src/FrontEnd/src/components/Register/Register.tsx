import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Button, IconButton, InputAdornment, TextField } from '@mui/material';
import { Visibility, VisibilityOff } from '@mui/icons-material';
import { useForm } from 'react-hook-form';
import './Register.css';
import logoImage from '../../assets/blankfactor-logo.jpg';
import { FormValues } from './types.ts';
import { authService } from '../../services/authService.ts';
import { toast } from 'react-toastify';

const Register: React.FC = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    getValues,
  } = useForm<FormValues>();

  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const onSubmit = async (data: FormValues) => {
    const { confirmPassword, ...formData } = data;
    try {
      await authService.register(formData);
      navigate('/signup', { state: { formData: formData } });
      toast.success('Register successfully', {
        position: 'top-right',
        autoClose: 3000,
        hideProgressBar: true,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: 'dark',
      });
    } catch (error) {
      console.error(error);
    }
  };

  const handleTogglePasswordVisibility = () => {
    setShowPassword((prevShowPassword) => !prevShowPassword);
  };

  const handleToggleConfirmPasswordVisibility = () => {
    setShowConfirmPassword((prevShowConfirmPassword) => !prevShowConfirmPassword);
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
          <div>
            <form onSubmit={handleSubmit(onSubmit)} className="register-signup-header-forms">
              <TextField
                id="first-name"
                label="First Name"
                variant="outlined"
                className="register-name-field"
                {...register('firstName', { required: 'First Name is required' })}
                error={!!errors.firstName}
                helperText={errors.firstName?.message}
              />
              <TextField
                id="last-name"
                label="Last Name"
                variant="outlined"
                className="register-name-field"
                {...register('lastName', { required: 'Last Name is required' })}
                error={!!errors.lastName}
                helperText={errors.lastName?.message}
              />
              <TextField
                id="email"
                label="Email address"
                variant="outlined"
                className="register-email-field"
                {...register('email', {
                  required: 'Email address is required',
                  pattern: {
                    value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                    message: 'Invalid email address',
                  },
                })}
                error={!!errors.email}
                helperText={errors.email?.message}
              />
              <TextField
                label="Password"
                type={showPassword ? 'text' : 'password'}
                {...register('password', {
                  required: 'Password is required',
                  minLength: {
                    value: 8,
                    message: 'Password must be at least 8 characters long',
                  },
                })}
                error={!!errors.password}
                helperText={errors.password?.message}
                className="register-password-field"
                InputProps={{
                  endAdornment: (
                    <InputAdornment position="end">
                      <IconButton onClick={handleTogglePasswordVisibility} edge="end">
                        {showPassword ? <Visibility /> : <VisibilityOff />}
                      </IconButton>
                    </InputAdornment>
                  ),
                }}
              />
              <TextField
                label="Confirm Password"
                type={showConfirmPassword ? 'text' : 'password'}
                {...register('confirmPassword', {
                  required: 'Confirm Password is required',
                  validate: (value) => value === getValues('password') || 'Passwords do not match',
                })}
                error={!!errors.confirmPassword}
                helperText={errors.confirmPassword?.message}
                className="confirm-register-password-field"
                InputProps={{
                  endAdornment: (
                    <InputAdornment position="end">
                      <IconButton onClick={handleToggleConfirmPasswordVisibility} edge="end">
                        {showConfirmPassword ? <Visibility /> : <VisibilityOff />}
                      </IconButton>
                    </InputAdornment>
                  ),
                }}
              />
              <Button type="submit" variant="contained" className="register-signup-continue-btn">
                Continue
              </Button>
            </form>
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
