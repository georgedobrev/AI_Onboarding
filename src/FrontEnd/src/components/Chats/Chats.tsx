import React, { useEffect, useState } from 'react';
import MessageIcon from '@mui/icons-material/Message';
import './Chats.css';
import { authService } from '../../services/authService.ts';
import { useLocation, useNavigate } from 'react-router-dom';

const Chats: React.FC = ({ onConversationClick }) => {
  const location = useLocation();
  const [selectedConversation, setSelectedConversation] = useState(null);
  const navigate = useNavigate();
  const [allConversations, setAllConversations] = useState([]);

  const loadAllChatMessages = async () => {
    const conversations = await authService.AIGetAllConversations();
    setAllConversations(conversations.data.conversations);
  };

  useEffect(() => {
    loadAllChatMessages();
  }, [selectedConversation, loadAllChatMessages]);

  const handleConversationClick = async (conversation) => {
    onConversationClick(conversation.id);
  };

  return (
    <div className="chat-container">
      <div className="chat-header">
        <h2 className="chats-title">Chats</h2>
        <div className="horizontal-chatline"></div>
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
      <div className="vertical-chatline-left"></div>
      <div className="vertical-chatline-right"></div>
      <div className="conversation-list"></div>
    </div>
  );
};

export default Chats;
