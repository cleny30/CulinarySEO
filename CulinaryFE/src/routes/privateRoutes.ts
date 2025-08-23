// This file defines the private routes for the application.

import AccountPage from "@/pages/accounts/page";
import type { RouteConfig } from "@/types";

// Private routes are typically accessible only to authenticated users.
const privateRoutes: RouteConfig[] = [
  {
    path: "/account",
    name: "Account",
    component: AccountPage,
    allowPermissions: [""],
  },
];
export default privateRoutes;
