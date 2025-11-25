import { createTheme, responsiveFontSizes } from "@mui/material/styles";

const appTheme = createTheme({
  palette: {
    primary: {
      main: "#1976d2",
    },
    secondary: {
      main: "#9c27b0",
    },
    background: {
      default: "#f5f7fa",
    },
  },

  typography: {
    fontFamily: `"Inter", "Roboto", "Helvetica", "Arial", sans-serif`,
    h4: { fontWeight: 600 },
    h6: { fontWeight: 600 },
    subtitle1: { fontWeight: 500 },
  },

  shape: {
    borderRadius: 8,
  },

  components: {
    // Buttons
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: "none",
          borderRadius: 6,
          fontWeight: 500,
        },
      },
    },

    // TextField default spacing
    MuiTextField: {
      defaultProps: {
        variant: "outlined",
      },
    },

    // Snackbar position (we’ll still override in component)
    MuiSnackbar: {
      styleOverrides: {
        root: {
          top: 16,
          right: 16,
        },
      },
    },

    // Paper shadows for Card, Dialog, Modals
    MuiPaper: {
      styleOverrides: {
        rounded: {
          borderRadius: 8,
        },
      },
    },
  },
});

export default responsiveFontSizes(appTheme);
