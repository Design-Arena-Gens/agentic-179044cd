import { Badge } from "@/components/ui/badge";

const statusStyles: Record<number, { label: string; variant: "success" | "warning" | "secondary" | "danger" | "default" }> = {
  0: { label: "Draft", variant: "secondary" },
  1: { label: "Active", variant: "success" },
  2: { label: "On Hold", variant: "warning" },
  3: { label: "Completed", variant: "default" },
  4: { label: "Cancelled", variant: "danger" }
};

interface ProjectStatusBadgeProps {
  status: number;
}

export function ProjectStatusBadge({ status }: ProjectStatusBadgeProps) {
  const config = statusStyles[status] ?? statusStyles[0];
  return <Badge variant={config.variant}>{config.label}</Badge>;
}
