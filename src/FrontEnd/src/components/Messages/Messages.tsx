import React, { useEffect, useState } from 'react';
import { useLocation, useParams } from 'react-router-dom';
import FileUploader from './FileUploader.tsx';
import { TextField, IconButton } from '@mui/material';
import { Send } from '@mui/icons-material';
import { authService } from '../../services/authService.ts';
import './Messages.css';

interface MessagesProps {
  setConversations: (conversations: any) => void;
  selectedQuestion: string;
  selectedAnswer: string;
}

const Messages: React.FC<MessagesProps> = ({
  setConversations,
  selectedQuestion,
  selectedAnswer,
}) => {
  const location = useLocation();
  const [userRole, setUserRole] = useState<string | null>(null);
  const [question, setQuestion] = useState('');
  const [messages, setMessages] = useState<
    { text: string; isAnswer: boolean; isTyping?: boolean }[]
  >([]);

  const roles = {
    Administrator: 'Administrator',
    Employee: 'Employee',
  };

  const f = async () => {
    const conversations = await authService.AIGetAllConversations();
    setConversations(conversations.data.conversations);
  };

  useEffect(() => {
    const storedUserRole = localStorage.getItem('userRole');
    f();
    setUserRole(storedUserRole);
  }, []);

  useEffect(() => {
    setMessages([]);
  }, []);

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setQuestion(event.target.value);
  };

  const handleSearchSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    if (question.trim() !== '') {
      setMessages((prevMessages) => [...prevMessages, { text: question, isAnswer: false }]);
      setQuestion('');
    }
    if (!messages.some((message) => message.isTyping)) {
      setMessages((prevMessages) => [
        ...prevMessages,
        { text: '', isAnswer: true, isTyping: true },
      ]);
      const response = await authService.AISearchResponse({ question });
      setMessages((prevMessages) => [
        ...prevMessages.slice(0, -1),
        { text: response.data, isAnswer: true },
      ]);
    }
  };

  const isMessageInProgress = messages.some((message) => message.isTyping);
  const name = localStorage.getItem('fullName');
  const renderMessages = messages.map((message, index) => {
    if (message.isAnswer) {
      return (
        <div className="message-answer-wrapper" key={index}>
          <span className="message-answer-name">Blankfactor Chat Bot</span>
          <div className="message answer">
            <span>{message.text}</span>
            {message.isTyping && <span className="dot-animation" />}
          </div>
        </div>
      );
    } else {
      return (
        <div className="message-question-wrapper" key={index}>
          <span className="message-question-name">{name}</span>
          <div className="message question">
            <span>{message.text}</span>
          </div>
        </div>
      );
    }
  });

  const renderSelectedQuestion = selectedQuestion.split('\n').map((question, index) => (
    <div className="message-question-wrapper" key={index}>
      <span className="message-question-name">Question:</span>
      <div className="message question">
        <span>{question}</span>
      </div>
    </div>
  ));

  const renderSelectedAnswer = selectedAnswer.split('\n').map((answer, index) => (
    <div className="message-answer-wrapper" key={index}>
      <span className="message-answer-name">Answer:</span>
      <div className="message answer">
        <span>{answer}</span>
      </div>
    </div>
  ));

  return (
    <div className="messages-container">
      <div className="messages-header">
        <h2 className="messages-title">Messages</h2>
        <div className="horizontal-messagesline"></div>
      </div>
      <div className="messages-content-container">
        <div className="messages-content">
          {renderSelectedQuestion}
          {renderSelectedAnswer}
          {renderMessages}
          {location.pathname === '/upload' && userRole === roles.Administrator && <FileUploader />}
          {location.pathname === '/home' && (
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
          )}
        </div>
      </div>
    </div>
  );
};

export default Messages;
