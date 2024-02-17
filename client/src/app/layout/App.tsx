import { Outlet } from "react-router-dom";
import Header from "./Header";
import "./style.css";

function App() {
  return (
    <div className="App">
      <Header />
      <Outlet />
    </div>
  );
}

export default App;
