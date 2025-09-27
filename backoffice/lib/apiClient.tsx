
import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';

const axiosInstance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  timeout: 5000,
  headers: {
    'Content-Type': 'application/json',
  },
});

class ApiClient {
  private authToken: string | null = null;

  setAuthHeader(token: string) {
    this.authToken = token;
    axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    console.log('Authorization Header:', axiosInstance.defaults.headers.common['Authorization']);
  }

  clearAuthHeader() {
    this.authToken = null;
    delete axiosInstance.defaults.headers.common['Authorization'];
  }

  // For authentication (no auth header needed)
  async authenticate<TRequest, TResponse>(url: string, data: TRequest): Promise<TResponse> {
    const response: AxiosResponse<TResponse> = await axiosInstance.post(url, data);
    return response.data;
  }

  // Protected routes (requires auth header)
  async get<TResponse>(url: string, config?: AxiosRequestConfig): Promise<TResponse> {
    if (!this.authToken) {
      throw new Error('Authentication required');
    }
    const response: AxiosResponse<TResponse> = await axiosInstance.get(url, config);
    return response.data;
  }

  async post<TRequest, TResponse>(url: string, data: TRequest, config?: AxiosRequestConfig): Promise<TResponse> {
    if (!this.authToken) {
      throw new Error('Authentication required');
    }
    const response: AxiosResponse<TResponse> = await axiosInstance.post(url, data, config);
    return response.data;
  }

  async put<TRequest, TResponse>(url: string, data: TRequest, config?: AxiosRequestConfig): Promise<TResponse> {
    if (!this.authToken) {
      throw new Error('Authentication required');
    }
    const response: AxiosResponse<TResponse> = await axiosInstance.put(url, data, config);
    return response.data;
  }

  async delete<TResponse>(url: string, config?: AxiosRequestConfig): Promise<TResponse> {
    console.log('Attempting to delete:', url, config);
    if (!this.authToken) {
      throw new Error('Authentication required');
    }
    const response: AxiosResponse<TResponse> = await axiosInstance.delete(url, config);
    return response.data;
  }
}

export default new ApiClient();
