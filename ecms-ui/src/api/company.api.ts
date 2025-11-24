import axiosInstance from "../utils/axiosInstance";
import type { Company } from "../types/employee.types";

export const getCompanies = async (): Promise<Company[]> => {
  const res = await axiosInstance.get("/Company");
  return res.data;
};
