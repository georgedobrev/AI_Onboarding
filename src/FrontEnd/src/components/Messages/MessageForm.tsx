// MessageForm.tsx
import React from 'react';
import { TextField, IconButton } from '@mui/material';
import { Send } from '@mui/icons-material';

interface MessageFormProps {
  question: string;
  isMessageInProgress: boolean;
  handleSearchChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
  handleSearchSubmit: (event: React.FormEvent) => Promise<void>;
}

const MessageForm: React.FC<MessageFormProps> = ({
  question,
  isMessageInProgress,
  handleSearchChange,
  handleSearchSubmit,
}) => {
  return (
    <form onSubmit={handleSearchSubmit} className="search-container">
      <div className="search-input">
        <TextField
          placeholder="Write a message"
          value={question}
          onChange={handleSearchChange}
          fullWidth
          InputProps={{
            className: 'search-textfield',
          }}
        />
        <IconButton className="send-icon" type="submit" disabled={isMessageInProgress}>
          <Send className="send-btn" />
        </IconButton>
      </div>
    </form>
  );
};

export default MessageForm;
