'use client';

import { useState, FormEvent } from 'react';
import { useRouter } from 'next/navigation';

export default function LoginPage() {
  const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [error, setError] = useState<string | null>(null);
  const router = useRouter();

  // Base URL for the API from environment variables
  const API_URL = process.env.NEXT_PUBLIC_API_URL;

  const handleSubmit = async (event: FormEvent) => {
    event.preventDefault();
    setError(null); // Reset error message on new submission

    try {
      const response = await fetch(`${API_URL}/users/authenticate`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ username, password }),
      });

      if (!response.ok) {
        // Handle non-2xx responses
        throw new Error('Invalid username or password');
      }

      const data = await response.json();

      // Assuming the backend returns a token in a 'token' field
      if (data.token) {
        // Store the JWT in localStorage
        localStorage.setItem('jwt_token', data.token);
        // Redirect to the admin home page on successful login
        router.push('/admin/dashboard');
      } else {
        throw new Error('Token not found in response');
      }
    } catch (err: unknown) {

      // Set the error message to be displayed on the page
      if (err instanceof Error) {
        setError(err.message);
        console.error('Authentication failed:', err);
      } else {
        setError('An unknown error occurred');
        console.error('Authentication failed with unknown error:', err);
      }

    }
  };

  return (
    <main className="flex items-center justify-center min-h-screen bg-gray-100">
      <div className="w-full max-w-md p-8 space-y-6 bg-white rounded-lg shadow-md">
        <h1 className="text-2xl font-bold text-center text-gray-900">Admin Login</h1>
        
        <form onSubmit={handleSubmit} className="space-y-6">
          <div>
            <label
              htmlFor="username"
              className="block text-sm font-medium text-gray-700"
            >
              Username
            </label>
            <input
              id="username"
              name="username"
              type="text"
              required
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              className="w-full px-3 py-2 mt-1 text-gray-900 bg-gray-50 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
            />
          </div>

          <div>
            <label
              htmlFor="password"
              className="block text-sm font-medium text-gray-700"
            >
              Password
            </label>
            <input
              id="password"
              name="password"
              type="password"
              required
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="w-full px-3 py-2 mt-1 text-gray-900 bg-gray-50 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
            />
          </div>

          {/* Display error message if authentication fails */}
          {error && (
            <div className="p-3 text-sm text-center text-red-800 bg-red-100 border border-red-400 rounded-md">
              {error}
            </div>
          )}

          <div>
            <button
              type="submit"
              className="w-full px-4 py-2 font-semibold text-white bg-indigo-600 rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
            >
              Log In
            </button>
          </div>
        </form>
      </div>
    </main>
  );
}