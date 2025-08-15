// This file defines the private routes for the application.

import AccountPage from "@/pages/Accounts/page";

// Private routes are typically accessible only to authenticated users.
const privateRoutes = [
  {
    path: "/account",
    name: "Account",
    component: AccountPage,
    allowPermissions: [""],
  },
];
export default privateRoutes;
