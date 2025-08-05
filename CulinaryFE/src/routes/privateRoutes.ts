import HomePage from "../pages/Home/page";
// This file defines the private routes for the application.
// Private routes are typically accessible only to authenticated users.
const privateRoutes = [
    {
    path: "/",
    name: "Home",
    component: HomePage,
    allowPermissions: [""], 
  },
]
export default privateRoutes;