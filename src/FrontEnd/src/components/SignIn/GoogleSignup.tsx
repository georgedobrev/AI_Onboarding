import {GoogleLogin} from "@react-oauth/google";
import './GoogleSignup.css';
import React, {useEffect, useState} from "react";
import {useNavigate, useLocation} from 'react-router-dom';
import logoImage from '../../assets/blankfactor-logo.jpg';

const GoogleSignup: React.FC = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const [isSignupSuccess, setSignupSuccess] = useState(false);

    useEffect(() => {
        const storedSuccess = localStorage.getItem('signupSuccess');
        if (storedSuccess) {
            setSignupSuccess(true);
        }
    }, []);

    const handleGoogleSignupSuccess = (credentialResponse: any) => {
        console.log(credentialResponse);
        // Perform any additional logic for successful sign-up
        // Redirect to "/home" route
        navigate('/home');
        setSignupSuccess(true);
        localStorage.setItem('signupSuccess', 'true');
    };

    const handleGoogleSignupError = () => {
        console.log('Login Failed');
        // Handle login failure if needed
    };

    const shouldRenderContainer = location.pathname !== '/home';

    return (
        shouldRenderContainer && (
            <div className="google-signup-container">
                <div className="google-signup-overlay">
                    <div className="google-signup-header">
                        <img src={logoImage} alt="blankfactor" className="google-signup-logo"/>
                        <h1 className="google-signup-welcome-h1">Welcome to <br/> </h1>
                        <span className="google-signup-welcome-span">Blankfactor ChatBot!</span>
                    </div>
                    <div className="google-signup-body">
                        {/*<h1 className="google-signup-h1">Sign up with Google</h1>*/}
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
        )
    );
}

export default GoogleSignup;
