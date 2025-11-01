export const dynamic = "force-dynamic";
export const revalidate = 0;

import { Suspense } from "react";
import { KpiCard } from "@/components/dashboard/kpi-card";
import { ProfitabilityChart } from "@/components/charts/profitability-chart";
import { UtilizationChart } from "@/components/charts/utilization-chart";
import { InvoiceAgingChart } from "@/components/charts/invoice-aging-chart";
import { ProjectTable } from "@/components/projects/project-table";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle
} from "@/components/ui/card";
import { Separator } from "@/components/ui/separator";
import type {
  DashboardSnapshotDto,
  ProjectSummaryDto,
  ClientSummaryDto
} from "@/lib/api";
import {
  getClients,
  getDashboardSnapshot,
  getProjects
} from "@/lib/api";
import { formatCurrency } from "@/lib/utils";
import { ArrowUpRight, LineChart, Users, Wallet } from "lucide-react";

const FALLBACK_DATA: {
  snapshot: DashboardSnapshotDto;
  projects: ProjectSummaryDto[];
  clients: ClientSummaryDto[];
} = {
  snapshot: {
    totalRecognizedRevenue: 520000,
    totalCost: 410000,
    totalBilled: 450000,
    grossMarginPercentage: 21.2,
    profitabilityTrend: [
      { period: new Date().toISOString(), revenue: 92000, cost: 57000, margin: 35000 },
      { period: new Date(Date.now() - 30 * 24 * 60 * 60 * 1000).toISOString(), revenue: 88000, cost: 54000, margin: 34000 },
      { period: new Date(Date.now() - 60 * 24 * 60 * 60 * 1000).toISOString(), revenue: 76000, cost: 51000, margin: 25000 },
      { period: new Date(Date.now() - 90 * 24 * 60 * 60 * 1000).toISOString(), revenue: 81000, cost: 52000, margin: 29000 }
    ],
    resourceUtilization: [
      { resourceName: "Jordan Maxwell", allocated: 320, utilized: 268 },
      { resourceName: "Priya Natarajan", allocated: 280, utilized: 240 }
    ],
    invoiceAging: [
      { label: "Current", amount: 90000 },
      { label: "1-30", amount: 35000 },
      { label: "31-60", amount: 12000 },
      { label: "61-90", amount: 8000 },
      { label: "90+", amount: 0 }
    ]
  },
  projects: [
    {
      id: "11111111-1111-1111-1111-111111111111",
      name: "Robotic Fulfillment Rollout",
      code: "ACM-ROBO-01",
      clientId: "99999999-9999-9999-9999-999999999999",
      status: 1,
      budgetAmount: 750000,
      actualCost: 410000,
      recognizedRevenue: 520000,
      startDate: new Date(Date.now() - 150 * 24 * 60 * 60 * 1000).toISOString(),
      endDate: undefined
    }
  ],
  clients: [
    {
      id: "99999999-9999-9999-9999-999999999999",
      name: "Acme Robotics",
      primaryContactName: "Dana White",
      primaryContactEmail: "dana.white@acmerobotics.co"
    }
  ]
} as const;

async function loadDashboardData() {
  const [snapshot, projects, clients] = await Promise.all([
    getDashboardSnapshot(),
    getProjects(),
    getClients()
  ]);

  return { snapshot, projects, clients };
}

export default async function DashboardPage() {
  let data: Awaited<ReturnType<typeof loadDashboardData>> = FALLBACK_DATA;

  try {
    data = await loadDashboardData();
  } catch (error) {
    console.error("Falling back to in-memory dashboard data.", error);
  }

  const { snapshot, projects, clients } = data;

  const revenueByClient = projects.reduce<Record<string, number>>((acc, project) => {
    acc[project.clientId] = (acc[project.clientId] ?? 0) + project.recognizedRevenue;
    return acc;
  }, {});

  return (
    <main className="mx-auto flex w-full max-w-7xl flex-1 flex-col gap-8 p-8 pb-16">
      <header className="flex flex-wrap items-center justify-between gap-4">
        <div>
          <p className="text-sm uppercase tracking-wide text-muted-foreground">
            Revenue Control & Project Suite
          </p>
          <h1 className="mt-2 text-3xl font-semibold">Operations Overview</h1>
        </div>
        <div className="flex items-center gap-3">
          <Button variant="outline">Export Report</Button>
          <Button className="gap-2">
            New Project
            <ArrowUpRight size={16} />
          </Button>
        </div>
      </header>

      <section className="grid gap-6 md:grid-cols-2 xl:grid-cols-4">
        <KpiCard
          title="Recognized Revenue"
          value={snapshot.totalRecognizedRevenue}
          delta={12.4}
          icon={<LineChart className="h-5 w-5 text-indigo-400" />}
        />
        <KpiCard
          title="Total Cost"
          value={snapshot.totalCost}
          delta={4.2}
          icon={<Wallet className="h-5 w-5 text-emerald-400" />}
          className="bg-gradient-to-br from-slate-900 via-slate-900 to-slate-800"
        />
        <KpiCard
          title="Billed Amount"
          value={snapshot.totalBilled}
          delta={8.1}
          icon={<Users className="h-5 w-5 text-cyan-400" />}
        />
        <Card className="bg-slate-900 text-white">
          <CardHeader className="pb-2">
            <CardTitle className="text-sm text-slate-300">Gross Margin</CardTitle>
          </CardHeader>
          <CardContent>
            <div className="text-4xl font-semibold">
              {snapshot.grossMarginPercentage.toFixed(1)}%
            </div>
            <p className="mt-2 text-xs text-slate-400">
              Margin trend is calculated across all active projects.
            </p>
          </CardContent>
        </Card>
      </section>

      <section className="grid gap-6 lg:grid-cols-5">
        <Card className="chart-card lg:col-span-3">
          <CardHeader>
            <CardTitle>Profitability Trend</CardTitle>
            <CardDescription>Revenue versus cost over the last quarters.</CardDescription>
          </CardHeader>
          <CardContent>
            <Suspense fallback={<div className="h-[300px] animate-pulse rounded-lg bg-slate-800/60" />}>
              <ProfitabilityChart data={snapshot.profitabilityTrend} />
            </Suspense>
          </CardContent>
        </Card>
        <Card className="lg:col-span-2">
          <CardHeader>
            <CardTitle>Invoice Aging</CardTitle>
            <CardDescription>Track outstanding balances across buckets.</CardDescription>
          </CardHeader>
          <CardContent>
            <Suspense fallback={<div className="h-[280px] animate-pulse rounded-lg bg-slate-800/40" />}>
              <InvoiceAgingChart data={snapshot.invoiceAging} />
            </Suspense>
          </CardContent>
        </Card>
      </section>

      <section className="grid gap-6 lg:grid-cols-5">
        <Card className="lg:col-span-3">
          <CardHeader>
            <CardTitle>Project Portfolio</CardTitle>
            <CardDescription>Financial performance at the project level.</CardDescription>
          </CardHeader>
          <CardContent className="pt-4">
            <ProjectTable projects={projects} />
          </CardContent>
        </Card>
        <Card className="lg:col-span-2">
          <CardHeader>
            <CardTitle>Resource Utilization</CardTitle>
            <CardDescription>Allocated hours vs actual delivery.</CardDescription>
          </CardHeader>
          <CardContent>
            <Suspense fallback={<div className="h-[260px] animate-pulse rounded-lg bg-slate-800/40" />}>
              <UtilizationChart data={snapshot.resourceUtilization} />
            </Suspense>
          </CardContent>
        </Card>
      </section>

      <section className="grid gap-4">
        <div className="flex items-center justify-between">
          <h2 className="text-lg font-semibold">Key Accounts</h2>
          <Button variant="ghost" className="text-muted-foreground">View clients</Button>
        </div>
        <Card>
          <CardContent className="p-0">
            <ul className="divide-y divide-border">
              {clients.map((client) => (
                <li key={client.id} className="flex items-center justify-between px-6 py-4">
                  <div>
                    <p className="text-sm font-medium">{client.name}</p>
                    <p className="text-xs text-muted-foreground">
                      {client.primaryContactName ?? "Unassigned"} · {client.primaryContactEmail ?? "No email"}
                    </p>
                  </div>
                <div className="text-sm text-muted-foreground">
                  {formatCurrency(revenueByClient[client.id] ?? 0)}
                </div>
                </li>
              ))}
              {clients.length === 0 && (
                <li className="px-6 py-8 text-center text-muted-foreground">
                  No clients available yet.
                </li>
              )}
            </ul>
          </CardContent>
        </Card>
      </section>

      <Separator />

      <footer className="flex flex-col gap-2 text-sm text-muted-foreground md:flex-row md:items-center md:justify-between">
        <span>© {new Date().getFullYear()} RCPS Professional Services Platform</span>
        <span>Clean architecture · ASP.NET Core API · Next.js interface</span>
      </footer>
    </main>
  );
}
