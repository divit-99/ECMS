import { useEffect, useState } from "react";
import { getCompanies } from "../api/company.api.ts";
import type { Company } from "../types/employee.types.ts";

const CACHE_KEY = "ecms_companies_cache";

interface UseCompaniesResult {
  companies: Company[];
  loading: boolean;
  error: string | null;
  refresh: () => Promise<void>;
}

export function useCompanies(): UseCompaniesResult {
  const [companies, setCompanies] = useState<Company[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // Load from cache first
  useEffect(() => {
    const cached = localStorage.getItem(CACHE_KEY);
    if (cached) {
      try {
        const parsed = JSON.parse(cached);
        setCompanies(parsed);
      } catch {
        // ignore cache parse errors
      }
    }
    refresh();
  }, []);

  const refresh = async () => {
    try {
      setLoading(true);
      setError(null);

      const result = await getCompanies();

      if (Array.isArray(result)) {
        setCompanies(result);
        localStorage.setItem(CACHE_KEY, JSON.stringify(result));
      } else {
        throw new Error("Invalid companies response");
      }
    } catch (err: any) {
      setError(err?.message || "Failed to load companies");
    } finally {
      setLoading(false);
    }
  };

  return {
    companies,
    loading,
    error,
    refresh,
  };
}
