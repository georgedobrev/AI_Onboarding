import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate, useLocation } from 'react-router-dom';
import Home from './components/Home/Home.tsx';
import Signup from './components/SignIn/Signup.tsx';
import Register from './components/Register/Register.tsx';
import Upload from './components/Upload/Upload.tsx';
import ResetPassword from './components/ResetPassword/ResetPassword.tsx';
import Account from './components/Account/Account.tsx';
import LandingPage from './components/LandingPage/LandingPage.tsx';
import { ToastContainer } from 'react-toastify';
import config from './config.json';
import 'react-toastify/dist/ReactToastify.css';
import { pdfjs } from 'react-pdf';
import { GoogleOAuthProvider } from '@react-oauth/google';
import './App.css';

pdfjs.GlobalWorkerOptions.workerSrc = `//cdnjs.cloudflare.com/ajax/libs/pdf.js/${pdfjs.version}/pdf.worker.js`;
const clientId = '87684702779-3ju9p8rrlfrbpq18e18ldd79eooph69g.apps.googleusercontent.com';

interface ProtectedRouteHomeProps {
  element: JSX.Element;
  redirectTo: string;
}

interface ProtectedRouteAccountProps {
  element: JSX.Element;
  redirectTo: string;
}

interface ProtectedRouteUploadProps {
  element: JSX.Element;
  redirectTo: string;
  allowedPaths: string[];
}

const ProtectedRouteHome: React.FC<ProtectedRouteHomeProps> = ({ element, redirectTo }) => {
  const accessToken = localStorage.getItem('accessToken');

  if (!accessToken) {
    return <Navigate to={redirectTo} replace />;
  }
  return element;
};

const ProtectedRouteAccount: React.FC<ProtectedRouteAccountProps> = ({ element, redirectTo }) => {
  const accessToken = localStorage.getItem('accessToken');

  if (!accessToken) {
    return <Navigate to={redirectTo} replace />;
  }
  return element;
};

const ProtectedRouteUpload: React.FC<ProtectedRouteUploadProps> = ({
  element,
  redirectTo,
  allowedPaths,
}) => {
  const accessToken = localStorage.getItem('accessToken');
  const location = useLocation();

  if (!accessToken) {
    return <Navigate to={redirectTo} replace />;
  }
  const tokenParts = accessToken.split('.');
  const tokenPayload = JSON.parse(atob(tokenParts[1]));
  const userRole = tokenPayload[config.JWTUserRole];
  if (userRole === 'Administrator') {
    return element;
  }

  if (!allowedPaths.includes(location.pathname)) {
    return <Navigate to={redirectTo} replace />;
  }
  return <Navigate to={redirectTo} replace />;
};

const App = () => {
  const [isInitialized, setIsInitialized] = useState(false);

  useEffect(() => {
    setIsInitialized(true);
  }, []);

  if (!isInitialized) {
    return null;
  }

  return (
    <GoogleOAuthProvider clientId={clientId}>
      <Router>
        <div className="App">
          <Routes>
            <Route path="/" element={<LandingPage />} />
            <Route path="/signup" element={<Signup />} />
            <Route path="/register" element={<Register />} />
            <Route path="/account/change-password" element={<ResetPassword />} />
            <Route path="/account/reset-password" element={<ResetPassword />} />
            <Route
              path="/home"
              element={<ProtectedRouteHome element={<Home />} redirectTo="/signup" />}
            />
            <Route
              path="/upload/"
              element={
                <ProtectedRouteAccount
                  element={<Upload />}
                  redirectTo="/signup"
                  allowedPaths={['/upload']}
                />
              }
            />
          </Routes>
          <ToastContainer />
        </div>
      </Router>
    </GoogleOAuthProvider>
  );
};

export default App;
