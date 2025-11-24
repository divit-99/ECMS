import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Chip, IconButton, Typography, Box } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import type { Employee } from "../../types/employee.types";

interface Props {
  employees: Employee[];
  onEdit: (emp: Employee) => void;
  onDelete: (id: number) => void;
  sortBy?: string;
  sortDir?: "asc" | "desc";
  onSort?: (field: string) => void;
}

export default function EmployeeList({ employees, onEdit, onDelete, sortBy, sortDir, onSort }: Props) {
  if (employees.length === 0) {
    return (
      <Box sx={{ textAlign: "center", py: 6 }}>
        <Typography color="text.secondary">No employees found</Typography>
      </Box>
    );
  }

  const handleSort = (field: string) => {
    if (onSort) onSort(field);
  };

  const arrow = (field: string) =>
    sortBy === field ? (sortDir === "asc" ? "↑" : "↓") : "";

  return (
    <TableContainer component={Paper} sx={{ maxHeight: 500 }}>
      <Table stickyHeader>
        <TableHead>
          <TableRow>
            <TableCell onClick={() => handleSort("name")} sx={{ fontWeight: 600, cursor: "pointer" }}>
              Name {arrow("name")}
            </TableCell>
            <TableCell onClick={() => handleSort("email")} sx={{ fontWeight: 600, cursor: "pointer" }}>
              Email {arrow("email")}
            </TableCell>
            <TableCell sx={{ fontWeight: 600 }}>Phone</TableCell>
            <TableCell onClick={() => handleSort("jobTitle")} sx={{ fontWeight: 600, cursor: "pointer" }}>
              Job Title {arrow("jobTitle")}
            </TableCell>
            <TableCell sx={{ fontWeight: 600 }}>Company</TableCell>
            <TableCell sx={{ fontWeight: 600 }}>Status</TableCell>
            <TableCell sx={{ fontWeight: 600 }} align="right">Actions</TableCell>
          </TableRow>
        </TableHead>

        <TableBody>
          {employees.map((e) => (
            <TableRow key={e.id} hover>
              <TableCell>{e.name}</TableCell>
              <TableCell>{e.email}</TableCell>
              <TableCell>{e.phone}</TableCell>
              <TableCell>{e.jobTitle}</TableCell>
              <TableCell>{e.companyName}</TableCell>
              <TableCell>
                <Chip
                  label={e.isActive ? "Active" : "Inactive"}
                  color={e.isActive ? "success" : "error"}
                  size="small"
                />
              </TableCell>
              <TableCell align="right">
                <IconButton
                  onClick={() => onEdit(e)}
                  size="small"
                  color="primary"
                >
                  <EditIcon />
                </IconButton>
                <IconButton
                  onClick={() => onDelete(e.id)}
                  size="small"
                  color="error"
                >
                  <DeleteIcon />
                </IconButton>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
