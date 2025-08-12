import HomePage from "@/pages/Home/page";

// Public routes are accessible to all users, regardless of authentication status.
const publicRoutes = [
  {
    path: "/",
    name: "Home",
    component: HomePage,
  },
];
export default publicRoutes;
