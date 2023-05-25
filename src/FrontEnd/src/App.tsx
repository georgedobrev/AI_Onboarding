import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './assets/App.css';
import Navbar from './components/Navbar.tsx';
import Home from './components/Home.tsx';

function App() {
    return (
        <Router>
            <div className="App">
                <Navbar />
                <Routes>
                    <Route path="/home" element={<Home />} />
                </Routes>
            </div>
        </Router>
    );
}

export default App;
