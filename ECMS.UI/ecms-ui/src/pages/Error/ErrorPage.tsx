import { Box, Typography, Button } from "@mui/material";
import ReportProblemIcon from "@mui/icons-material/ReportProblem";
import { useLocation, useNavigate } from "react-router-dom";

export default function ErrorPage() {
  const navigate = useNavigate();
  const { search } = useLocation();
  const params = new URLSearchParams(search);
  const type = params.get("type");

  const getMessage = () => {
    switch (type) {
      case "server-down":
        return "The server is currently unreachable. Please try again in a moment.";
      case "server-error":
        return "The server encountered an unexpected error.";
      default:
        return "Something went wrong. Please try again.";
    }
  };

  return (
    <Box
      sx={{
        minHeight: "70vh",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        textAlign: "center",
        px: 2,
      }}
    >
      <ReportProblemIcon sx={{ fontSize: 70, color: "error.main" }} />

      <Typography variant="h4" sx={{ mt: 2, mb: 1 }}>
        Oops!
      </Typography>

      <Typography variant="body1" sx={{ mb: 4, maxWidth: 400 }}>
        {getMessage()}
      </Typography>

      <Button variant="contained" onClick={() => navigate("/")}>
        Go to Home
      </Button>
    </Box>
  );
}
