import React from 'react';
import Navbar from '../Navbar/Navbar.tsx';
import Chats from '../Chats/Chats.tsx';
import Messages from '../Messages/Messages.tsx';
import './Home.css';

const Home: React.FC = () => {
  return (
    <>
      <Navbar />
      <div className="content-container">
        <div className="header-container">
          <h1 className="header-title">Chat Bot - Blankfactor</h1>
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
