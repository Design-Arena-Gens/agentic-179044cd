"use client";

import {
  Area,
  AreaChart,
  CartesianGrid,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis
} from "recharts";
import { formatCurrency } from "@/lib/utils";

interface ProfitabilityChartProps {
  data: Array<{
    period: string;
    revenue: number;
    cost: number;
    margin: number;
  }>;
}

export function ProfitabilityChart({ data }: ProfitabilityChartProps) {
  const chartData = data.map((item) => ({
    ...item,
    periodLabel: new Date(item.period).toLocaleDateString("en-US", {
      month: "short",
      year: "numeric"
    })
  }));

  return (
    <ResponsiveContainer width="100%" height={300}>
      <AreaChart data={chartData}>
        <defs>
          <linearGradient id="colorRevenue" x1="0" y1="0" x2="0" y2="1">
            <stop offset="5%" stopColor="#6366f1" stopOpacity={0.8} />
            <stop offset="95%" stopColor="#6366f1" stopOpacity={0} />
          </linearGradient>
          <linearGradient id="colorCost" x1="0" y1="0" x2="0" y2="1">
            <stop offset="5%" stopColor="#f97316" stopOpacity={0.8} />
            <stop offset="95%" stopColor="#f97316" stopOpacity={0} />
          </linearGradient>
        </defs>
        <CartesianGrid strokeDasharray="3 3" className="stroke-muted" />
        <XAxis dataKey="periodLabel" />
        <YAxis tickFormatter={(value) => formatCurrency(value).replace("$", "")} />
        <Tooltip
          formatter={(value: number) => formatCurrency(value)}
          labelFormatter={(label) => `Period: ${label}`}
        />
        <Area
          type="monotone"
          dataKey="revenue"
          stroke="#6366f1"
          fill="url(#colorRevenue)"
        />
        <Area
          type="monotone"
          dataKey="cost"
          stroke="#f97316"
          fill="url(#colorCost)"
        />
      </AreaChart>
    </ResponsiveContainer>
  );
}
