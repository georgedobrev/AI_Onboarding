import { GoogleLogin } from "@react-oauth/google";
import './GoogleSignup.css';
import React, { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';

const GoogleSignup: React.FC = () => {
    const navigate = useNavigate();
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

    return (
        <div className="google-signup-container">
            {!isSignupSuccess && (
                <GoogleLogin
                    onSuccess={handleGoogleSignupSuccess}
                    onError={handleGoogleSignupError}
                />
            )}
        </div>
    );
}

export default GoogleSignup;
