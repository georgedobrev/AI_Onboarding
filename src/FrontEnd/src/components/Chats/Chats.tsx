import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { atom } from 'jotai';
import {
  Add as AddIcon,
  Message as MessageIcon,
  DeleteForever as DeleteIcon,
  Check as CheckIcon,
  Close as CloseIcon,
} from '@mui/icons-material';
import { authService } from '../../services/authService.ts';
import './Chats.css';

interface ChatsProps {
  onConversationClick: (conversationId: number) => void;
  id: number;
  triggerLoadConversations: boolean;
}

interface Conversation {
  text: string;
  questionAnswers: string[];
  id: number;
}

export const loadAllChatMessages = async () => {
  const conversations = await authService.AIGetAllConversations();
  return conversations.data.conversations.reverse();
};

const Chats: React.FC<ChatsProps> = ({ onConversationClick, triggerLoadConversations }) => {
  const navigate = useNavigate();
  const allConversationsAtom = atom<Conversation[]>([]);
  const [allConversations, setAllConversations] = useState<Conversation[]>([]);
  const [selectedConversation, setSelectedConversation] = useState<Conversation | null>(null);
  const [clickedConversation, setClickedConversation] = useState<Conversation | null>(null);
  const [showDeleteIcon, setShowDeleteIcon] = useState<boolean>(true);

  useEffect(() => {
    const fetchConversations = async () => {
      const response = await loadAllChatMessages();
      setAllConversations(response);
    };

    fetchConversations();
  }, []);
  const handleConversationClick = (conversation: Conversation) => {
    setSelectedConversation(conversation);
    setClickedConversation(conversation);
    setShowDeleteIcon(true);
    onConversationClick(conversation.id);
  };

  const handleAddIconClick = () => {
    localStorage.removeItem('conversationId');
    navigate('/home');
    setSelectedConversation(null);
    setClickedConversation(null);
    onConversationClick(null);
  };

  const handleDeleteClick = (event: React.MouseEvent) => {
    event.stopPropagation();
    setShowDeleteIcon(!showDeleteIcon);
  };

  const handleTickClick = async (event: React.MouseEvent, conversation: Conversation) => {
    event.stopPropagation();
    setTimeout(async () => {
      await authService.AIDeleteConversationById(conversation.id);
      const response = await loadAllChatMessages();
      setAllConversations(response);
    }, 100);
  };

  const handleCancelClick = (event: React.MouseEvent) => {
    event.stopPropagation();
    setShowDeleteIcon(true);
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
            className={`conversation ${selectedConversation === conversation ? 'selected' : ''}`}
            key={index}
            onClick={() => handleConversationClick(conversation)}
          >
            <MessageIcon className="conversation-message-icon" />
            <div className="conversation-text">
              <span>{conversation.text}</span>
              <span>{conversation.questionAnswers[0].question}</span>
            </div>
            {clickedConversation?.id === conversation.id && (
              <>
                {showDeleteIcon ? (
                  <DeleteIcon
                    className="conversation-delete-icon"
                    onClick={(e) => handleDeleteClick(e, conversation)}
                  />
                ) : (
                  <>
                    <CheckIcon
                      className="conversation-tick-icon"
                      onClick={(e) => handleTickClick(e, conversation)}
                    />
                    <CloseIcon
                      className="conversation-close-icon"
                      onClick={(e) => handleCancelClick(e, conversation)}
                    />
                  </>
                )}
              </>
            )}
          </div>
        ))}
      </div>
      <div className="vertical-chatline-left"></div>
      <div className="vertical-chatline-right"></div>
    </div>
  );
};
export default Chats;
