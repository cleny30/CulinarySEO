import ProductLayout from "@/components/layout/productLayout";
import HomePage from "@/pages/Home/page";
import ShopDetail from "@/pages/ShopDetail/page";
import ShoppingPage from "@/pages/Shopping/page";

import type { RouteConfig } from "@/types";

// Public routes are accessible to all users, regardless of authentication status.
const publicRoutes: RouteConfig[] = [
  {
    path: "/",
    name: "Home",
    component: HomePage,
  },
  {
    path: "/shop",
    name: "Shop",
    component: ShoppingPage,
    layout: ProductLayout,
  },
    {
    path: "/shop/:productId",
    name: "ShopDetail",
    component: ShopDetail,
  },
];
export default publicRoutes;
