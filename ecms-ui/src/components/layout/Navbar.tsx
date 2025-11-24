import { AppBar, Toolbar, Typography, Button, Box } from "@mui/material";
import { useAuth } from "../../context/AuthContext";
import { useNavigate } from "react-router-dom";

export default function Navbar() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  return (
    <AppBar position="static" color="primary">
      <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
        <Typography variant="h6">Employee Contact Management System</Typography>

        <Box sx={{ display: "flex", alignItems: "center", gap: 8 }}>
          {user?.fullName && (
            <Typography variant="subtitle1">
              Welcome, {user.fullName}!
            </Typography>
          )}

          <Button
            color="inherit"
            onClick={() => {
              logout();
              navigate("/auth", { replace: true });
            }}
          >
            Logout
          </Button>
        </Box>
      </Toolbar>
    </AppBar>
  );
}
