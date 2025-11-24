import axiosInstance from "../utils/axiosInstance";

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  fullName?: string;
  isAdmin?: boolean;
}

export const loginApi = async (data: LoginRequest): Promise<LoginResponse> => {
  const res = await axiosInstance.post("/Auth/login", data);
  return res.data.data;
};

export const registerApi = async (data: any) => {
  const res = await axiosInstance.post("/Auth/register", data);
  return res.data.data;
};
