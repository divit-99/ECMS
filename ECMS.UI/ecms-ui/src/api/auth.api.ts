import axiosInstance from "../utils/axiosInstance";

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  message: string;
  data: {
    token: string;
    fullName?: string;
    isAdmin?: boolean;
  };
}

export const loginApi = async (data: LoginRequest): Promise<LoginResponse> => {
  const res = await axiosInstance.post("/Auth/login", data);
  return res.data;
};

export async function signupApi(data: {
  fullName: string;
  email: string;
  password: string;
}) {
  const res = await axiosInstance.post("/Auth/signup", data);
  return res.data;
}
