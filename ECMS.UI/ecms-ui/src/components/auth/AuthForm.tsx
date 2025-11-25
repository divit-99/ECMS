import { Button, TextField, Box } from "@mui/material";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";

import {
  loginSchema,
  signupSchema,
  type LoginFormData,
  type SignupFormData,
} from "../../validations/authValidation";

interface AuthFormProps {
  mode: "login" | "signup";
  onLogin: (data: LoginFormData) => void;
  onSignup: (data: SignupFormData) => void;
}

export default function AuthForm({ mode, onLogin, onSignup }: AuthFormProps) {
  const schema = mode === "login" ? loginSchema : signupSchema;

  type AuthFormType = {
  fullName?: string;
  email: string;
  password: string;
};

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<AuthFormType>({
    resolver: yupResolver(schema),
  });

  const submitHandler = (data: AuthFormType) => {
    if (mode === "login") onLogin(data as LoginFormData);
    else onSignup(data as SignupFormData);
  };

  return (
    <Box component="form" onSubmit={handleSubmit(submitHandler)}>
      {mode === "signup" && (
        <TextField
          label="Full Name"
          fullWidth
          margin="normal"
          {...register("fullName")}
          error={!!errors.fullName}
          helperText={errors.fullName?.message}
        />
      )}

      <TextField
        label="Email"
        fullWidth
        margin="normal"
        {...register("email")}
        error={!!errors.email}
        helperText={errors.email?.message}
      />

      <TextField
        label="Password"
        type="password"
        fullWidth
        margin="normal"
        {...register("password")}
        error={!!errors.password}
        helperText={errors.password?.message}
      />

      <Button type="submit" variant="contained" fullWidth sx={{ mt: 2 }}>
        {mode === "login" ? "Login" : "Sign Up"}
      </Button>
    </Box>
  );
}
