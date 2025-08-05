import LoginPage from "../pages/Auth/Login/page";
// This file defines the public routes for the application.
// Public routes are accessible to all users, regardless of authentication status.
const publicRoutes = [
{
    path: "/login",
    name: "Login",
    component: LoginPage,
  },
]
export default publicRoutes;