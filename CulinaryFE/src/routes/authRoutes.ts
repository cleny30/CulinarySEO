import AuthLayout from "@/components/layout/authLayout";
import LoginPage from "@/pages/Auth/login/page";
import RegisterPage from "@/pages/Auth/register/page";
import VerifyOtpPage from "@/pages/Auth/verify-otp/page";
import type { RouteConfig } from "@/types";

const authRoutes: RouteConfig[] = [
  { path: "/login", name: "login", component: LoginPage, layout: AuthLayout },
  {
    path: "/register",
    name: "register",
    component: RegisterPage,
    layout: AuthLayout,
  },
  {
    path: "/verify-otp",
    name: "verify-otp",
    component: VerifyOtpPage,
    layout: AuthLayout,
  },
];

export default authRoutes;
