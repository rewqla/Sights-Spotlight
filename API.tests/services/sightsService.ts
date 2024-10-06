import { HttpResponse } from "../interfaces/HttpResponse";
import { HttpService } from "./httpService";

export class SightsService {
  private httpService: HttpService;

  constructor(baseURL: string) {
    this.httpService = new HttpService(baseURL);
  }

  async getSights(query: string): Promise<HttpResponse<any[]>> {
    const endpoint = `/api/sights${query}`;

    try {
      const response = await this.httpService.get<any[]>(endpoint);

      return response!;
    } catch (error: any) {
      console.error("Failed to fetch countries:", error.message);
      throw error;
    }
  }
}
