import React from 'react';
import Navbar from '../Navbar/Navbar.tsx';
import Chats from '../Chats/Chats.tsx';
import Messages from '../Messages/Messages.tsx';
import FileUploader from '../Messages/FileUploader.tsx';
import './Home.css';
import blankfactorLogo from '../../assets/blankfactor-logo.jpg';

const Home: React.FC = () => {
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
          <Chats />
          <Messages />
        </div>
      </div>
    </>
  );
};
export default Home;
