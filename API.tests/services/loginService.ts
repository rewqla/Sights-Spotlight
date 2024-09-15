import { HttpResponse } from "../interfaces/HttpResponse";
import { LoginRequest } from "../interfaces/LoginRequest";
import { LoginResponse } from "../interfaces/LoginResponse";
import { HttpService } from "./httpService";

export class LoginService {
  private httpService: HttpService;

  constructor(baseURL: string) {
    this.httpService = new HttpService(baseURL);
  }

  async login(
    loginRequest: LoginRequest
  ): Promise<HttpResponse<LoginResponse>> {
    const endpoint = "/api/accounts/login";

    try {
      const response = await this.httpService.post<LoginResponse>(
        endpoint,
        loginRequest
      );

      return response;
    } catch (error: any) {
      console.error("Login failed:", error.message);
      throw error;
    }
  }
}
