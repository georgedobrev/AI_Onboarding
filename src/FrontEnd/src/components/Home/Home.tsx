import React, { useState, useEffect } from 'react';
import { Outlet, useNavigate } from 'react-router-dom';
import Navbar from '../Navbar/Navbar.tsx';
import Chats from '../Chats/Chats.tsx';
import blankfactorLogo from '../../assets/blankfactor-logo.jpg';
import './Home.css';

const Home: React.FC = () => {
  const [isMobileView, setIsMobileView] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const handleResize = () => {
      setIsMobileView(window.innerWidth < 768);
    };

    handleResize(); // Initial check
    window.addEventListener('resize', handleResize);

    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, []);

  const handleConversationClick = (id) => {
    navigate(`/home/${id}`);
  };

  return (
    <>
      <Navbar />
      <div className="content-container">
        <div className="header-container">
          <div className="header-logo-title">
            <img src={blankfactorLogo} alt="Blankfactor Logo" className="header-logo" />
            <h1 className="header-title">Chat Bot - Blankfactor</h1>
          </div>
          <div className="header-line"></div>
        </div>
        <div className="main-container">
          {!isMobileView && <Chats onConversationClick={handleConversationClick} />}
          <Outlet />
        </div>
      </div>
    </>
  );
};

export default Home;
