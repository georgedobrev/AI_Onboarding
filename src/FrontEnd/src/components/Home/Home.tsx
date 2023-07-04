import React, { useState, useEffect } from 'react';
import { Outlet, useNavigate, useParams } from 'react-router-dom';
import Navbar from '../Navbar/Navbar.tsx';
import Chats from '../Chats/Chats.tsx';
import blankfactorLogo from '../../assets/blankfactor-logo.jpg';
import './Home.css';

const Home: React.FC = () => {
  const [isMobileView, setIsMobileView] = useState(false);
  const navigate = useNavigate();
  const { id } = useParams();

  useEffect(() => {
    const handleResize = () => {
      setIsMobileView(window.innerWidth < 768);
    };

    if (!id) {
      localStorage.removeItem('conversationId');
    }

    handleResize();
    window.addEventListener('resize', handleResize);

    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, []);

  const handleConversationClick = (id) => {
    if (id) {
      localStorage.setItem('conversationId', id);
      navigate(`/home/${id}`);
    }
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
