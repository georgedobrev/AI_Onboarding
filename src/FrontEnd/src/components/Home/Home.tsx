import React, { useState, useEffect } from 'react';
import Navbar from '../Navbar/Navbar.tsx';
import Chats from '../Chats/Chats.tsx';
import Messages from '../Messages/Messages.tsx';
import blankfactorLogo from '../../assets/blankfactor-logo.jpg';
import './Home.css';

const Home: React.FC = () => {
  const [isMobileView, setIsMobileView] = useState(false);
  const [conversations, setConversations] = useState([]);
  const [selectedConversation, setSelectedConversation] = useState(null);
  const [selectedQuestion, setSelectedQuestion] = useState('');
  const [selectedAnswer, setSelectedAnswer] = useState('');

  const handleConversationClick = (conversation) => {
    const questions = [];
    const answers = [];

    for (let i = 0; i < conversation.questionAnswers.length; i++) {
      const question = conversation.questionAnswers[i].question;
      const answer = conversation.questionAnswers[i].answer;

      questions.push(question);
      answers.push(answer);
    }

    setSelectedQuestion(questions.join('\n')); // Join questions with a line break
    setSelectedAnswer(answers.join('\n')); // Join answers with a line break
  };

  useEffect(() => {
    const handleResize = () => {
      setIsMobileView(window.innerWidth < 768);
    };

    handleResize(); // Initial check
    window.addEventListener('resize', handleResize);

    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, []);

  return (
    <>
      <Navbar />
      <div className="content-container">
        <div className="header-container">
          <div className="header-logo-title">
            <img src={blankfactorLogo} alt="Blankfactor Logo" className="header-logo" />
            <h1 className="header-title">Chat Bot - Blankfactor</h1>
          </div>
          <div className="header-line"></div>
        </div>
        <div className="main-container">
          {!isMobileView && (
            <Chats conversations={conversations} onConversationClick={handleConversationClick} />
          )}
          <Messages
            setConversations={setConversations}
            selectedQuestion={selectedQuestion}
            selectedAnswer={selectedAnswer}
          />
        </div>
      </div>
    </>
  );
};

export default Home;
