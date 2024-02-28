import { Link } from "react-router-dom";

const Header = () => {
  return (
    <header
      style={{ backgroundColor: "#282c34", padding: "20px", color: "white" }}
    >
      <h1 style={{ margin: 0 }}>Super header</h1>
      <nav>
        <ul
          style={{
            listStyleType: "none",
            padding: 0,
            margin: 0,
            display: "flex",
          }}
        >
          <li style={{ marginRight: "20px" }}>
            <Link to="/" style={{ color: "white", textDecoration: "none" }}>
              Home
            </Link>
          </li>
          <li style={{ marginRight: "20px" }}>
            <Link
              to="/register"
              style={{ color: "white", textDecoration: "none" }}
            >
              Register
            </Link>
          </li>
          <li style={{ marginRight: "20px" }}>
            <Link
              to="/login"
              style={{ color: "white", textDecoration: "none" }}
            >
              Login
            </Link>
          </li>
          <li style={{ marginRight: "20px" }}>
            <Link
              to="/countries"
              style={{ color: "white", textDecoration: "none" }}
            >
              Counties
            </Link>
          </li>
        </ul>
      </nav>
    </header>
  );
};

export default Header;
