import React from 'react';
import './Chats.css';

const Chats: React.FC = () => {
  return (
    <div className="chat-container">
      <div className="chat-header">
        <h2 className="chats-title">Chats</h2>
        <div className="horizontal-chatline"></div>
      </div>
      <div className="vertical-chatline-left"></div>
      <div className="vertical-chatline-right"></div>
    </div>
  );
};
export default Chats;
