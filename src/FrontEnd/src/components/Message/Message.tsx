import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useLocation, useParams } from 'react-router-dom';
import { IconButton, TextField } from '@mui/material';
import { Send, EmojiObjects, ErrorOutline } from '@mui/icons-material';
import store, { fetchConversations, fetchAISearchResponse } from '../../store/reduxStore.ts';
import { authService } from '../../services/authService.ts';
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

const Message: React.FC = () => {
  const location = useLocation();
  const dispatch = useDispatch();
  const { id } = useParams<{ id: string }>();
  const [question, setQuestion] = useState<string>('');
  const [messages, setMessages] = useState<Message[]>([]);
  const [currentConversationId, setCurrentConversationId] = useState<number | null>(null);
  const [questionsAnswers, setQuestionsAnswers] = useState<string[]>([]);
  const [showWelcomeHeader, setShowWelcomeHeader] = useState<boolean>(false);
  const [isMessageSent, setIsMessageSent] = useState<boolean>(false);
  const conversationId = localStorage.getItem('conversationId');
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

  useEffect(() => {
    handleConversationById();
    if (!id && !conversationId) {
      setCurrentConversationId(null);
      setQuestionsAnswers([]);
    } else {
      setCurrentConversationId(Number(id));
      localStorage.setItem('conversationId', id ?? '');
    }
  }, [id]);

  useEffect(() => {
    setMessages([]);
    if (aiSearchResponse.answer !== '') {
      aiSearchResponse.answer = '';
    }
  }, [currentConversationId, location.pathname]);

  useEffect(() => {
    if (location.pathname === '/upload') {
      return;
    }
    if (aiSearchResponse.answer) {
      setMessages((prevMessages) => [
        ...prevMessages.slice(0, -1),
        { text: aiSearchResponse.answer, isAnswer: true },
      ]);
    }
    dispatch(fetchConversations());
  }, [aiSearchResponse.answer]);

  useEffect(() => {
    const hasConversationData = messages.length > 0 || questionsAnswers.length > 0;
    setShowWelcomeHeader(!hasConversationData);
    setIsMessageSent(false);
  }, [messages, isMessageSent]);

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setQuestion(event.target.value);
  };

  const handleSearchSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    setIsMessageSent(true);
    if (question.trim() !== '') {
      setMessages((prevMessages) => [...prevMessages, { text: question, isAnswer: false }]);
      setQuestion('');
    }
    if (!messages.some((message) => message.isTyping)) {
      setMessages((prevMessages) => [
        ...prevMessages,
        { text: '', isAnswer: true, isTyping: true },
      ]);

      if (!id && !conversationId) {
        dispatch(fetchAISearchResponse({ question }));
        dispatch(fetchConversations());
      } else {
        dispatch(fetchAISearchResponse({ question, id: id || conversationId }));
        dispatch(fetchConversations());
      }
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
        {showWelcomeHeader && window.location.pathname === '/home' && (
          <div className="welcome-header">
            <div className="welcome-capabilities">
              <div className="welcome-capabilities-header">
                <EmojiObjects className="lightbulb-emoji" />
                <h2>Capabilities:</h2>
              </div>
              <div className="welcome-capabilities-accepts">
                • Accepts document submissions in various formats, including DOCX, PDF, and others.
              </div>
              <div className="welcome-capabilities-util">
                • Utilizes AI for document processing and extraction of relevant information.
              </div>
              <div className="welcome-capabilities-queries">
                • Supports natural language processing for user queries and provides relevant
                responses.
              </div>
            </div>
            <div className="welcome-limitations">
              <div className="welcome-limitations-header">
                <ErrorOutline className="limitations-emoji" />
                <h2>Limitations:</h2>
              </div>
              <div className="welcome-limitations-processing">
                • Limited to processing document formats such as DOCX, PDF, and other supported
                formats.
              </div>
              <div className="welcome-limitations-accuracy">
                • The accuracy of AI-based document processing may vary depending on the quality and
                complexity of the documents.
              </div>
              <div className="welcome-limitations-queries">
                • The chatbot's natural language processing capabilities may have limitations in
                understanding complex queries or specialized terminology.
              </div>
            </div>
            <div className="welcome-examples>"></div>
          </div>
        )}
        <div className="messages-content">
          {renderSelectedQuestionAnswers}
          {renderMessages}
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
