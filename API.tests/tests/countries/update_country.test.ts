import { test, expect, describe, beforeAll } from "bun:test";
import { CountryService } from "../../services/countryService";
import { setupCountryService } from "../testSetup";

let countryService: CountryService;

beforeAll(async () => {
  countryService = await setupCountryService();
});

describe("PUT /api/countries/{id}", () => {
  test("Should successfully update a country with valid data and token", async () => {
    // Arrange
    const countryId = 35;
    const updateCountryData = {
      id: countryId,
      name: "23123Italyyyius",
      description: "Not bad as mad2",
      mainImageURL: "23",
      secondaryImageURL: "2223",
      continent: "Asia",
    };

    // Act
    const response = await countryService.updateCountry(
      updateCountryData,
      countryId,
      true
    );

    expect(response.status).toBe(204);
  });

  describe("PUT /api/countries/{id} with unknown country", () => {
    test("Should return 404 for an unknown country ID", async () => {
      const unknownCountryId = 9999;
      const updateCountryData = {
        id: unknownCountryId,
        name: "UnknownLand",
        description: "This country does not exist",
        mainImageURL: "23",
        secondaryImageURL: "2223",
        continent: "Africa",
      };

      const response = await countryService.updateCountry(
        updateCountryData,
        unknownCountryId,
        true
      );

      // Assert
      expect(response.status).toBe(404);
    });
  });

  describe("PUT /api/countries/{id} with missing fields", () => {
    test("Should return 400 for missing required fields", async () => {
      // Arrange
      const countryId = 35;
      const incompleteUpdateData = {
        id: countryId,
        name: "",
        description: "Not bad as mad2",
        mainImageURL: "",
        continent: "Australia",
      };

      // Act
      const response = await countryService.updateCountry(
        incompleteUpdateData,
        countryId,
        true
      );

      // Assert
      expect(response.status).toBe(400);
    });
  });

  describe("PUT /api/countries/{id} without token", () => {
    test("Should return 401 for updating a country without authentication token", async () => {
      // Arrange
      const countryId = 35;
      const updateCountryData = {
        id: countryId,
        name: "CountryWithoutToken",
        description: "Trying to update without token",
        mainImageURL: "23",
        secondaryImageURL: "2223",
        continent: "Europe",
      };

      // Act
      const response = await countryService.updateCountry(
        updateCountryData,
        countryId,
        false
      );

      // Assert
      expect(response.status).toBe(401);
    });
  });
});
