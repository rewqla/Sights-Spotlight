import { test, expect, describe } from "bun:test";
import { LoginService } from "../../services/loginService";

process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = "0";
const baseUrl = "https://localhost:7188";
const loginService = new LoginService(baseUrl);

describe("Login Endpoint Tests", () => {
  test("POST /api/accounts/login - Successful login with valid credentials", async () => {
    // Arrange
    const loginData = {
      username: "admin",
      password: "12345678",
    };

    // Act
    const response = await loginService.login(loginData);

    // Assert
    expect(response.status).toBe(200);

    // console.log(response);

    expect(response.body).toHaveProperty("email");
    expect(response.body).toHaveProperty("token");
    expect(typeof response.body?.token).not.toBeEmpty();
  });

  test("POST /api/accounts/login - Invalid login with wrong credentials", async () => {
    // Arrange
    const loginData = {
      username: "invalidUser",
      password: "invalidPassword",
    };

    // Act
    const response = await loginService.login(loginData);

    // Assert
    expect(response.status).toBe(401);
  });

  test("POST /api/accounts/login - Login fails with missing username", async () => {
    // Arrange
    const loginData = {
      username: "",
      password: "validPassword",
    };

    // Act
    const response = await loginService.login(loginData);

    // Assert
    expect(response.status).toBe(401);
  });

  test("POST /api/accounts/login - Login fails with missing password", async () => {
    // Arrange
    const loginData = {
      username: "validUser",
      password: "",
    };

    // Act
    const response = await loginService.login(loginData);

    // Assert
    expect(response.status).toBe(401);
  });
});
