export interface Employee {
  id: number;
  name: string;
  email: string;
  phone?: string | null;
  jobTitle?: string | null;
  companyId: number;
  companyName?: string;
  isActive: boolean;
  createdAt?: string;
}

export interface Company {
  id: number;
  companyName: string;
  domain?: string;
  industry?: string;
  website?: string;
}