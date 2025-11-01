"use client";

import {
  Bar,
  BarChart,
  Legend,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis
} from "recharts";

interface UtilizationChartProps {
  data: Array<{
    resourceName: string;
    allocated: number;
    utilized: number;
  }>;
}

export function UtilizationChart({ data }: UtilizationChartProps) {
  return (
    <ResponsiveContainer width="100%" height={300}>
      <BarChart data={data}>
        <XAxis dataKey="resourceName" />
        <YAxis />
        <Tooltip />
        <Legend />
        <Bar dataKey="allocated" fill="#6366f1" />
        <Bar dataKey="utilized" fill="#22c55e" />
      </BarChart>
    </ResponsiveContainer>
  );
}
