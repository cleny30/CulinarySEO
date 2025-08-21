import HomePage from "@/pages/Home/page";
import ShoppingPage from "@/pages/Shopping/page";

// Public routes are accessible to all users, regardless of authentication status.
const publicRoutes = [
  {
    path: "/",
    name: "Home",
    component: HomePage,
  },
  {
    path: "/shop",
    name: "Shop",
    component: ShoppingPage,
  },
];
export default publicRoutes;
