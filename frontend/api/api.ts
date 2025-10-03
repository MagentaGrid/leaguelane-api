class ApiClient {
  private baseURL: string
  private authToken: string | null = null

  constructor(baseURL: string) {
    this.baseURL = baseURL
  }

  setAuthToken(token: string) {
    this.authToken = token
  }

  private async request<T>(endpoint: string, options: RequestInit = {}): Promise<T> {
    const url = `${this.baseURL}${endpoint}`

    const config: RequestInit = {
      headers: {
        "Content-Type": "application/json",
        ...(this.authToken && { Authorization: `Bearer ${this.authToken}` }),
        ...options.headers,
      },
      ...options,
    }

    const response = await fetch(url, config)

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`)
    }

    return response.json()
  }

  async get<T>(endpoint: string): Promise<T> {
    if (!this.authToken) {
      throw new Error("Authentication required")
    }
    return this.request<T>(endpoint, { method: "GET" })
  }

  async getPublic<T>(endpoint: string): Promise<T> {
    return this.request<T>(endpoint, { method: "GET" })
  }

  async post<T>(endpoint: string, data: any): Promise<T> {
    if (!this.authToken) {
      throw new Error("Authentication required")
    }
    return this.request<T>(endpoint, {
      method: "POST",
      body: JSON.stringify(data),
    })
  }

  async put<T>(endpoint: string, data: any): Promise<T> {
    if (!this.authToken) {
      throw new Error("Authentication required")
    }
    return this.request<T>(endpoint, {
      method: "PUT",
      body: JSON.stringify(data),
    })
  }

  async delete<T>(endpoint: string): Promise<T> {
    if (!this.authToken) {
      throw new Error("Authentication required")
    }
    return this.request<T>(endpoint, { method: "DELETE" })
  }

  async authenticate(credentials: { username: string; password: string }): Promise<{ token: string }> {
    const response = await this.request<{ token: string }>("/auth/login", {
      method: "POST",
      body: JSON.stringify(credentials),
    })

    this.setAuthToken(response.token)
    return response
  }
}

// Create and export a singleton instance
const apiClient = new ApiClient(process.env.NEXT_PUBLIC_API_URL || "http://localhost:3000/api")

export default apiClient
