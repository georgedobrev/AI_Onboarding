import React from "react";
import './Messages.css';

const Messages: React.FC = () => {
    return (
        <div className="messages-container">
            <h2 className="messages-title">Messages</h2>
            <div className="vertical-messagesline"></div>
            <div className="horizontal-messagesline"></div>
        </div>

    )
}
export default Messages;