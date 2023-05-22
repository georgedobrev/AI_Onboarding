import "./assets/App.css";
import Navbar from "./components/navbar.tsx";

function App() {
    return (
        <div className="App">
            <Navbar />
            <div className="line"></div>
            <div className="heading-container">
                <h1>Chat Bot - Blankfactor</h1>
            </div>
            <div className="cross-line"></div>
            <div className="content">
                {/* Content goes here */}
            </div>
        </div>
    );
}

export default App;
