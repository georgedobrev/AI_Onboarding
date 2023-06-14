import React, { useState } from 'react';
import { TextField, IconButton } from '@mui/material';
import { Send } from '@mui/icons-material';
import FileUploader from './FileUploader.tsx';
import './Messages.css';

const Messages: React.FC = () => {
  const [searchQuery, setSearchQuery] = useState('');

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearchQuery(event.target.value);
  };

  const handleSearchSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    // Perform search logic here based on the searchQuery
    // You can interact with your chat bot or perform any other action
  };

  return (
    <div className="messages-container">
      <div className="messages-header">
        <h2 className="messages-title">Messages</h2>
        <div className="horizontal-messagesline"></div>
      </div>
      <div className="messages-content-container">
        <div className="messages-content">
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
              <IconButton className="send-icon">
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
