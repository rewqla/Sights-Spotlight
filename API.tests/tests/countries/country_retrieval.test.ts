import { test, expect, describe, beforeAll } from "bun:test";
import { CountryService } from "../../services/countryService";
import { LoginService } from "../../services/loginService";
import { setupCountryService } from "../testSetup";

let countryService: CountryService;

beforeAll(async () => {
  countryService = await setupCountryService();
});

describe("GET /api/countries", () => {
  test("Should retrieve country list without token", async () => {
    // Arrange

    // Act
    const response = await countryService.getAllCountries();
    // Assert
    expect(response.status).toBe(200);
  });

  test("Should retrieve country list with token", async () => {
    // Arrange

    // Act
    const response = await countryService.getAllCountries();

    // Assert
    expect(response.status).toBe(200);

    expect(Array.isArray(response.body)).toBe(true);
    expect(response.body!.length).toBeGreaterThan(0);
  });
});
