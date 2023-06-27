import React from 'react';
import MessageIcon from '@mui/icons-material/Message';
import './Chats.css';

const Chats: React.FC = ({ conversations, onConversationClick }) => {
  const handleConversationClick = (conversation) => {
    onConversationClick(conversation);
    console.log(conversation.questionAnswers[0].question);
  };

  return (
    <div className="chat-container">
      <div className="chat-header">
        <h2 className="chats-title">Chats</h2>
        <div className="horizontal-chatline"></div>
        {conversations.map((conversation, index) => (
          <div
            className="conversation"
            key={index}
            onClick={() => handleConversationClick(conversation)}
          >
            <MessageIcon className="conversation-message-icon" />
            <span>{conversation.text}</span>
            <span>{conversation.questionAnswers[0].question}</span>
          </div>
        ))}
      </div>
      <div className="vertical-chatline-left"></div>
      <div className="vertical-chatline-right"></div>
      <div className="conversation-list"></div>
    </div>
  );
};

export default Chats;
