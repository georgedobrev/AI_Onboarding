import React, { useState } from 'react';
import TextField from '@mui/material/TextField';
import AttachFileIcon from '@mui/icons-material/AttachFile';
import SendIcon from '@mui/icons-material/Send';
import IconButton from '@mui/material/IconButton';
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
              <IconButton style={{ color: 'white' }}>
                <AttachFileIcon style={{ marginRight: '10px' }} />
              </IconButton>
              <TextField
                placeholder="Write a message"
                value={searchQuery}
                onChange={handleSearchChange}
                fullWidth
                InputProps={{
                  className: 'search-textfield',
                  style: {
                    color: 'white',
                  },
                }}
              />
              <IconButton style={{ color: 'white' }}>
                <SendIcon style={{ marginLeft: '10px' }} />
              </IconButton>
            </div>
          </form>
        </div>
        <div className="vertical-messagesline"></div>
        <div className="files-content"></div>
      </div>
    </div>
  );
};

export default Messages;
