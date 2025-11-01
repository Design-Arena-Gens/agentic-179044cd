import { ProjectSummaryDto } from "@/lib/api";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow
} from "@/components/ui/table";
import { ProjectStatusBadge } from "./project-status";
import { formatCurrency } from "@/lib/utils";

interface ProjectTableProps {
  projects: ProjectSummaryDto[];
}

export function ProjectTable({ projects }: ProjectTableProps) {
  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead>Name</TableHead>
          <TableHead>Code</TableHead>
          <TableHead>Status</TableHead>
          <TableHead className="text-right">Budget</TableHead>
          <TableHead className="text-right">Actual Cost</TableHead>
          <TableHead className="text-right">Recognized Revenue</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {projects.map((project) => (
          <TableRow key={project.id}>
            <TableCell className="font-medium">{project.name}</TableCell>
            <TableCell>{project.code ?? "—"}</TableCell>
            <TableCell>
              <ProjectStatusBadge status={project.status} />
            </TableCell>
            <TableCell className="text-right">
              {formatCurrency(project.budgetAmount)}
            </TableCell>
            <TableCell className="text-right">
              {formatCurrency(project.actualCost)}
            </TableCell>
            <TableCell className="text-right">
              {formatCurrency(project.recognizedRevenue)}
            </TableCell>
          </TableRow>
        ))}
        {projects.length === 0 && (
          <TableRow>
            <TableCell colSpan={6} className="text-center text-muted-foreground">
              No projects found.
            </TableCell>
          </TableRow>
        )}
      </TableBody>
    </Table>
  );
}
