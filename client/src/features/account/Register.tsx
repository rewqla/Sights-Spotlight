import axios from "axios";
import { useState } from "react";
import Account from "../../app/api/agent";
import "./Account.css";

const Register = () => {
  const [registerData, setRegisterData] = useState({
    username: "",
    email: "",
    firstName: "",
    lastName: "",
    password: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setRegisterData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      console.log(registerData);

      const response = await Account.register(registerData);

      console.log("Registration successful!", response);
      console.log("Your token", response.token);
    } catch (error) {
      console.error("Registration failed!", error);
    }
  };

  return (
    <div>
      <h1>Register</h1>
      <form className="form-container" onSubmit={handleSubmit}>
        <div className="form-group">
          <label className="form-label">Username:</label>
          <input
            type="text"
            className="form-input"
            name="username"
            value={registerData.username}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Email:</label>
          <input
            type="email"
            className="form-input"
            name="email"
            value={registerData.email}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label className="form-label">First Name:</label>
          <input
            type="text"
            className="form-input"
            name="firstName"
            value={registerData.firstName}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Last Name:</label>
          <input
            type="text"
            className="form-input"
            name="lastName"
            value={registerData.lastName}
            onChange={handleChange}
          />
        </div>
        <div className="form-group">
          <label className="form-label">Password:</label>
          <input
            type="password"
            className="form-input"
            name="password"
            value={registerData.password}
            onChange={handleChange}
          />
        </div>
        <button type="submit" className="form-button">
          Register
        </button>
      </form>
    </div>
  );
};

export default Register;
