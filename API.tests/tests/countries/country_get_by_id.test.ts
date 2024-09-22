import { test, expect, describe } from "bun:test";
import { CountryService } from "../../services/countryService";

process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = "0";
const baseUrl = "https://localhost:7188";
const authToken =
  "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZG1pbkBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiTWVtYmVyIiwiQWRtaW4iXSwiZXhwIjoxNzI3NTQ4NTM3fQ.ojDtuOhNzM3JKFIQS9oEQV3V15O7q1di_TIk_9RcOQSi3zsZpHeDC4uqkB2B1gaMSas_-o3t3RwYbuedlMoSrg";
const countryService = new CountryService(baseUrl, authToken);

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
