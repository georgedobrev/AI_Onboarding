import React, { useState } from 'react';
import { TextField, InputAdornment, IconButton } from '@mui/material';
import { Visibility, VisibilityOff } from '@mui/icons-material';

interface PasswordFieldProps {
  label: string;
  passwordValue: string;
  onPasswordChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  className: string;
}

const PasswordField: React.FC<PasswordFieldProps> = ({
  label,
  passwordValue,
  onPasswordChange,
  className,
}) => {
  const [showPassword, setShowPassword] = useState(false);

  const handleClickShowPassword = () => {
    setShowPassword(!showPassword);
  };

  const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
  };

  return (
    <TextField
      id="outlined-password-input"
      label={label}
      type={showPassword ? 'text' : 'password'}
      autoComplete="new-password"
      className={className}
      value={passwordValue}
      onChange={onPasswordChange}
      InputProps={{
        endAdornment: (
          <InputAdornment position="end">
            <IconButton
              aria-label="toggle password visibility"
              onClick={handleClickShowPassword}
              onMouseDown={handleMouseDownPassword}
              edge="end"
            >
              {showPassword ? <Visibility /> : <VisibilityOff />}
            </IconButton>
          </InputAdornment>
        ),
      }}
    />
  );
};

export default PasswordField;
