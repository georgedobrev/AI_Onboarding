import React, { useState } from 'react';
import { TextField, IconButton } from '@mui/material';
import { Send } from '@mui/icons-material';
import FileUploader from './FileUploader.tsx';
import { authService } from '../../services/authService.ts';
import './Messages.css';

const Messages: React.FC = () => {
  const [searchQuery, setSearchQuery] = useState('');
  const [messages, setMessages] = useState<
    { text: string; isAnswer: boolean; isTyping?: boolean }[]
  >([]);

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearchQuery(event.target.value);
  };

  const handleSearchSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    if (searchQuery.trim() !== '') {
      setMessages((prevMessages) => [...prevMessages, { text: searchQuery, isAnswer: false }]);
      setSearchQuery('');
    }
    setMessages((prevMessages) => [...prevMessages, { text: '', isAnswer: true, isTyping: true }]);
    const response = await authService.AISearchResponse(searchQuery);
    setMessages((prevMessages) => [
      ...prevMessages.slice(0, -1),
      { text: response.data, isAnswer: true },
    ]);
  };

  return (
    <div className="messages-container">
      <div className="messages-header">
        <h2 className="messages-title">Messages</h2>
        <div className="horizontal-messagesline"></div>
      </div>
      <div className="messages-content-container">
        <div className="messages-content">
          <div className="chat-messages">
            {messages.map((message) => (
              <div
                className={`message ${message.isAnswer ? 'answer' : ''} ${
                  message.isTyping ? 'typing' : ''
                }`}
              >
                {message.isAnswer && <div className="logo-container" />}
                {message.text}
              </div>
            ))}
          </div>
          <form onSubmit={handleSearchSubmit} className="search-container">
            <div className="search-input">
              <FileUploader />
              <TextField
                placeholder="Write a message"
                value={searchQuery}
                onChange={handleSearchChange}
                fullWidth
                InputProps={{
                  className: 'search-textfield',
                }}
              />
              <IconButton className="send-icon" type="submit">
                <Send className="send-btn" />
              </IconButton>
            </div>
          </form>
        </div>
        <div className="vertical-messagesline"></div>
        <div className="files-content">
          <h2 className="files-title">Files</h2>
          <div className="horizontal-filesline"></div>
        </div>
      </div>
    </div>
  );
};

export default Messages;
