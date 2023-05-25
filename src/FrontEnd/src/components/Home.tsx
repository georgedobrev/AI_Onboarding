import React from "react";
import Chats from "./Chats/Chats.tsx";
import Messages from "./Messages/Messages.tsx";

const Home: React.FC = () => {
    return (
        <>
            <div className="content-container">
                <div className="heading-container">
                    <h1 className="app-title">Chat Bot - Blankfactor</h1>
                    <div className="header-line"></div>
                </div>
                <Chats/>
                <Messages/>
            </div>
        </>
    )
}
export default Home;