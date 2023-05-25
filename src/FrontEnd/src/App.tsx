import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import Navbar from './components/Navbar/Navbar.tsx'
import Home from './components/Home/Home.tsx'
function App() {
    return (
        <Router>
            <div className="App">
                <Navbar />
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/home" element={<Home />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;