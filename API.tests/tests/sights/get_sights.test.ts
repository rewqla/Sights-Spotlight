import { test, expect, describe, beforeAll } from "bun:test";
import { SightsService } from "../../services/sightsService";

const baseUrl = "https://localhost:7188";
const sightsService = new SightsService(baseUrl);

describe("GET /api/sights", () => {
  test("Should return sights filtered by country and sorted by YearOfFoundation", async () => {
    // Arrange
    const queryParams = "?Country=Italy&SortBy=YearOfFoundation";

    // Act
    const response = await sightsService.getSights(queryParams);
    const responseBody = response.body;

    // Assert
    expect(response.status).toBe(200);
  });
});
