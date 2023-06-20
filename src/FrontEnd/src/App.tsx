import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate, useLocation } from 'react-router-dom';
import './App.css';
import Home from './components/Home/Home.tsx';
import Signup from './components/SignIn/Signup.tsx';
import Register from './components/Register/Register.tsx';
import Account from './components/Account/Account.tsx';
import ResetPassword from './components/ResetPassword/ResetPassword.tsx';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { pdfjs } from 'react-pdf';
import { GoogleOAuthProvider } from '@react-oauth/google';

pdfjs.GlobalWorkerOptions.workerSrc = `//cdnjs.cloudflare.com/ajax/libs/pdf.js/${pdfjs.version}/pdf.worker.js`;
const clientId = '87684702779-3ju9p8rrlfrbpq18e18ldd79eooph69g.apps.googleusercontent.com';

interface ProtectedRouteHomeProps {
  element: JSX.Element;
  redirectTo: string;
}

interface ProtectedRouteAccountProps {
  element: JSX.Element;
  redirectTo: string;
  allowedPaths: string[];
}

const ProtectedRouteHome: React.FC<ProtectedRouteHomeProps> = ({ element, redirectTo }) => {
  const accessToken = localStorage.getItem('accessToken');

  if (!accessToken) {
    return <Navigate to={redirectTo} replace />;
  } else {
    return element;
  }
};

const ProtectedRouteAccount: React.FC<ProtectedRouteAccountProps> = ({
  element,
  redirectTo,
  allowedPaths,
}) => {
  const accessToken = localStorage.getItem('accessToken');
  const location = useLocation();

  if (!accessToken) {
    return <Navigate to={redirectTo} replace />;
  } else {
    const tokenParts = accessToken.split('.');
    const tokenPayload = JSON.parse(atob(tokenParts[1]));
    const userRole = tokenPayload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    if (userRole === 'Administrator') {
      return element;
    }
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
            <Route path="/" element={<Navigate to="/signup" />} />
            <Route path="/signup" element={<Signup />} />
            <Route path="/register" element={<Register />} />
            <Route path="/account/reset-password" element={<ResetPassword />} />
            <Route path="/account/reset-password/success" element={<ResetPassword />} />
            <Route
              path="/home"
              element={
                <ProtectedRouteHome
                  element={
                    <>
                      <Home />
                    </>
                  }
                  redirectTo="/signup"
                />
              }
            />
            <Route
              path="/account/*"
              element={
                <ProtectedRouteAccount
                  element={<Account />}
                  redirectTo="/signup"
                  allowedPaths={['/account']}
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
