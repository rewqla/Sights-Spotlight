import { HttpResponse } from "../interfaces/HttpResponse";

export class HttpService {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }

  async get<T>(
    endpoint: string,
    customHeaders: Record<string, string> = {}
  ): Promise<HttpResponse<T>> {
    return await this._request<T>("GET", endpoint, null, customHeaders);
  }

  async post<T>(
    endpoint: string,
    body: any,
    customHeaders: Record<string, string> = {}
  ): Promise<HttpResponse<T>> {
    return await this._request<T>("POST", endpoint, body, customHeaders);
  }

  async put<T>(
    endpoint: string,
    body: any,
    customHeaders: Record<string, string> = {}
  ): Promise<HttpResponse<T>> {
    return await this._request<T>("PUT", endpoint, body, customHeaders);
  }

  async delete<T>(
    endpoint: string,
    customHeaders: Record<string, string> = {}
  ): Promise<HttpResponse<T>> {
    return await this._request<T>("DELETE", endpoint, null, customHeaders);
  }

  private async _request<T>(
    method: string,
    endpoint: string,
    body: any = null,
    customHeaders: Record<string, string> = {}
  ): Promise<HttpResponse<T>> {
    const url = `${this.baseURL}${endpoint}`;

    const headers: Record<string, string> = {
      "Content-Type": "application/json",
      ...customHeaders,
    };

    const options: RequestInit = {
      method,
      headers,
      body: body ? JSON.stringify(body) : null,
    };

    try {
      const response = await fetch(url, options);
      let responseBody: T | null = null;

      if (response.status !== 204) {
        const contentType = response.headers.get("Content-Type") || "";
        if (contentType.includes("application/json")) {
          responseBody = await response.json(); // Only parse if content type is JSON
        }
      }

      return {
        status: response.status,
        body: responseBody,
      };
    } catch (error: any) {
      console.error(`Failed to ${method} ${url}:`, error.message);
      throw error;
    }
  }
}
