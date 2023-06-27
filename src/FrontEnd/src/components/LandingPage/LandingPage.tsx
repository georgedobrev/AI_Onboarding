import React from 'react';
import logoImage from '../../assets/blankfactor-big-logo.jpg';
import './LandingPage.css';
import Button from './Button.tsx';

const LandingPage: React.FC = () => {
  return (
    <div className="landing-page-container">
      <div className="landing-page-header">
        <img src={logoImage} alt="blankfactor" className="landing-page-logo" />
        <div className="landing-page-header-text">
          <h1 className="landing-page-h1">Welcome to Blankfactor Chat Bot</h1>
          <p className="landing-page-text">
            Welcome to AI_Onboarding, your intelligent chat bot assistant!
            <br />
            Simplify the process of onboarding in just a few clicks. <br />
            Say goodbye to lengthy paperwork and hello to hassle-free document submission.
          </p>
        </div>
        <Button />
      </div>
    </div>
  );
};

export default LandingPage;
