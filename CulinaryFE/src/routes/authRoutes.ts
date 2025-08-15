import LoginPage from "@/pages/Auth/login/page";
import RegisterPage from "@/pages/Auth/register/page";
import VerifyOtpPage from "@/pages/Auth/verify-otp/page";

const authRoutes = [
  { path: "/login", name: "login", component: LoginPage },
  { path: "/register", name: "register", component: RegisterPage },
  { path: "/verify-otp", name: "verify-otp", component: VerifyOtpPage },
];

export default authRoutes;
