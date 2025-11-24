import { useEffect, useState } from "react";
import { Box, Container, Typography, Button, CircularProgress, Snackbar, Alert } from "@mui/material";

import AddIcon from "@mui/icons-material/Add";

import SearchBar from "../../components/common/SearchBar";
import Pagination from "../../components/common/Pagination";
import EmployeeList from "../../components/employee/EmployeeList";
import EmployeeFormModal from "../../components/employee/EmployeeFormModal";

import { getEmployees, updateEmployee, deleteEmployee, createEmployee } from "../../api/employee.api";

import { getCompanies } from "../../api/company.api";
import type { Employee, Company } from "../../types/employee.types";

const ITEMS_PER_PAGE = 10;

export default function Dashboard() {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [companies, setCompanies] = useState<Company[]>([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [search, setSearch] = useState("");
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingEmployee, setEditingEmployee] = useState<Employee | null>(null);
  const [totalCount, setTotalCount] = useState(0);

  const [toastOpen, setToastOpen] = useState(false);
  const [toastMessage, setToastMessage] = useState("");
  const [toastSeverity, setToastSeverity] = useState<"success" | "error">( "success" );

  const showToast = (
    msg: string,
    severity: "success" | "error" = "success"
  ) => {
    setToastMessage(msg);
    setToastSeverity(severity);
    setToastOpen(true);
  };

  // Debounce search on user input searchTerm
  useEffect(() => {
    const timer = setTimeout(() => {
      setSearch(searchTerm);
      setPage(1);
    }, 400);

    return () => clearTimeout(timer);
  }, [searchTerm]);

  // Mapping helper
  const mapEmployee = (e: any): Employee => ({
    id: e.id,
    name: e.fullName ?? e.name,
    email: e.email,
    phone: e.phone,
    jobTitle: e.jobTitle,
    companyId: e.companyId,
    companyName: e.companyName,
    isActive: Boolean(e.isActive),
  });

  // Load companies
  useEffect(() => {
    getCompanies()
      .then(setCompanies)
      .catch(() => {});
  }, []);

  // Load employees
  useEffect(() => {
    setLoading(true);
    setError(null);

    getEmployees(page, ITEMS_PER_PAGE, search)
      .then((data) => {
        const items = Array.isArray(data.data) ? data.data : [];
        setEmployees(items.map(mapEmployee));
        setTotalPages(Math.ceil(data.totalCount / ITEMS_PER_PAGE));
        setTotalCount(data.totalCount);
      })
      .catch(() => {
        setError("Failed to load employees");
      })
      .finally(() => setLoading(false));
  }, [page, search]);

  // Handle Add / Edit Save
  const handleSave = async (data: Partial<Employee>) => {
    try {
      if (editingEmployee) {
        await updateEmployee(editingEmployee.id, data);
        showToast("Employee updated", "success");
      } else {
        await createEmployee(data);
        showToast("Employee created", "success");
      }

      setModalOpen(false);
      setEditingEmployee(null);

      // Rerender post save
      const res = await getEmployees(page, ITEMS_PER_PAGE, search);
      const items = Array.isArray(res.data) ? res.data : [];
      setEmployees(items.map(mapEmployee));
    } catch (err) {
      showToast("Save failed", "error");
      console.error(err);
    }
  };

  // Handle delete
  const handleDelete = async (id: number) => {
    if (!window.confirm("Are you sure you want to delete this employee?"))
      return;

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
            {totalCount} employees found
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

      <SearchBar value={searchTerm} onChange={setSearchTerm} />

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

          <Pagination page={page} totalPages={totalPages} onChange={setPage} />
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
