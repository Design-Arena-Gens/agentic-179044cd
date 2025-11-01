export const API_BASE_URL =
  process.env.NEXT_PUBLIC_API_BASE_URL ?? "http://localhost:5000/api";

async function fetchJson<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    ...init,
    headers: {
      "Content-Type": "application/json",
      ...(init?.headers ?? {})
    },
    cache: "no-store"
  });

  if (!response.ok) {
    const text = await response.text();
    throw new Error(`API error (${response.status}): ${text}`);
  }

  return response.json() as Promise<T>;
}

export interface DashboardSnapshotDto {
  totalRecognizedRevenue: number;
  totalCost: number;
  totalBilled: number;
  grossMarginPercentage: number;
  profitabilityTrend: Array<{
    period: string;
    revenue: number;
    cost: number;
    margin: number;
  }>;
  resourceUtilization: Array<{
    resourceName: string;
    allocated: number;
    utilized: number;
  }>;
  invoiceAging: Array<{
    label: string;
    amount: number;
  }>;
}

export interface ProjectSummaryDto {
  id: string;
  name: string;
  code?: string;
  clientId: string;
  status: number;
  budgetAmount: number;
  actualCost: number;
  recognizedRevenue: number;
  startDate: string;
  endDate?: string;
}

export interface ClientSummaryDto {
  id: string;
  name: string;
  primaryContactName?: string;
  primaryContactEmail?: string;
}

export const getDashboardSnapshot = () =>
  fetchJson<DashboardSnapshotDto>("/dashboard");

export const getProjects = () =>
  fetchJson<ProjectSummaryDto[]>("/projects");

export const getClients = () =>
  fetchJson<ClientSummaryDto[]>("/clients");
