import React, {useEffect, useState} from "react";
import {useNavigate, useLocation, Link} from 'react-router-dom';
import {CredentialResponse, GoogleLogin} from "@react-oauth/google";
import './Signup.css';
import {TextField} from "@mui/material";
import PasswordField from './PasswordField';
import logoImage from '../../assets/blankfactor-logo.jpg';
import Button from '@mui/material/Button';

const Signup: React.FC = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const [isSignupSuccess, setSignupSuccess] = useState(false);

    useEffect(() => {
        const storedSuccess = localStorage.getItem('signupSuccess');
        if (storedSuccess) {
            setSignupSuccess(true);
        }
    }, []);

    const handleGoogleSignupSuccess = (credentialResponse: CredentialResponse) => {
        console.log(credentialResponse);
        navigate('/home');
        setSignupSuccess(true);
        localStorage.setItem('signupSuccess', 'true');
    };

    const handleGoogleSignupError = () => {
        console.log('Login Failed');
    };

    const shouldRenderContainer = location.pathname !== '/home';

    return shouldRenderContainer ? (
        <div className="signup-container">
            <div className="signup-overlay">
                <div className="signup-header">
                    <div className="signup-header-container">
                        <img src={logoImage} alt="blankfactor" className="google-signup-logo"/>
                        <h1 className="signup-welcome-h1">Welcome to <br/></h1>
                        <span className="signup-welcome-span">Blankfactor ChatBot!</span>
                    </div>
                    <div className="signup-header-forms">
                        <TextField id="outlined-basic" label="Email address" variant="outlined"
                                   className="email-field"/>
                        <PasswordField label="Password" className="password-field"/>
                        <Button variant="contained" className="signup-continue-btn">Continue</Button>
                    </div>
                    <span className="signup-noaccount">Don't have an account?&nbsp;
                        <Link to="/register" className="signup-hyperlink">Sign up</Link>
                    </span>
                </div>
                <div className="signup-body">
                    <span className="signup-or">OR</span>
                    <div className="signup-button-container">
                        {!isSignupSuccess && (
                            <GoogleLogin
                                onSuccess={handleGoogleSignupSuccess}
                                onError={handleGoogleSignupError}
                                text="signup_with"
                                size="large"
                            />
                        )}
                    </div>
                </div>
            </div>
        </div>
    ) : null;
};

export default Signup;
