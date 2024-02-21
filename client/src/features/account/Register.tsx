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

  const handleApiErrors = (errors: any) => {
    console.log(errors);
    if (errors.response.data.errors) {
      const modelStateErrors: string[] = [];

      for (const key in errors.response.data.errors) {
        if (errors.response.data.errors) {
          modelStateErrors.push(errors.response.data.errors[key]);
        }
      }

      modelStateErrors.forEach((error: string) => {
        const firstError = error[0];

        if (firstError.includes("Passwords")) {
          console.log("password", { message: firstError });
        } else if (firstError.includes("Email")) {
          console.log("email", { message: firstError });
        } else if (firstError.includes("FirstName")) {
          console.log("username", { message: firstError });
        } else if (firstError.includes("LastName")) {
          console.log("lastName", { message: firstError });
        }
      });
    }
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
      handleApiErrors(error);
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
