import React, { type ReactNode } from "react";
import { Outlet, Navigate, useLocation } from "react-router-dom";
// import { useAuth } from "../context/AuthProvider";

interface ProtectedRouteProps {
  allowPermissions?: string[];
  children?: ReactNode;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({
  allowPermissions,
  children,
}) => {
  // const { isAuthenticated, getPermission } = null as any; // Replace with your auth context or hook
  // const location = useLocation();

  // if (!isAuthenticated()) {
  //   // Redirect to Login if the user is not authenticated
  //   return <Navigate to="/Login" state={{ from: location }} replace />;
  // }

  // const userPermissions = getPermission();

  // Check if the user has at least one of the required permissions
  const hasPermission = allowPermissions?
    // ? allowPermissions.some((permission) =>
    //     userPermissions.includes(permission)
    //   )
    true
    : true;

  if (!hasPermission) {
    // Redirect to home or an unauthorized page if the user lacks required permissions
    return <Navigate to="/Login" replace />;
  }

  // Render the child component if authenticated and has the required permissions
  return children || <Outlet />;
};

export default ProtectedRoute;
