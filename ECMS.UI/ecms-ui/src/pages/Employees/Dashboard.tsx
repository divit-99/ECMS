import React from "react";
import { Box, Container, Typography, Button, CircularProgress, Snackbar, Alert } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import SearchBar from "../../components/common/SearchBar";
import Pagination from "../../components/common/Pagination";
import EmployeeList from "../../components/employee/EmployeeList";
import EmployeeFormModal from "../../components/employee/EmployeeFormModal";
import { getEmployees, updateEmployee, deleteEmployee } from "../../api/employee.api";
import { getCompanies } from "../../api/company.api";
import type { Employee, Company } from "../../types/employee.types";

const ITEMS_PER_PAGE = 10;

export default function Dashboard() {
  const [employees, setEmployees] = React.useState<Employee[]>([]);
  const [companies, setCompanies] = React.useState<Company[]>([]);
  const [search, setSearch] = React.useState("");
  const [page, setPage] = React.useState(1);
  const [loading, setLoading] = React.useState(true);
  const [error, setError] = React.useState<string | null>(null);
  const [modalOpen, setModalOpen] = React.useState(false);
  const [editingEmployee, setEditingEmployee] = React.useState<Employee | null>(null);
  const [toastOpen, setToastOpen] = React.useState(false);
  const [toastMessage, setToastMessage] = React.useState("");
  const [toastSeverity, setToastSeverity] = React.useState<"success" | "error">("success");

  const showToast = (msg: string, severity: "success" | "error" = "success") => {
    setToastMessage(msg);
    setToastSeverity(severity);
    setToastOpen(true);
  };

  React.useEffect(() => {
    getCompanies()
      .then((data) => setCompanies(data))
      .catch(() => {});
  }, []);

  React.useEffect(() => {
    setLoading(true);
    setError(null);

    getEmployees(page, ITEMS_PER_PAGE, search)
      .then((data) => {
        const items = Array.isArray(data.data) ? data.data : [];

        const mapped: Employee[] = items.map((e: any) => ({
          id: e.id,
          name: e.fullName ?? e.name,
          email: e.email,
          phone: e.phone,
          jobTitle: e.jobTitle,
          companyId: e.companyId,
          companyName: e.companyName,
          isActive: Boolean(e.isActive),
        }));

        setEmployees(mapped);
      })
      .catch((err) => {
        setError("Failed to load employees");
        console.error(err);
      })
      .finally(() => setLoading(false));
  }, [page, search]);

  const handleSave = async (data: Partial<Employee>) => {
    if (editingEmployee) {
      try {
        await updateEmployee(editingEmployee.id, data);
        showToast("Employee updated", "success");
        setModalOpen(false);
        setEditingEmployee(null);

        getEmployees(page, ITEMS_PER_PAGE, search).then((res) => {
          const list = res.data || [];
          setEmployees(
            list.map((e: any) => ({
              id: e.id,
              name: e.fullName ?? e.name,
              email: e.email,
              phone: e.phone,
              jobTitle: e.jobTitle,
              companyId: e.companyId,
              companyName: e.companyName,
              isActive: Boolean(e.isActive),
            }))
          );
        });
      } catch (err: any) {
        showToast("Update failed", "error");
        console.error(err);
      }
    } else {
      const newEmp: Employee = {
        id: Math.max(0, ...employees.map((e) => e.id)) + 1,
        name: data.name ?? "",
        email: data.email ?? "",
        phone: data.phone ?? "",
        jobTitle: data.jobTitle ?? "",
        companyId: data.companyId ?? companies[0]?.id ?? 1,
        companyName: companies.find((c) => c.id === data.companyId)?.companyName,
        isActive: data.isActive ?? true,
      };

      setEmployees((prev) => [...prev, newEmp]);
      showToast("Employee added (local)", "success");
      setModalOpen(false);
    }
  };

  const handleDelete = async (id: number) => {
    if (!window.confirm("Are you sure you want to delete this employee?")) return;

    try {
      await deleteEmployee(id);
      showToast("Employee deleted", "success");
      setEmployees((prev) => prev.filter((e) => e.id !== id));
    } catch (err) {
      showToast("Delete failed", "error");
    }
  };

  return (
    <Container maxWidth="lg" sx={{ py: 4 }}>
      <Box sx={{ display: "flex", justifyContent: "space-between", mb: 3 }}>
        <Box>
          <Typography variant="h4">Employee List</Typography>
          <Typography variant="body2" color="text.secondary">
            {employees.length} employees found
          </Typography>
        </Box>

        <Button
          variant="contained"
          startIcon={<AddIcon />}
          onClick={() => {
            setEditingEmployee(null);
            setModalOpen(true);
          }}
        >
          Add Employee
        </Button>
      </Box>

      <SearchBar value={search} onChange={setSearch} />

      {loading ? (
        <Box sx={{ display: "flex", justifyContent: "center", py: 8 }}>
          <CircularProgress />
        </Box>
      ) : error ? (
        <Typography color="error" sx={{ textAlign: "center", py: 5 }}>
          {error}
        </Typography>
      ) : (
        <>
          <EmployeeList
            employees={employees}
            onEdit={(emp) => {
              setEditingEmployee(emp);
              setModalOpen(true);
            }}
            onDelete={handleDelete}
          />

          <Pagination page={page} totalPages={10} onChange={setPage} />
        </>
      )}

      <EmployeeFormModal
        open={modalOpen}
        onClose={() => setModalOpen(false)}
        onSave={handleSave}
        employee={editingEmployee}
        companies={companies}
      />

      <Snackbar
        open={toastOpen}
        autoHideDuration={4000}
        onClose={() => setToastOpen(false)}
      >
        <Alert onClose={() => setToastOpen(false)} severity={toastSeverity}>
          {toastMessage}
        </Alert>
      </Snackbar>
    </Container>
  );
}
