import * as yup from "yup";

export const employeeValidationSchema = yup.object({
  name: yup.string().required("Name is required"),
  email: yup.string().email("Invalid email format").required("Email is required")
            .matches(/^[^\s@]+@[^\s@]+\.[^\s@]+$/, "Invalid email format"),
  phone: yup.string().nullable().optional(),
  jobTitle: yup.string().nullable().optional(),
  companyId: yup.number().required("Company is required"),
  isActive: yup.boolean().required(),
});

export type EmployeeFormData = yup.InferType<typeof employeeValidationSchema>;
