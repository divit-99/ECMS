import type { Employee } from "../types/employee.types";

export const mapEmployee = (e: any): Employee => ({
  id: e.id,
  name: e.fullName ?? e.name,
  email: e.email,
  phone: e.phone,
  jobTitle: e.jobTitle,
  companyId: e.companyId,
  companyName: e.companyName,
  isActive: Boolean(e.isActive),
});
