import { useSelector } from "react-redux";
import { Navigate, Outlet, useLocation } from "react-router-dom";
import type { RootState } from "@/redux/store";

const AuthRoute = () => {
  const currentUser = useSelector(
    (state: RootState) => state.auth.login.currentUser
  );
  const userSignupInfo = useSelector(
    (state: RootState) => state.auth.signup.userSignupInfo
  );
  const location = useLocation();

  if (currentUser) {
    return <Navigate to="/" />;
  }

  if (location.pathname === "/verify-otp" && !userSignupInfo) {
    return <Navigate to="/login" />;
  }

  return <Outlet />;
};

export default AuthRoute;
