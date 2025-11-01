"use client";

import { Cell, Pie, PieChart, ResponsiveContainer, Tooltip } from "recharts";
import { formatCurrency } from "@/lib/utils";

interface InvoiceAgingChartProps {
  data: Array<{
    label: string;
    amount: number;
  }>;
}

const colors = ["#22c55e", "#facc15", "#f97316", "#ef4444", "#a855f7"];

export function InvoiceAgingChart({ data }: InvoiceAgingChartProps) {
  const total = data.reduce((acc, item) => acc + item.amount, 0);
  const chartData = data.map((item) => ({
    ...item,
    percentage: total === 0 ? 0 : (item.amount / total) * 100
  }));

  return (
    <ResponsiveContainer width="100%" height={280}>
      <PieChart>
        <Tooltip formatter={(value: number) => formatCurrency(value)} />
        <Pie
          data={chartData}
          dataKey="amount"
          nameKey="label"
          cx="50%"
          cy="50%"
          outerRadius={110}
          label={(entry) =>
            `${entry.label}: ${entry.percentage.toFixed(1)}%`
          }
        >
          {chartData.map((_, index) => (
            <Cell key={index} fill={colors[index % colors.length]} />
          ))}
        </Pie>
      </PieChart>
    </ResponsiveContainer>
  );
}
