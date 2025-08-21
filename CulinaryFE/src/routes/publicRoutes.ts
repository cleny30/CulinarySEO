import HomePage from "@/pages/Home/page";
import ShopDetail from "@/pages/ShopDetail/page";
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
    {
    path: "/shop/:productId",
    name: "ShopDetail",
    component: ShopDetail,
  },
];
export default publicRoutes;
