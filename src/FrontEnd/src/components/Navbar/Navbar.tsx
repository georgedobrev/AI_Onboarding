import React, { useState, useEffect } from 'react';
import { List, ListItem, ListItemIcon } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import HomeIcon from '@mui/icons-material/Home';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import FileUploadIcon from '@mui/icons-material/FileUpload';
import './Navbar.css';

const Navbar: React.FC = () => {
  const [userRole, setUserRole] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const storedUserRole = localStorage.getItem('userRole');
    setUserRole(storedUserRole);
  }, []);

  const handleUploadClick = () => {
    navigate('/upload');
  };

  const handleHomeClick = () => {
    navigate('/home');
  };

  const handleAccountClick = () => {
    navigate('/account');
  };

  return (
    <div className="navbar">
      <div className="nav-container">
        <List>
          <ListItem button onClick={handleHomeClick}>
            <ListItemIcon>
              <HomeIcon />
            </ListItemIcon>
          </ListItem>
          {userRole === 'Administrator' && (
            <ListItem button onClick={handleUploadClick}>
              <ListItemIcon>
                <FileUploadIcon />
              </ListItemIcon>
            </ListItem>
          )}
          <ListItem button onClick={handleAccountClick}>
            <ListItemIcon>
              <AccountCircleIcon />
            </ListItemIcon>
          </ListItem>
        </List>
        <div className="navbar-line"></div>
      </div>
    </div>
  );
};

export default Navbar;
