import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import Home from './components/Home/Home.tsx';
import Signup from "./components/SignIn/Signup.tsx";
function App() {
    return (
        <Router>
            <div className="App">
                <Routes>
                    <Route path="/home" element={<Home />} />
                </Routes>
                <Signup />
            </div>
        </Router>
    );
}


export default App;
