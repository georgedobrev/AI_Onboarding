import React from 'react';
import Navbar from '../Navbar/Navbar.tsx';
import FileUploader from '../Message/FileUploader.tsx';
import blankfactorLogo from '../../assets/blankfactor-logo.jpg';

const Upload: React.FC = () => {
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
          <FileUploader />
        </div>
      </div>
    </>
  );
};
export default Upload;
