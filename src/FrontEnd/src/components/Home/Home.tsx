import React from "react";
import Navbar from "../Navbar/Navbar.tsx";
import './Home.css';

const Home: React.FC = () => {
    return (
        <>
        <Navbar />
        <div className="content-container">
            <div className="header-container">
                <h1 className="header-title">Chat Bot - Blankfactor</h1>
                <div className="header-line"></div>
            </div>
        </div>
        </>
    )
}
export default Home;
