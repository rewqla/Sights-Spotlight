import { createBrowserRouter } from "react-router-dom";
import Login from "../../features/account/Login";
import Register from "../../features/account/Register";
import HomePage from "../../features/home/HomePage";
import App from "../layout/App";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "login", element: <Login /> },
      { path: "register", element: <Register /> },
    ],
  },
]);
