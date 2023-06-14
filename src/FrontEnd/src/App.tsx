import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import Home from './components/Home/Home.tsx';
import Signup from './components/SignIn/Signup.tsx';
import Register from './components/Register/Register.tsx';
import { GoogleOAuthProvider } from '@react-oauth/google';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const clientId = '87684702779-3ju9p8rrlfrbpq18e18ldd79eooph69g.apps.googleusercontent.com';

const App = () => {
  return (
    <GoogleOAuthProvider clientId={clientId}>
      <Router>
        <div className="App">
          <Routes>
            <Route path="/home" element={<Home />} />
          </Routes>
          <Routes>
            <Route path="/signup" element={<Signup />} />
          </Routes>
          <Routes>
            <Route path="/register" element={<Register />} />
          </Routes>
          <ToastContainer />
        </div>
      </Router>
    </GoogleOAuthProvider>
  );
};

export default App;
