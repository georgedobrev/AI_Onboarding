import React, { useState } from 'react';
import { TextField, IconButton } from '@mui/material';
import { Send } from '@mui/icons-material';
import FileUploader from './FileUploader.tsx';
import './Messages.css';

const Messages: React.FC = () => {
  const [searchQuery, setSearchQuery] = useState('');
  const [messages, setMessages] = useState<string[]>([]);

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearchQuery(event.target.value);
  };

  const handleSearchSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    if (searchQuery.trim() !== '') {
      setMessages((prevMessages) => [...prevMessages, searchQuery]);
      setSearchQuery('');
    }
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
            {messages.map((message, index) => (
              <div key={index} className="message">
                {message}
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
