import { test, expect, describe } from "bun:test";

process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = "0";
const baseUrl = "https://localhost:7188";
const authToken =
  "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZG1pbkBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiTWVtYmVyIiwiQWRtaW4iXSwiZXhwIjoxNzI2NjgyODEwfQ.JI-PrmhOM3uYuXlw_vubnlJgTaPBcu6992cPR8ckLmdAfiIFAWh7J92bYJyaSvQsYeqcvSoL9sBKPF2We6tZPw";

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
});
