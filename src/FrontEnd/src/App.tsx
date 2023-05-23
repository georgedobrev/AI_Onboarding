import './assets/App.css';
import Navbar from './components/navbar.tsx';

function App() {
  return (
    <div className="App">
      <Navbar />

      <div className="content-container">
        <div className="heading-container">
          <h1 className="app-title">Chat Bot - Blankfactor</h1>
          <div className="header-line"></div>
        </div>
        <div className="chat-container">
          <h2>Chats</h2>
          <div className="vertical-chatline"></div>
          <div className="horizontal-chatline"></div>
        </div>
      </div>
    </div>
  );
}

export default App;
