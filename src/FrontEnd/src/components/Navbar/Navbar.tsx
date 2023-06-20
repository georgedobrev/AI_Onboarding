import React from 'react';
import { List, ListItem, ListItemIcon } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import HomeIcon from '@mui/icons-material/Home';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import './Navbar.css';

const userRole = localStorage.getItem('userRole');

const Navbar: React.FC = () => {
    const navigate = useNavigate();

    const handleAccountClick = () => {
        navigate('/account');
    };

    const handleHomeClick = () => {
        navigate('/home');
    }

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
                        <ListItem button onClick={handleAccountClick}>
                            <ListItemIcon>
                                <AccountCircleIcon />
                            </ListItemIcon>
                        </ListItem>
                    )}
                </List>
                <div className="navbar-line"></div>
            </div>
        </div>
    );
};

export default Navbar;
