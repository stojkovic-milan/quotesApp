import './App.css';
import Quotes from './pages/Quotes';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Unauthorized from './pages/Unauthorized';


function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/unauthorized" element={<Unauthorized />} />
        <Route path="*" element={<Quotes />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
