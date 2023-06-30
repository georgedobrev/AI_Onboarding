import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { authService } from '../../services/authService.ts';
import { Add as AddIcon, Message as MessageIcon } from '@mui/icons-material';
import './Chats.css';

const Chats: React.FC = ({ onConversationClick }) => {
  const navigate = useNavigate();
  const [allConversations, setAllConversations] = useState([]);

  const loadAllChatMessages = async () => {
    const conversations = await authService.AIGetAllConversations();
    setAllConversations(conversations.data.conversations.reverse());
  };

  useEffect(() => {
    loadAllChatMessages();
  }, [loadAllChatMessages]);

  const handleConversationClick = async (conversation) => {
    onConversationClick(conversation.id);
  };

  const handleAddIconClick = () => {
    localStorage.removeItem('conversationId');
    navigate('/home');
    onConversationClick(null);
  };

  return (
    <div className="chat-container">
      <div className="chat-header">
        <h2 className="chats-title">Chats</h2>
        <div className="add-chat-container" onClick={handleAddIconClick}>
          <AddIcon className="add-chat-icon" />
          <p>New Chat</p>
        </div>
        {allConversations.map((conversation, index) => (
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
      <div className="conversation-list"></div>
    </div>
  );
};

export default Chats;
