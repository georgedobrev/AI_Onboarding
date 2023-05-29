import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import {GoogleOAuthProvider} from '@react-oauth/google';

// 87684702779-3ju9p8rrlfrbpq18e18ldd79eooph69g.apps.googleusercontent.com

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
    <React.StrictMode>
        <GoogleOAuthProvider clientId="87684702779-3ju9p8rrlfrbpq18e18ldd79eooph69g.apps.googleusercontent.com">
            <App/>
        </GoogleOAuthProvider>
    </React.StrictMode>,
)
