import type { NextConfig } from 'next';

const nextConfig: NextConfig = {
  reactStrictMode: true,
  swcMinify: true,
  output: 'standalone',

  async rewrites() {
    return [
      {
        source: '/api/:path*',
        destination: 'http://api-service:5000/api/:path*', // Change this to your actual backend service name and port
      },
    ];
  },
};

export default nextConfig;
