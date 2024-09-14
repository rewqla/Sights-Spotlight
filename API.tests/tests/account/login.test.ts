import { test, expect, describe } from "bun:test";

process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = "0";
const baseUrl = "https://localhost:7188";

describe("Login Endpoint Tests", () => {
  test("POST /api/accounts/login - Successful login with valid credentials", async () => {
    // Arrange
    const loginData = {
      username: "admin",
      password: "12345678",
    };

    const endpoint = `${baseUrl}/api/accounts/login`;

    // Act
    const response = await fetch(endpoint, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(loginData),
    });

    // Assert
    expect(response.status).toBe(200);

    const data = await response.json();

    expect(data).toHaveProperty("email");
    expect(data).toHaveProperty("token");
    expect(typeof data.token).toBe("string");
  });

  test("POST /api/accounts/login - Invalid login with wrong credentials", async () => {
    // Arrange
    const loginData = {
      username: "invalidUser",
      password: "invalidPassword",
    };

    const endpoint = `${baseUrl}/api/accounts/login`;

    // Act
    const response = await fetch(endpoint, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(loginData),
    });

    // Assert
    expect(response.status).toBe(401);
  });

  test("POST /api/accounts/login - Login fails with missing username", async () => {
    // Arrange
    const loginData = {
      username: "",
      password: "validPassword",
    };

    const endpoint = `${baseUrl}/api/accounts/login`;

    // Act
    const response = await fetch(endpoint, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(loginData),
    });

    // Assert
    expect(response.status).toBe(401);
  });

  test("POST /api/accounts/login - Login fails with missing password", async () => {
    // Arrange
    const loginData = {
      username: "validUser",
      password: "",
    };

    const endpoint = `${baseUrl}/api/accounts/login`;

    // Act
    const response = await fetch(endpoint, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(loginData),
    });

    // Assert
    expect(response.status).toBe(401);
  });
});
