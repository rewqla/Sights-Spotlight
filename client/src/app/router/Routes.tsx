import { createBrowserRouter } from "react-router-dom";
import Login from "../../features/account/Login";
import Register from "../../features/account/Register";
import Countries from "../../features/country/Counties";
import CountryDetails from "../../features/country/CountryDetails";
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
      { path: "countries", element: <Countries /> },
      { path: "countries/:id", element: <CountryDetails /> },
    ],
  },
]);
