import { useState } from "react";
import Account from "../../app/api/agent";
import "./Account.css";

const Login = () => {
  const [loginData, setLoginData] = useState({
    username: "",
    password: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setLoginData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      console.log(loginData);

      const response = await Account.login(loginData);

      console.log("Login successful!", response);
      console.log("Your token", response.token);
    } catch (error) {
      console.error("Login failed!", error);
    }
  };

  return (
    <div>
      <h1>Login</h1>
      <form className="form-container" onSubmit={handleSubmit}>
        <div className="form-group">
          <label className="form-label">Username:</label>
          <input
            type="text"
            className="form-input"
            name="username"
            value={loginData.username}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Password:</label>
          <input
            type="password"
            className="form-input"
            name="password"
            value={loginData.password}
            onChange={handleChange}
          />
        </div>
        <button type="submit" className="form-button">
          Login
        </button>
      </form>
    </div>
  );
};

export default Login;
