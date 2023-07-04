import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Divider, Drawer, IconButton, List, ListItem, ListItemIcon } from '@mui/material';
import { Menu as MenuIcon } from '@mui/icons-material';
import HomeIcon from '@mui/icons-material/Home';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import FileUploadIcon from '@mui/icons-material/FileUpload';
import Chats from '../Chats/Chats.tsx';
import './Navbar.css';

const Navbar: React.FC = () => {
  const [userRole, setUserRole] = useState<string | null>(null);
  const [open, setOpen] = useState(false);
  const [isMobileView, setIsMobileView] = useState(false);
  const [windowWidth, setWindowWidth] = useState(window.innerWidth);
  const navigate = useNavigate();
  const mobileViewNum = 768;
  const roles = {
    Administrator: 'Administrator',
    Employee: 'Employee',
  };

  useEffect(() => {
    const storedUserRole = localStorage.getItem('userRole');
    setUserRole(storedUserRole);

    const handleResize = () => {
      setWindowWidth(window.innerWidth);
      setIsMobileView(window.innerWidth < mobileViewNum);
    };

    setWindowWidth(window.innerWidth);
    setIsMobileView(window.innerWidth < mobileViewNum);

    window.addEventListener('resize', handleResize);

    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, []);

  const handleUploadClick = () => {
    navigate('/upload');
  };

  const handleHomeClick = () => {
    localStorage.removeItem('conversationId');
    navigate('/home');
  };

  const handleAccountClick = () => {
    navigate('/account');
  };

  const handleDrawerOpen = () => {
    setOpen(true);
  };

  const handleDrawerClose = () => {
    setOpen(false);
  };

  return (
    <div className="navbar">
      <div className="nav-container">
        {isMobileView ? (
          <IconButton
            aria-label="open drawer"
            edge="start"
            onClick={handleDrawerOpen}
            className="menu-button"
          >
            <MenuIcon />
          </IconButton>
        ) : (
          <List className="nav-list">
            <ListItem button onClick={handleHomeClick}>
              <ListItemIcon>
                <HomeIcon />
              </ListItemIcon>
            </ListItem>
            {userRole === roles.Administrator && (
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
        )}
        {isMobileView && windowWidth <= mobileViewNum && (
          <Drawer anchor="left" open={open} onClose={handleDrawerClose} className="drawer">
            <div className="drawer-list">
              <List>
                <ListItem button onClick={handleHomeClick}>
                  <ListItemIcon>
                    <HomeIcon />
                  </ListItemIcon>
                </ListItem>
                {userRole === roles.Administrator && (
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
              <Chats />
            </div>
            <Divider />
          </Drawer>
        )}
        <div className="navbar-line"></div>
      </div>
    </div>
  );
};

export default Navbar;
