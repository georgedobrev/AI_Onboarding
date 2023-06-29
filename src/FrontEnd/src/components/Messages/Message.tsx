import React, { useEffect, useState } from 'react';
import { useLocation, useParams } from 'react-router-dom';
import FileUploader from './FileUploader.tsx';
import { TextField, IconButton } from '@mui/material';
import { Send } from '@mui/icons-material';
import { authService } from '../../services/authService.ts';
import './Messages.css';

const Message: React.FC = () => {
  const location = useLocation();
  const { id } = useParams();
  const [userRole, setUserRole] = useState<string | null>(null);
  const [question, setQuestion] = useState('');
  const [messages, setMessages] = useState<
    { text: string; isAnswer: boolean; isTyping?: boolean }[]
  >([]);
  const [currentConversationId, setCurrentConversationId] = useState<string | null>(null);
  const [questionsAnswers, setQuestionsAnswers] = useState([]);

  const handleConversationClick = (conversation) => {
    const questionsAnswers = [];

    for (let i = 0; i < conversation.questionAnswers.length; i++) {
      const question = conversation.questionAnswers[i].question;
      const answer = conversation.questionAnswers[i].answer;

      questionsAnswers.push(question);
      questionsAnswers.push(answer);
    }

    console.log(`questionsAnswers: ${questionsAnswers} `);
    setQuestionsAnswers(questionsAnswers);
  };

  const lplp = async () => {
    if (id === undefined) {
      return;
    } else {
      const response = await authService.AIGetConversationById(id);
      console.log(response.data);
      handleConversationClick(response.data);
    }
  };

  const roles = {
    Administrator: 'Administrator',
    Employee: 'Employee',
  };

  useEffect(() => {
    const storedUserRole = localStorage.getItem('userRole');
    setUserRole(storedUserRole);
    lplp();
  }, [id]);

  useEffect(() => {
    setMessages([]);
  }, [currentConversationId]);

  useEffect(() => {
    setCurrentConversationId(id);
  }, [id]);

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

      let response: object;
      if (!id) {
        response = await authService.AISearchResponse({ question });
      } else {
        response = await authService.AISearchResponse({ question, id });
      }
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

  const renderSelectedQuestionAnswers = questionsAnswers.map((item, index) => {
    if (index % 2 === 0) {
      return (
        <div className="message-question-wrapper" key={index}>
          <span className="message-question-name">{name}</span>
          <div className="message question">
            <span>{item}</span>
          </div>
        </div>
      );
    } else {
      return (
        <div className="message-answer-wrapper" key={index}>
          <span className="message-answer-name">Blankfactor Chat Bot</span>
          <div className="message answer">
            <span>{item}</span>
          </div>
        </div>
      );
    }
  });

  return (
    <div className="messages-container">
      <div className="messages-header">
        <h2 className="messages-title">Messages</h2>
        <div className="horizontal-messagesline"></div>
      </div>
      <div className="messages-content-container">
        <div className="messages-content">
          {renderSelectedQuestionAnswers}
          {renderMessages}
          {location.pathname === '/upload' && userRole === roles.Administrator && <FileUploader />}
          {location.pathname.includes('home') && (
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

export default Message;
