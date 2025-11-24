import axiosInstance from "../utils/axiosInstance";
import type { Employee } from "../types/employee.types";

export const getEmployees = async (
  pageNumber: number,
  pageSize: number,
  search?: string
) => {
  const params: any = { pageNumber, pageSize };
  if (search) params.search = search;

  const res = await axiosInstance.get("/Employee", { params });
  return res.data;
};

export const updateEmployee = async (id: number, data: Partial<Employee>) => {
  const res = await axiosInstance.put(`/Employee/${id}`, {
    fullName: data.name,
    email: data.email,
    phone: data.phone,
    jobTitle: data.jobTitle,
    companyId: data.companyId,
    isActive: data.isActive,
  });

  return res.data;
};

export const createEmployee = async (data: Partial<Employee>) => {
  const res = await axiosInstance.post("/Employee", {
    fullName: data.name,
    email: data.email,
    phone: data.phone,
    jobTitle: data.jobTitle,
    companyId: data.companyId,
    isActive: data.isActive,
  });

  return res.data;
};

export const deleteEmployee = async (id: number) => {
  await axiosInstance.delete(`/Employee/${id}`);
};
