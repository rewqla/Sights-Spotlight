import "./Account.css";

const Login = () => {
  return (
    <div>
      <h1>Login</h1>
      <form className="form-container">
        <div className="form-group">
          <label className="form-label">Username:</label>
          <input type="text" className="form-input" />
        </div>
        <div className="form-group">
          <label className="form-label">Password:</label>
          <input type="password" className="form-input" />
        </div>
        <button type="submit" className="form-button">
          Login
        </button>
      </form>
    </div>
  );
};

export default Login;
