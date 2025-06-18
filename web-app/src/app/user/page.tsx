// app/users/page.tsx
'use client';

import { useEffect, useState } from 'react';

type User = {
  userId: number;
  userName: string;
  firstName: string;
  lastName: string;
  role: number;
  phoneNumber?: string | null;
  active: boolean;
};

type ApiResponse = {
  users: User[];
  isSuccess: boolean;
  errorMessage?: string | null;
};

export default function UsersPage() {
  const [users, setUsers] = useState<User[]>([]);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetch(`${process.env.NEXT_PUBLIC_API_URL}/users`)
      .then((res) => res.json())
      .then((data: ApiResponse) => {
        if (data.isSuccess) {
          setUsers(data.users);
        } else {
          setError(data.errorMessage || 'Failed to load users');
        }
      })
      .catch(() => setError('Failed to fetch users'));
  }, []);

  if (error) return <div className="text-red-500">{error}</div>;

  return (
    <div className="p-6">
      <h1 className="text-2xl font-semibold mb-4">Users</h1>
      <table className="min-w-full border border-gray-300">
        <thead>
          <tr className="bg-gray-100">
            <th className="border px-4 py-2">ID</th>
            <th className="border px-4 py-2">Username</th>
            <th className="border px-4 py-2">Name</th>
            <th className="border px-4 py-2">Role</th>
            <th className="border px-4 py-2">Active</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr key={user.userId}>
              <td className="border px-4 py-2">{user.userId}</td>
              <td className="border px-4 py-2">{user.userName}</td>
              <td className="border px-4 py-2">
                {user.firstName} {user.lastName}
              </td>
              <td className="border px-4 py-2">{user.role}</td>
              <td className="border px-4 py-2">{user.active ? 'Yes' : 'No'}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
