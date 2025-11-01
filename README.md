# RCPS Professional Services Platform

## Backend
- Location: `src/RCPS.Api`
- Stack: ASP.NET Core 8, EF Core, SQL Server

## Frontend
- Location: `src/RCPS.Frontend`
- Stack: Next.js 14 (React, TypeScript), Tailwind CSS, Recharts, ShadCN UI

## Development

### API
```bash
cd src/RCPS.Api
dotnet restore
dotnet run
```

### Frontend
```bash
cd src/RCPS.Frontend
npm install
npm run dev
```

Configure the environment variable `NEXT_PUBLIC_API_BASE_URL` to point to the API base URL (defaults to `http://localhost:5000/api`).
