import "./Account.css";
const Register = () => {
  return (
    <div>
      <h1>Register</h1>
      <form className="form-container">
        <div className="form-group">
          <label className="form-label">First name:</label>
          <input type="text" className="form-input" />
        </div>
        <div className="form-group">
          <label className="form-label">Last name:</label>
          <input type="text" className="form-input" />
        </div>
        <div className="form-group">
          <label className="form-label">Username:</label>
          <input type="text" className="form-input" />
        </div>
        <div className="form-group">
          <label className="form-label">Email:</label>
          <input type="email" className="form-input" />
        </div>
        <div className="form-group">
          <label className="form-label">Password:</label>
          <input type="password" className="form-input" />
        </div>
        <button type="submit" className="form-button">
          Register
        </button>
      </form>
    </div>
  );
};

export default Register;
