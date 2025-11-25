import { useState } from "react";
import { Box, Paper, Typography, Tabs, Tab, Snackbar, Alert } from "@mui/material";

import AuthForm from "../../components/auth/AuthForm";
import { loginApi, signupApi } from "../../api/auth.api";
import { useAuth } from "../../context/AuthContext";

export default function AuthPage() {
  const { login } = useAuth();

  const [mode, setMode] = useState<"login" | "signup">("login");

  const [toastOpen, setToastOpen] = useState(false);
  const [toastMsg, setToastMsg] = useState("");
  const [toastSeverity, setToastSeverity] = useState<"success" | "error">(
    "success"
  );

  const showToast = (msg: string, sev: "success" | "error") => {
    setToastMsg(msg);
    setToastSeverity(sev);
    setToastOpen(true);
  };

  const handleLogin = async (data: any) => {
    try {
      const res = await loginApi(data);
      login(res.data.token, {
        fullName: res.data.fullName,
        isAdmin: res.data.isAdmin,
      });
      const apiMsg = res?.message || "Login successful!";
      showToast(apiMsg, "success");
    } catch (err: any) {
      const apiMsg =
        err?.response?.data?.message ||
        err?.response?.data?.error ||
        "Invalid credentials!";

      showToast(apiMsg, "error");
    }
  };

  const handleSignup = async (data: any) => {
    try {
      const res = await signupApi(data);
      showToast(res.message ?? "Signup successful", "success");
      setMode("login");
    } catch (err: any) {
      const apiMsg =
        err?.response?.data?.message ||
        err?.response?.data?.error ||
        "Signup failed!";

      showToast(apiMsg, "error");
    }
  };

  return (
    <Box
      sx={{
        maxWidth: 680,
        width: "100%",
        margin: "48px auto",
        px: 2
      }}
    >
      <Paper elevation={3} sx={{ p: 4 }}>
        <Typography variant="h3" align="center" gutterBottom>
          Employee Management
        </Typography>

        <Tabs value={mode} onChange={(_, v) => setMode(v)} variant="fullWidth" sx={{ mb: 3 }} aria-label="auth tabs">
          <Tab label="LOGIN" value="login" />
          <Tab label="SIGN UP" value="signup" />
        </Tabs>

        <Box sx={{ mb: 3 }}>
          <Typography variant="h6" align="center" >
            {mode === "login" ? "Sign in to manage employees" : "Create an account to get started"}
          </Typography>
          <Typography color="text.secondary" align="center" sx={{ mt: 1 }}>
            {mode === "login"
              ? "Don't have an account? Click the SIGN UP tab above"
              : "Already have an account? Click the LOGIN tab above"}
          </Typography>
        </Box>

        <AuthForm mode={mode} onLogin={handleLogin} onSignup={handleSignup} />
      </Paper>

      <Snackbar
        open={toastOpen}
        autoHideDuration={4000}
        onClose={() => setToastOpen(false)}
        anchorOrigin={{ vertical: "top", horizontal: "center" }}
      >
        <Alert severity={toastSeverity} variant="filled">
          {toastMsg}
        </Alert>
      </Snackbar>
    </Box>
  );
}
