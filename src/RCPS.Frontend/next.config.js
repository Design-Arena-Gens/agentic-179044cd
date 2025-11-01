/** @type {import('next').NextConfig} */
const nextConfig = {
  output: 'standalone',
  reactStrictMode: true,
  images: {
    unoptimized: true
  },
  experimental: {
    typedRoutes: true
  }
};

module.exports = nextConfig;
