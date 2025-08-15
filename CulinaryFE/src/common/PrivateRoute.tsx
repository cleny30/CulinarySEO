import type { RootState } from "@/redux/store";
import { useSelector } from "react-redux";
import { Navigate, Outlet } from "react-router-dom";

interface PrivateRouteProp {
  allowPermissions?: string[];
}

const PrivateRoute = ({ allowPermissions }: PrivateRouteProp) => {
  const hasPermission = allowPermissions || true;
  // ? allowPermissions.some((permission) =>
  //     userPermissions.includes(permission)
  // )
  const currentUser = useSelector(
    (state: RootState) => state.auth.login.currentUser
  );

  if (!currentUser) {
    return <Navigate to="/login" replace />;
  }

  if (currentUser && !hasPermission) {
    return <Navigate to="/pages/404" replace />;
  }

  // Render the child component if authenticated and has the required permissions
  return <Outlet />;
};
export default PrivateRoute;
