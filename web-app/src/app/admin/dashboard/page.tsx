'use client';

import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';

export default function AdminHomePage() {
  const [token, setToken] = useState<string | null>(null);
  const router = useRouter();

  useEffect(() => {
    // This check runs on the client side
    const storedToken = localStorage.getItem('jwt_token');
    if (!storedToken) {
      // If no token is found, redirect to the login page
      router.push('/admin/login');
    } else {
      setToken(storedToken);
    }
  }, [router]);

  const handleLogout = () => {
    // Clear the token and redirect to login
    localStorage.removeItem('jwt_token');
    router.push('/login');
  };

  // Render a loading state or null while checking for the token
  if (!token) {
    return (
        <div className="flex items-center justify-center min-h-screen">
            <p>Loading...</p>
        </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <nav className="flex items-center justify-between p-4 bg-white shadow-md">
        <h1 className="text-xl font-bold text-gray-800">Admin Dashboard</h1>
        <button
          onClick={handleLogout}
          className="px-4 py-2 font-semibold text-white bg-red-600 rounded-md hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500"
        >
          Logout
        </button>
      </nav>
      <main className="p-8">
        <div className="p-6 bg-white border border-gray-200 rounded-lg shadow-sm">
          <h2 className="text-2xl font-semibold text-gray-900">Welcome, Admin! 👋</h2>
          <p className="mt-2 text-gray-600">
            This is your dashboard. You can manage users, view reports, and configure settings from here.
          </p>
        </div>
      </main>
    </div>
  );
}