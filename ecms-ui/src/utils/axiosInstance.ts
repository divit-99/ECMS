import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "https://localhost:7899/api",
});

axiosInstance.interceptors.response.use(
  (res) => res,
  (err) => {
    if (err.response?.status === 401) {
      localStorage.removeItem("authToken");
      localStorage.removeItem("authUser");
      window.location.href = "/auth";
    }
    return Promise.reject(err);
  }
);

export default axiosInstance;
