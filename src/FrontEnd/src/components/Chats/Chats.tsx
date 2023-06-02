import React from "react";
import './Chats.css';

const Chats: React.FC = () => {
    return (
        <div className="chat-container">
            <h2 className="chats-title">Chats</h2>
            <div className="vertical-chatline"></div>
            <div className="horizontal-chatline"></div>
        </div>

    )
}
export default Chats;