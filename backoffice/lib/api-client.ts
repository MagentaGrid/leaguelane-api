// Bypass SSL check for local development (self-signed certs)
if (process.env.NODE_ENV === "development") {
    process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
}

const API_URL = process.env.API_URL || process.env.NEXT_PUBLIC_API_URL;

type FetchOptions = {
    method?: "GET" | "POST" | "PUT" | "DELETE";
    body?: any;
    headers?: Record<string, string>;
};

export async function apiClient<T>(endpoint: string, options: FetchOptions = {}): Promise<T> {

    // Ensure the endpoint starts with a slash if not provided
    const path = endpoint.startsWith("/") ? endpoint : `/${endpoint}`;

    // The user's env contained https://localhost:7487. 
    // If the user's API URL already has a trailing slash, handle it? 
    // Assuming cleaner URL concatenation:
    const baseUrl = API_URL?.replace(/\/$/, "");
    const url = `${baseUrl}${path}`;

    const headers: Record<string, string> = {
        "Content-Type": "application/json",
        ...options.headers,
    };

    const config: RequestInit = {
        method: options.method || "GET",
        headers,
        body: options.body ? JSON.stringify(options.body) : undefined,
        // Depending on self-signed certs in dev, we might need specific agent settings for Node,
        // but since Next.js default fetch is usually fine or requires `NODE_TLS_REJECT_UNAUTHORIZED=0` in dev env if purely local.
        // We won't add verified agents unless requested/error occurs.
    };

    console.log(url);
    console.log(config);
    const response = await fetch(url, config);

    if (!response.ok) {
        // Try to parse error message from API
        let errorMessage = `API Error: ${response.status} ${response.statusText}`;
        try {
            const errorData = await response.json();
            errorMessage = errorData.errorMessage || errorData.message || errorMessage;
        } catch {
            // ignore json parse error
        }
        throw new Error(errorMessage);
    }

    return response.json();
}
