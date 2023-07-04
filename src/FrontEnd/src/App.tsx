import React, { useEffect, useState } from 'react';
import { Provider } from 'react-redux';
import { BrowserRouter as Router, Route, Routes, Navigate, useLocation } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import { pdfjs } from 'react-pdf';
import { GoogleOAuthProvider } from '@react-oauth/google';
import jwt_decode from 'jwt-decode';
import Home from './components/Home/Home.tsx';
import Signup from './components/SignIn/Signup.tsx';
import Register from './components/Register/Register.tsx';
import Upload from './components/Upload/Upload.tsx';
import ResetPassword from './components/ResetPassword/ResetPassword.tsx';
import Account from './components/Account/Account.tsx';
import LandingPage from './components/LandingPage/LandingPage.tsx';
import Message from './components/Message/Message.tsx';
import store from './store/reduxStore.ts';
import config from './config.json';
import './App.css';
import 'react-toastify/dist/ReactToastify.css';

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

const roles = {
  Administrator: 'Administrator',
  Employee: 'Employee',
};

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
  const tokenPayload = jwt_decode(accessToken);
  const userRole = tokenPayload[config.JWTUserRole];
  if (userRole === roles.Administrator) {
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
    <Provider store={store}>
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
              >
                <Route index element={<Message />} />
                <Route path=":id" element={<Message />} />
              </Route>
              <Route
                path="/account"
                element={<ProtectedRouteAccount element={<Account />} redirectTo="/signup" />}
              />
              <Route
                path="/upload/"
                element={
                  <ProtectedRouteUpload
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
    </Provider>
  );
};

export default App;
