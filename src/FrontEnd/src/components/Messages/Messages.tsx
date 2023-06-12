import React from 'react';
import './Messages.css';

const Messages: React.FC = () => {
  return (
    <div className="messages-container">
      <div className="messages-header">
        <h2 className="messages-title">Messages</h2>
        <div className="horizontal-messagesline"></div>
      </div>
      <div className="messages-content-container">
        <div className="messages-content"></div>
      </div>
      <div className="vertical-messagesline"></div>;
    </div>
  );
};

export default Messages;
