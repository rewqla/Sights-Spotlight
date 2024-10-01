import { test, expect, describe, beforeAll } from "bun:test";
import { CountryService } from "../../services/countryService";
import { LoginService } from "../../services/loginService";
import { generateRandomCountryName } from "../../utils/generateRandomCountryName";
import { setupCountryService } from "../testSetup";

let countryService: CountryService;

beforeAll(async () => {
  countryService = await setupCountryService();
});

describe("POST /api/countries", () => {
  test("Should create a new country with valid data and token", async () => {
    // Arrange
    const randomCountryName = generateRandomCountryName();
    const newCountryData = {
      name: randomCountryName,
      description: "Not bad as mad",
      mainImageURL: "23",
      secondaryImageURL: "2223",
      continent: "Antarctica",
    };

    // Act
    const response = await countryService.createCountry(newCountryData, true);

    // Assert
    console.log(response);
    expect(response.status).toBe(201);
    expect(response.body).toBeNumber();
  });
});
