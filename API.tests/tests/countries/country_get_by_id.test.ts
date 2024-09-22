import { test, expect, describe, beforeAll } from "bun:test";
import { CountryService } from "../../services/countryService";
import { LoginService } from "../../services/loginService";
import { setupCountryService } from "../testSetup";

let countryService: CountryService;

beforeAll(async () => {
  countryService = await setupCountryService();
});

describe("GET /api/countries/{id}", () => {
  test("Should return 401 for unauthorized request without token", async () => {
    // Arrange

    // Act
    const response = await countryService.getCountryById(3);

    // Assert
    expect(response.status).toBe(401);
  });

  test("Should successfully retrieve country details with valid token and existing Id", async () => {
    // Arrange

    // Act
    const response = await countryService.getCountryById(3, true);

    // Assert
    expect(response.status).toBe(200);

    expect(response.body).toHaveProperty("id");
    expect(response.body).toHaveProperty("name");
    expect(response.body).toHaveProperty("imageURL");
    expect(response.body).toHaveProperty("description");
  });

  test("Should return 404 for non-existing country ID with valid token", async () => {
    // Arrange

    // Act
    const response = await countryService.getCountryById(-3, true);

    // Assert
    expect(response.status).toBe(404);
  });
});
