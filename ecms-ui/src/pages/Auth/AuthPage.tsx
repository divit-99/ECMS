import { useNavigate } from "react-router-dom";
import AuthForm from "../../components/auth/AuthForm";
import { loginApi } from "../../api/auth.api";
import { useAuth } from "../../context/AuthContext";

export default function AuthPage() {
  const navigate = useNavigate();
  const { login } = useAuth();

  const handleLogin = async (data: any) => {
    try {
      const res = await loginApi(data);
      login(res.token, {
        fullName: res.fullName,
        isAdmin: res.isAdmin,
      });

      navigate("/");
    } catch (err) {
      alert("Invalid credentials");
    }
  };

  return <AuthForm onSubmit={handleLogin} />;
}
