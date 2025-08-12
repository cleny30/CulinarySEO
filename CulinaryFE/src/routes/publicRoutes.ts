import LoginPage from "../pages/auth/login/page";
import HomePage from "../pages/home/page";
// This file defines the public routes for the application.
// Public routes are accessible to all users, regardless of authentication status.
const publicRoutes = [
  {
    path: "/login",
    name: "Login",
    component: LoginPage,
  },
  {
    path: "/",
    name: "Home",
    component: HomePage,
  },
];
export default publicRoutes;
