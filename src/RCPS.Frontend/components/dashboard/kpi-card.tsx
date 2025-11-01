import { ReactNode } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { cn, formatCurrency } from "@/lib/utils";

interface KpiCardProps {
  title: string;
  value: number;
  delta?: number;
  currency?: string;
  icon?: ReactNode;
  className?: string;
}

export function KpiCard({
  title,
  value,
  delta,
  currency = "USD",
  icon,
  className
}: KpiCardProps) {
  const formatted = formatCurrency(value, currency);
  const hasDelta = typeof delta === "number";
  const deltaText =
    hasDelta && delta !== undefined
      ? `${delta >= 0 ? "+" : "-"}${Math.abs(delta).toFixed(1)}%`
      : null;

  return (
    <Card className={cn("bg-gradient-to-br from-slate-900 via-slate-800 to-slate-900 text-white", className)}>
      <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
        <CardTitle className="text-sm font-medium text-slate-300">{title}</CardTitle>
        {icon}
      </CardHeader>
      <CardContent>
        <div className="text-2xl font-semibold">{formatted}</div>
        {hasDelta && deltaText && (
          <p className="mt-2 text-xs text-slate-400">
            {deltaText} vs last period
          </p>
        )}
      </CardContent>
    </Card>
  );
}
