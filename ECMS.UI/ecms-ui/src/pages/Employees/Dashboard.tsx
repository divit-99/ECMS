import { useEffect, useState } from "react";
import { Box, Container, Typography, Button, CircularProgress, Snackbar, Alert} from "@mui/material";

import AddIcon from "@mui/icons-material/Add";

import SearchBar from "../../components/common/SearchBar";
import Pagination from "../../components/common/Pagination";
import EmployeeList from "../../components/employee/EmployeeList";
import EmployeeFormModal from "../../components/employee/EmployeeFormModal";
import ConfirmDialog from "../../components/common/ConfirmDialog";

import { getEmployees, updateEmployee, deleteEmployee, createEmployee } from "../../api/employee.api";
import { useCompanies } from "../../hooks/useCompanies";
import type { Employee } from "../../types/employee.types";
import { mapEmployee } from "../../utils/mapEmployee";

const ITEMS_PER_PAGE = 10;

export default function Dashboard() {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [search, setSearch] = useState("");
  const [sortBy, setSortBy] = useState("name");
  const [sortDir, setSortDir] = useState<"asc" | "desc">("asc");
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [modalOpen, setModalOpen] = useState(false);
  const [editingEmployee, setEditingEmployee] = useState<Employee | null>(null);
  const [totalCount, setTotalCount] = useState(0);
  const [saving, setSaving] = useState(false);

  const [confirmOpen, setConfirmOpen] = useState(false);
  const [deleteId, setDeleteId] = useState<number | null>(null);
  const [deleting, setDeleting] = useState(false);

  const [toastOpen, setToastOpen] = useState(false);
  const [toastMessage, setToastMessage] = useState("");
  const [toastSeverity, setToastSeverity] = useState<"success" | "error">(
    "success"
  );

  const showToast = (
    msg: string,
    severity: "success" | "error" = "success"
  ) => {
    setToastMessage(msg);
    setToastSeverity(severity);
    setToastOpen(true);
  };

  const { companies } = useCompanies();

  // Debounce search on user input searchTerm
  useEffect(() => {
    const timer = setTimeout(() => {
      setSearch(searchTerm);
      setPage(1);
    }, 400);

    return () => clearTimeout(timer);
  }, [searchTerm]);

  // Load employees
  useEffect(() => {
    setLoading(true);
    setError(null);

    getEmployees(page, ITEMS_PER_PAGE, search, sortBy, sortDir)
      .then((data) => {
        const items = Array.isArray(data.data) ? data.data : [];
        setEmployees(items.map(mapEmployee));
        setTotalPages(Math.ceil(data.totalCount / ITEMS_PER_PAGE));
        setTotalCount(data.totalCount);
      })
      .catch(() => {
        setError("Failed to load employees!");
      })
      .finally(() => setLoading(false));
  }, [page, search, sortBy, sortDir]);

  // Sort handler
  const handleSort = (column: string) => {
    if (sortBy === column) {
      setSortDir((prev) => (prev === "asc" ? "desc" : "asc"));
    } else {
      setSortBy(column);
      setSortDir("asc");
    }

    setPage(1);
  };

  // Handle Add / Edit Save
  const handleSave = async (data: Partial<Employee>) => {
    try {
      setSaving(true);

      if (editingEmployee) {
        let res = await updateEmployee(editingEmployee.id, data);
        showToast(res.message ?? "Employee updated!", "success");
      } else {
        let res = await createEmployee(data);
        showToast(res.message ?? "Employee created!", "success");
      }

      setModalOpen(false);
      setEditingEmployee(null);

      // Rerender post save
      setPage(1);
      const res = await getEmployees(page, ITEMS_PER_PAGE, search);
      const items = Array.isArray(res.data) ? res.data : [];
      setEmployees(items.map(mapEmployee));
    } catch (err: any) {
      const apiMsg =
        err.response?.data?.error ||
        err.response?.data?.message ||
        "Save failed!";

      showToast(apiMsg, "error");
      console.error(err);
    } finally {
      setSaving(false);
    }
  };

  // Handle delete (open dialog)
  const requestDelete = (id: number) => {
    setDeleteId(id);
    setConfirmOpen(true);
  };

  const confirmDelete = async () => {
    if (!deleteId) return;
    setDeleting(true);

    try {
      const res = await deleteEmployee(deleteId);
      showToast(res.message ?? "Employee deleted!", "success");

      setEmployees((prev) => prev.filter((e) => e.id !== deleteId));
    } catch (err: any) {
      const apiMsg =
        err.response?.data?.error ||
        err.response?.data?.message ||
        "Delete failed!";

      showToast(apiMsg, "error");
    } finally {
      setConfirmOpen(false);
      setDeleteId(null);
      setDeleting(false);
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
            onDelete={(id) => requestDelete(id)}
            sortBy={sortBy}
            sortDir={sortDir}
            onSort={handleSort}
          />

          <Pagination page={page} totalPages={totalPages} onChange={setPage} />
        </>
      )}

      <EmployeeFormModal
        open={modalOpen}
        onClose={() => !saving && setModalOpen(false)}
        onSave={handleSave}
        employee={editingEmployee}
        companies={companies}
        saving={saving}
      />

      <Snackbar
        open={toastOpen}
        autoHideDuration={4000}
        onClose={() => setToastOpen(false)}
        anchorOrigin={{ vertical: "top", horizontal: "right" }}
      >
        <Alert
          onClose={() => setToastOpen(false)}
          severity={toastSeverity}
          variant="filled"
        >
          {toastMessage}
        </Alert>
      </Snackbar>

      <ConfirmDialog
        open={confirmOpen}
        title="Delete Employee"
        message="Are you sure you want to delete this employee?"
        onCancel={() => setConfirmOpen(false)}
        onConfirm={confirmDelete}
        deleting={deleting}
      />
    </Container>
  );
}
