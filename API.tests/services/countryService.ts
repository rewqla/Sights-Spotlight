import { HttpResponse } from "../interfaces/HttpResponse";
import { HttpService } from "./httpService";

export class CountryService {
  private httpService: HttpService;
  private authToken: string;

  constructor(baseURL: string, authToken: string) {
    this.httpService = new HttpService(baseURL);
    this.authToken = authToken;
  }

  async getAllCountries(useToken = false): Promise<HttpResponse<any[]>> {
    const endpoint = "/api/countries";

    const customHeaders: Record<string, string> = {};
    if (useToken) {
      customHeaders["Authorization"] = `Bearer ${this.authToken}`;
    }

    try {
      const response = await this.httpService.get<any[]>(
        endpoint,
        customHeaders
      );

      return response!;
    } catch (error: any) {
      console.error("Failed to fetch countries:", error.message);
      throw error;
    }
  }

  async getCountryById(
    countryId: number,
    useToken = false
  ): Promise<HttpResponse<any[]>> {
    const endpoint = `/api/countries/${countryId}`;

    const customHeaders: Record<string, string> = {};
    if (useToken) {
      customHeaders["Authorization"] = `Bearer ${this.authToken}`;
    }

    try {
      const response = await this.httpService.get<any[]>(
        endpoint,
        customHeaders
      );

      return response!;
    } catch (error: any) {
      console.error("Failed to fetch countries:", error.message);
      throw error;
    }
  }

  async createCountry(
    countryData: object,
    useToken = false
  ): Promise<HttpResponse<any>> {
    const endpoint = `/api/countries`;

    const customHeaders: Record<string, string> = {};
    customHeaders["Content-Type"] = `application/json`;
    if (useToken) {
      customHeaders["Authorization"] = `Bearer ${this.authToken}`;
    }

    try {
      const response = await this.httpService.post<any>(
        endpoint,
        countryData,
        customHeaders
      );
      return response!;
    } catch (error: any) {
      console.error("Failed to create country:", error.message);
      throw error;
    }
  }

  async updateCountry(
    countryData: object,
    countryId: number,
    useToken = false
  ): Promise<HttpResponse<any>> {
    const endpoint = `/api/countries/${countryId}`;

    const customHeaders: Record<string, string> = {};
    customHeaders["Content-Type"] = `application/json`;
    if (useToken) {
      customHeaders["Authorization"] = `Bearer ${this.authToken}`;
    }

    try {
      const response = await this.httpService.put<any>(
        endpoint,
        countryData,
        customHeaders
      );

      return response!;
    } catch (error: any) {
      console.error("Failed to create country:", error.message);
      throw error;
    }
  }
}
