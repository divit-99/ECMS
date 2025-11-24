import * as yup from "yup";

export interface LoginFormData {
  email: string;
  password: string;
}

export const loginSchema = yup.object({
  email: yup.string().email("Invalid email format!").required("Email is required!")
            .matches(/^[^\s@]+@[^\s@]+\.[^\s@]+$/, "Invalid email format!"),
  password: yup.string().required("Password is required!"),
});

export interface SignupFormData {
  fullName: string;
  email: string;
  password: string;
}

export const signupSchema = yup.object({
  fullName: yup
    .string()
    .min(3, "Full name must be at least 3 characters!")
    .required("Full name is required!"),
  email: yup.string().email("Invalid email format!").required("Email is required!")
            .matches(/^[^\s@]+@[^\s@]+\.[^\s@]+$/, "Invalid email format!"),
  password: yup
    .string()
    .min(6, "Password must be at least 6 characters!")
    .required("Password is required!"),
});
