import { Button, TextField, Paper, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";

export interface AuthFormData {
  email: string;
  password: string;
}

const schema = yup.object({
  email: yup.string().email().required(),
  password: yup.string().required(),
});

export default function AuthForm({
  onSubmit,
}: {
  onSubmit: (data: AuthFormData) => void;
}) {
  const { register, handleSubmit } = useForm<AuthFormData>({
    resolver: yupResolver(schema),
  });

  return (
    <Paper sx={{ p: 4, maxWidth: 400, mx: "auto", mt: 10 }}>
      <Typography variant="h5" mb={3}>
        Login
      </Typography>

      <form onSubmit={handleSubmit(onSubmit)}>
        <TextField
          label="Email"
          fullWidth
          margin="normal"
          {...register("email")}
        />

        <TextField
          label="Password"
          type="password"
          fullWidth
          margin="normal"
          {...register("password")}
        />

        <Button type="submit" variant="contained" fullWidth sx={{ mt: 2 }}>
          Login
        </Button>
      </form>
    </Paper>
  );
}
