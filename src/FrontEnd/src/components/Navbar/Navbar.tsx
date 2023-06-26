import React, { useState, useEffect } from 'react';
import { List, ListItem, ListItemIcon, IconButton, Drawer, Divider } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import Chats from '../Chats/Chats.tsx';
import { Menu as MenuIcon } from '@mui/icons-material';
import HomeIcon from '@mui/icons-material/Home';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import FileUploadIcon from '@mui/icons-material/FileUpload';
import './Navbar.css';

const Navbar: React.FC = () => {
  const [userRole, setUserRole] = useState<string | null>(null);
  const [open, setOpen] = useState(false);
  const [isMobileView, setIsMobileView] = useState(false);
  const [windowWidth, setWindowWidth] = useState(window.innerWidth);
  const navigate = useNavigate();

  useEffect(() => {
    const storedUserRole = localStorage.getItem('userRole');
    setUserRole(storedUserRole);

    const handleResize = () => {
      setWindowWidth(window.innerWidth);
      setIsMobileView(window.innerWidth < 768);
    };

    setWindowWidth(window.innerWidth);
    setIsMobileView(window.innerWidth < 768);

    window.addEventListener('resize', handleResize);

    return () => {
      window.removeEventListener('resize', handleResize);
    };
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
        )}
        {isMobileView && windowWidth <= 768 && (
          <Drawer anchor="left" open={open} onClose={handleDrawerClose} className="drawer">
            <div className="drawer-list">
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
              <Chats /> {/* Moved Chats component inside the drawer-list */}
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
