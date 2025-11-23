import { useEffect } from "react";
import { MenuItem, TextField, Dialog, DialogTitle, DialogContent, DialogActions, Button, FormControlLabel, Checkbox } from "@mui/material";
import { useForm, Controller, type SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import type { Employee, Company } from "../../types/employee.types";
import { employeeValidationSchema, type EmployeeFormData } from "../../validations/employeeValidation";

interface Props {
  open: boolean;
  onClose: () => void;
  onSave: (data: Partial<Employee>) => void;
  employee?: Employee | null;
  companies: Company[];
}

function EmployeeFormModal({
  open,
  onClose,
  onSave,
  employee,
  companies,
}: Props) {
  const isEditMode = !!employee;

  const {
    control,
    handleSubmit,
    reset,
    formState: { isValid, errors },
  } = useForm<EmployeeFormData>({
    defaultValues: {
      name: "",
      email: "",
      phone: "",
      jobTitle: "",
      companyId: companies[0]?.id ?? 1,
      isActive: true,
    },
    resolver: yupResolver(employeeValidationSchema) as any, // <– Fix resolver type error
    mode: "onChange",
  });

  // Reset form when edit mode changes
  useEffect(() => {
    if (employee) {
      reset({
        name: employee.name,
        email: employee.email,
        phone: employee.phone ?? "",
        jobTitle: employee.jobTitle ?? "",
        companyId: employee.companyId,
        isActive: employee.isActive,
      });
    } else {
      reset({
        name: "",
        email: "",
        phone: "",
        jobTitle: "",
        companyId: companies[0]?.id ?? 1,
        isActive: true,
      });
    }
  }, [employee, companies, reset]);

  const onSubmit: SubmitHandler<EmployeeFormData> = (data) => {
    onSave(data);
  };

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>{isEditMode ? "Edit Employee" : "Add Employee"}</DialogTitle>

      <form onSubmit={handleSubmit(onSubmit)}>
        <DialogContent dividers>

          <Controller
            name="name"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Full Name"
                fullWidth
                margin="normal"
                error={!!errors.name}
                helperText={errors.name?.message}
              />
            )}
          />

          <Controller
            name="email"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Email"
                fullWidth
                margin="normal"
                error={!!errors.email}
                helperText={errors.email?.message}
              />
            )}
          />

          <Controller
            name="phone"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Phone"
                fullWidth
                margin="normal"
              />
            )}
          />

          <Controller
            name="jobTitle"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Job Title"
                fullWidth
                margin="normal"
              />
            )}
          />

          <Controller
            name="companyId"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                select
                label="Company"
                fullWidth
                margin="normal"
                onChange={(e) => field.onChange(Number(e.target.value))}
              >
                {companies.map((c) => (
                  <MenuItem key={c.id} value={c.id}>
                    {c.companyName}
                  </MenuItem>
                ))}
              </TextField>
            )}
          />

          <Controller
            name="isActive"
            control={control}
            render={({ field }) => (
              <FormControlLabel
                control={<Checkbox {...field} checked={!!field.value} />}
                label="Active"
              />
            )}
          />

        </DialogContent>

        <DialogActions>
          <Button onClick={onClose}>Cancel</Button>
          <Button
            type="submit"
            variant="contained"
            disabled={!isValid}
          >
            {isEditMode ? "Save" : "Add"}
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  );
}

export default EmployeeFormModal;
