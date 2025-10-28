import { lazy } from "react";
import ProductLayout from "@/components/layout/productLayout";

import ShopDetail from "@/pages/ShopDetail/page";
const ShoppingPage = lazy(() => import("@/pages/Shopping/page"));

import type { RouteConfig } from "@/types";
import HomePage from "@/pages/Home/page";

// Public routes are accessible to all users, regardless of authentication status.
const publicRoutes: RouteConfig[] = [
  {
    path: "/",
    name: "Home",
    component: HomePage,
  },
  {
    path: "/collections/all",
    name: "Shop",
    component: ShoppingPage,
    layout: ProductLayout,
  },
  {
    path: "/collections/:slug",
    name: "Shop",
    component: ShoppingPage,
    layout: ProductLayout,
  },
  {
    path: "/collections/:slug/:id",
    name: "ShopDetail",
    component: ShopDetail,
  },
];
export default publicRoutes;
