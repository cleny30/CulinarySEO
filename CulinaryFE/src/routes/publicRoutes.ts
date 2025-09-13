import { lazy } from "react";
import ProductLayout from "@/components/layout/productLayout";

import ShopDetail from "@/pages/ShopDetail/page";
const ShoppingPage = lazy(() => import("@/pages/shopping/page"));

import type { RouteConfig } from "@/types";
import HomePage from "@/pages/home/page";

// Public routes are accessible to all users, regardless of authentication status.
const publicRoutes: RouteConfig[] = [
  {
    path: "/",
    name: "Home",
    component: HomePage,
  },
  {
    path: "/collection/all",
    name: "Shop",
    component: ShoppingPage,
    layout: ProductLayout,
  },
  {
    path: "/collection/:slug",
    name: "Shop",
    component: ShoppingPage,
    layout: ProductLayout,
  },
  {
    path: "/collection/:slug/:id",
    name: "ShopDetail",
    component: ShopDetail,
  },
];
export default publicRoutes;
