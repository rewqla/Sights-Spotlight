// testSetup.ts

import { CountryService } from "../services/countryService";
import { LoginService } from "../services/loginService";

process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = "0";

const baseUrl = "https://localhost:7188";
let authToken = "";
const loginService = new LoginService(baseUrl);
let countryService: CountryService;

export async function setupCountryService() {
  if (!authToken) {
    // Login and get the token
    const loginData = {
      username: "admin",
      password: "12345678",
    };

    const loginResponse = await loginService.login(loginData);
    authToken = loginResponse.body?.token!;
  }

  // Initialize and return the CountryService
  if (!countryService) {
    countryService = new CountryService(baseUrl, authToken);
  }

  return countryService;
}
