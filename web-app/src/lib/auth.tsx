// lib/auth.ts
export async function loginAdmin(username: string, password: string): Promise<string | null> {
    const res = await fetch("{{url}}/users/authenticate", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ username, password }),
    });
  
    if (!res.ok) return null;
  
    const data = await res.json();
    return data.token; // assuming { token: '...' }
  }
  