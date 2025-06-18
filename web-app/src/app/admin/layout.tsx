export default function AdminLayout({ children }: { children: React.ReactNode }) {
    return (
      <div className="min-h-screen bg-gray-50">
        <nav className="bg-blue-700 text-white p-4">Admin Panel</nav>
        <main>{children}</main>
      </div>
    );
  }