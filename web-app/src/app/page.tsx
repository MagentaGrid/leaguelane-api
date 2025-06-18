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

      const handleSubmit = async (event: FormEvent) =>
      {
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
        <div className="min-h-screen flex flex-col md:flex-row font-sans">
         {/* Left Section */}
      <div className="w-1/2 bg-blue-200 flex items-center justify-center">
        <div className="text-center">
          <img src="/login.svg" alt="Tooth" className="w-72 mx-auto" />
        </div>
      </div>
  
        {/* Right section: Login form area */}
        <div className="w-full md:w-1/2 bg-white flex items-center justify-center p-8 rounded-lg shadow-lg">
          <div className="max-w-md w-full p-8 space-y-6 bg-white rounded-xl shadow-lg">
            {/* DentCare Logo/Brand */}
            <div className="text-center">
              <h1 className="text-4xl font-extrabold text-red-600 mb-2">DentCare<sup className="text-xl">&reg;</sup></h1>
              <p className="text-gray-600 text-lg">Nice to see you again</p>
            </div>
  
            {/* Login Form */}
            <form onSubmit={handleSubmit} className="space-y-4">
              {/* Username Input */}
              <div>
                <label htmlFor="username" className="block text-sm font-medium text-gray-700 mb-1">
                  User name <span className="text-red-500">*</span>
                </label>
                <input
                  type="text"
                  id="username"
                  name="username"
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                  placeholder="colinballinger@rm" // Placeholder from the image
                  className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm focus:outline-none"
                  required
                />
              </div>
  
              {/* Password Input */}
              <div>
                <label htmlFor="password" className="block text-sm font-medium text-gray-700 mb-1">
                  Password <span className="text-red-500">*</span>
                </label>
                <input
                  type="password" // Use type="password" for security
                  id="password"
                  name="password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  placeholder="colinballinger@rm" // Placeholder from the image
                  className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm focus:outline-none"
                  required
                />
              </div>
  
              {/* Remember Me & Forgot Password */}
              <div className="flex items-center justify-between">
                
                {/* Forgot Password Link */}
                <div className="text-sm">
                  <a href="#" className="font-medium text-blue-600 hover:text-blue-500">
                    Forgot password?
                  </a>
                </div>
              </div>
  
              {/* Sign-in Button */}
              <div>
                <button
                  type="submit"
                  className="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 transition duration-150 ease-in-out"
                >
                  Sign-in
                </button>
              </div>
            </form>
           
          </div>
        </div>
      </div>
    );
  }
 