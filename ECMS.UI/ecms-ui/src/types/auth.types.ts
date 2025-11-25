export interface AuthUser {
  fullName?: string;
  isAdmin?: boolean;
}

export interface AuthState {
  token: string | null;
  user: AuthUser | null;
}
