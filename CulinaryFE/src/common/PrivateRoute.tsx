import { Navigate, Outlet } from "react-router-dom";

interface PrivateRouteProp {
  allowPermissions?: string[];
}

const PrivateRoute = ({ allowPermissions }: PrivateRouteProp) => {
  const hasPermission = allowPermissions || true;
  // ? allowPermissions.some((permission) =>
  //     userPermissions.includes(permission)
  //   )

  if (!hasPermission) {
    // Redirect to home or an unauthorized page if the user lacks required permissions
    return <Navigate to="/Login" replace />;
  }

  // Render the child component if authenticated and has the required permissions
  return <Outlet />;
};
export default PrivateRoute;
