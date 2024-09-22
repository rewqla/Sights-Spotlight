import { test, expect, describe } from "bun:test";
import { LoginService } from "../../services/loginService";

process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = "0";
const baseUrl = "https://localhost:7188";
const loginService = new LoginService(baseUrl);

describe("POST /api/accounts/login", () => {
  test("Should successfully login with valid credentials", async () => {
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

  test("Should return 401 for invalid login credentials", async () => {
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

  test("Should return 401 when login fails due to missing username", async () => {
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

  test("Should return 401 when login fails due to missing password", async () => {
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
