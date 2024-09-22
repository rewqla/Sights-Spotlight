import { test, expect, describe } from "bun:test";
import { CountryService } from "../../services/countryService";

process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = "0";
const baseUrl = "https://localhost:7188";
const authToken =
  "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZG1pbkBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiTWVtYmVyIiwiQWRtaW4iXSwiZXhwIjoxNzI3NTQ4NTM3fQ.ojDtuOhNzM3JKFIQS9oEQV3V15O7q1di_TIk_9RcOQSi3zsZpHeDC4uqkB2B1gaMSas_-o3t3RwYbuedlMoSrg";
const countryService = new CountryService(baseUrl, authToken);

describe("countries retrieval", () => {
  test("GET /api/countries - Retrieve all countries without token", async () => {
    // Arrange
    const endpoint = `${baseUrl}/api/countries`;

    // Act
    const response = await fetch(endpoint, {
      method: "GET",
    });

    // Assert
    expect(response.status).toBe(200);
  });

  test("GET /api/countries - Retrieve all countries wit token", async () => {
    // Arrange
    const endpoint = `${baseUrl}/api/countries`;
    const headers = {
      Authorization: `Bearer ${authToken}`,
    };

    // Act
    const response = await fetch(endpoint, {
      method: "GET",
      headers,
    });

    // Assert
    expect(response.status).toBe(200);

    const data = await response.json();

    expect(Array.isArray(data)).toBe(true);
    expect(data.length).toBeGreaterThan(0);
  });

  test("GET /api/countries/ - Find by without token", async () => {
    // Arrange

    // Act
    const response = await countryService.getCountryById(3);

    // Assert
    expect(response.status).toBe(401);
  });

  test("GET /api/countries/ - Find by with real id", async () => {
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

  test("GET /api/countries/ - Find by with not real id", async () => {
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
});
