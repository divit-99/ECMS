import * as yup from "yup";

export const employeeValidationSchema = yup.object({
  name: yup.string().required("Name is required"),
  email: yup.string().email("Invalid email").required("Email is required"),
  phone: yup.string().nullable(),
  jobTitle: yup.string().nullable(),
  companyId: yup.number().required("Company is required"),
  isActive: yup.boolean().required(),
});

export type EmployeeFormData = yup.InferType<typeof employeeValidationSchema>;
