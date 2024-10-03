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
    expect(response.status).toBe(201);
    expect(response.body).toBeNumber();
  });

  test("Should return 500 for invalid continent", async () => {
    // Arrange
    const randomCountryName = generateRandomCountryName();
    const newCountryData = {
      name: randomCountryName,
      description: "Not bad as mad",
      mainImageURL: "23",
      secondaryImageURL: "2223",
      continent: "InvalidContinent",
    };

    // Act
    const response = await countryService.createCountry(newCountryData, true);

    // Assert
    expect(response.status).toBe(500);
  });

  test("Should return 401 for creating a country without a token", async () => {
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
    const response = await countryService.createCountry(newCountryData, false); // No token

    // Assert
    expect(response.status).toBe(401); // Expecting unauthorized status
  });

  test("Should return 400 for missing required fields", async () => {
    const newCountryData = {
      name: "",
      description: "Not bad as mad",
      mainImageURL: "",
      secondaryImageURL: "2223",
      continent: "Antarctica",
    };

    const response = await countryService.createCountry(newCountryData, true);

    expect(response.status).toBe(400);
    expect(response.body.errors).toContainEqual(
      expect.objectContaining({
        propertyName: "Name",
        message: "The name must be greater than 3",
      })
    );
    expect(response.body.errors).toContainEqual(
      expect.objectContaining({
        propertyName: "MainImageURL",
        message: "The MainImageURL must be not empty",
      })
    );
  });

  test("Should return 400 for not unique name", async () => {
    // Arrange
    const existingCountryName = "Serbian";
    const newCountryData = {
      name: existingCountryName,
      description: "A description",
      mainImageURL: "url1",
      secondaryImageURL: "url2",
      continent: "Asia",
    };

    // Act
    const response = await countryService.createCountry(newCountryData, true);

    // Assert
    expect(response.status).toBe(400);
    expect(response.body.errors).toContainEqual(
      expect.objectContaining({
        propertyName: "Name",
        message: "Name is not unique",
      })
    );
  });
});
