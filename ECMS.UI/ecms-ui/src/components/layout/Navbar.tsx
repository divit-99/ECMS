import { AppBar, Toolbar, Typography, IconButton, Tooltip, Box, useMediaQuery } from "@mui/material";
import { useAuth } from "../../context/AuthContext";
import { useNavigate } from "react-router-dom";
import LogoutIcon from "@mui/icons-material/Logout";

export default function Navbar() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();
  const isMobile = useMediaQuery("(max-width: 768px)");;

  return (
    <AppBar position="static" color="primary">
      <Toolbar sx={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
        <Typography variant="h6" sx={{ fontWeight: 500 }}>
            {isMobile ? "ECMS" : "Employee Contact Management System"}
        </Typography>

        <Box sx={{ display: "flex", alignItems: "center", gap: 4 }}>
          {user?.fullName && (
            <Typography variant="subtitle1">
              Welcome, {user.fullName}!
            </Typography>
          )}

          <Tooltip title="Logout">
            <IconButton
              color="inherit"
              onClick={() => {
                logout();
                navigate("/auth", { replace: true });
              }}
            >
              <LogoutIcon />
            </IconButton>
          </Tooltip>
        </Box>
      </Toolbar>
    </AppBar>
  );
}
