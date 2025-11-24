import axios from "axios";

const axiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL
});

axiosInstance.interceptors.response.use(
  (res) => res,
  (err) => {
    if (!err.response) {
      // No network, server offline, CORS failure, timeout, DNS error, etc.
      console.error("Server unreachable:", err);
      window.location.href = "/error?type=server-down";
      return;
    }

    const status = err.response.status;

    // Unauthorized (Token invalid / expired)
    if (status === 401) {
      localStorage.removeItem("authToken");
      localStorage.removeItem("authUser");
      window.location.href = "/auth";
      return;
    }

    // Server Error (500+)
    if (status >= 500) {
      console.error("API Server Error:", err);
      window.location.href = "/error?type=server-error";
      return;
    }

    // Default: Pass error to caller
    return Promise.reject(err);
  }
);

export default axiosInstance;
