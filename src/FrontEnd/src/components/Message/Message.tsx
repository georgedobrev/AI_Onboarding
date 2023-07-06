import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useLocation, useParams } from 'react-router-dom';
import { IconButton, TextField } from '@mui/material';
import { Send } from '@mui/icons-material';
import FileUploader from './FileUploader.tsx';
import { authService } from '../../services/authService.ts';
import store, { fetchAISearchResponse, fetchConversations } from '../../store/reduxStore.ts';
import './Message.css';

interface Message {
  text: string;
  isAnswer: boolean;
  isTyping?: boolean;
}

interface Conversation {
  id: string;
  questionAnswers: [
    {
      question: string;
      answer: string;
    }
  ];
}

interface AIResponse {
  data: {
    id: string;
    answer: string;
  };
}

const Message: React.FC = () => {
  const location = useLocation();
  const dispatch = useDispatch();
  const { id } = useParams<{ id: string }>();
  const [userRole, setUserRole] = useState<string | null>(null);
  const [question, setQuestion] = useState<string>('');
  const [messages, setMessages] = useState<Message[]>([]);
  const [currentConversationId, setCurrentConversationId] = useState<number | null>(null);
  const [questionsAnswers, setQuestionsAnswers] = useState<string[]>([]);
  type RootState = ReturnType<typeof store.getState>;
  const aiSearchResponse = useSelector((state: RootState) => state.aiSearchResponse);
  const handleConversationClick = (conversation: Conversation) => {
    const questionsAnswers: string[] = [];

    conversation.questionAnswers.forEach((qa) => {
      questionsAnswers.push(qa.question);
      questionsAnswers.push(qa.answer);
    });

    setQuestionsAnswers(questionsAnswers);
  };

  const handleConversationById = async () => {
    if (!id) {
      return;
    } else {
      const response = await authService.AIGetConversationById(Number(id));
      const userConversation: Conversation = {
        id: response.data.id,
        questionAnswers: JSON.parse(JSON.stringify(response.data.questionAnswers)),
      };
      handleConversationClick(userConversation);
    }
  };

  const roles = {
    Administrator: 'Administrator',
    Employee: 'Employee',
  };

  useEffect(() => {
    const storedUserRole = localStorage.getItem('userRole');
    setUserRole(storedUserRole);
    handleConversationById();
    if (!id && !localStorage.getItem('conversationId')) {
      setCurrentConversationId(null);
      setQuestionsAnswers([]);
    } else {
      setCurrentConversationId(Number(id));
      localStorage.setItem('conversationId', id ?? '');
    }
  }, [id]);

  useEffect(() => {
    setMessages([]);
  }, [currentConversationId]);

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

      if (!id && !localStorage.getItem('conversationId')) {
        dispatch(fetchAISearchResponse({ question }));
        dispatch(fetchConversations());
      } else {
        dispatch(
          fetchAISearchResponse({ question, id: id || localStorage.getItem('conversationId') })
        );
        dispatch(fetchConversations());
      }
      setMessages((prevMessages) => [
        ...prevMessages.slice(0, -1),
        { text: aiSearchResponse.answer, isAnswer: true },
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
