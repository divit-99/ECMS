import { useEffect } from "react";
import {
  MenuItem,
  TextField,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  FormControlLabel,
  Checkbox,
} from "@mui/material";
import {
  useForm,
  Controller,
  type SubmitHandler,
  type Resolver,
} from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import type { Employee, Company } from "../../types/employee.types";
import {
  employeeValidationSchema,
  type EmployeeFormData,
} from "../../validations/employeeValidation";

interface EmployeeFormModalProps {
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
}: EmployeeFormModalProps) {
  const isEditMode = !!employee;

  const {
    control,
    handleSubmit,
    reset,
    formState: { isValid, isDirty },
  } = useForm<EmployeeFormData>({
    resolver: yupResolver(
      employeeValidationSchema
    ) as unknown as Resolver<EmployeeFormData>,
    mode: "onChange",
    defaultValues: {
      name: "",
      email: "",
      phone: "",
      jobTitle: "",
      companyId: companies && companies.length ? companies[0].id : 1,
      isActive: true,
    },
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
        companyId: companies && companies.length ? companies[0].id : 1,
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
            render={({ field, fieldState }) => (
              <TextField
                {...field}
                id="name"
                label="Full name"
                required
                fullWidth
                margin="normal"
                error={!!fieldState.error}
                helperText={fieldState.error?.message}
              />
            )}
          />

          <Controller
            name="email"
            control={control}
            render={({ field, fieldState }) => (
              <TextField
                {...field}
                id="email"
                label="Email"
                required
                fullWidth
                margin="normal"
                error={!!fieldState.error}
                helperText={fieldState.error?.message}
              />
            )}
          />

          <Controller
            name="phone"
            control={control}
            render={({ field, fieldState }) => (
              <TextField
                {...field}
                id="phone"
                label="Phone"
                fullWidth
                margin="normal"
                error={!!fieldState.error}
                helperText={fieldState.error?.message}
              />
            )}
          />

          <Controller
            name="jobTitle"
            control={control}
            render={({ field, fieldState }) => (
              <TextField
                {...field}
                id="jobTitle"
                label="Job Title"
                fullWidth
                margin="normal"
                error={!!fieldState.error}
                helperText={fieldState.error?.message}
              />
            )}
          />

          <Controller
            name="companyId"
            control={control}
            render={({ field, fieldState }) => (
              <TextField
                {...field}
                id="companyId"
                label="Company"
                select
                required
                fullWidth
                margin="normal"
                value={field.value}
                onChange={(e) =>
                  field.onChange(Number((e.target as HTMLInputElement).value))
                }
                error={!!fieldState.error}
                helperText={fieldState.error?.message}
              >
                {companies.map((company) => (
                  <MenuItem key={company.id} value={company.id}>
                    {company.companyName}
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
                control={
                  <Checkbox
                    {...field}
                    checked={!!field.value}
                    onChange={(e) => field.onChange(e.target.checked)}
                    inputProps={{ id: "isActive" }}
                  />
                }
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
            disabled={!isValid || !isDirty}
          >
            {isEditMode ? "Save" : "Add"}
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  );
}

export default EmployeeFormModal;
