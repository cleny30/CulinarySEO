import { useSelector } from "react-redux";
import { Navigate, Outlet } from "react-router-dom";
import type { RootState } from "@/redux/store";


const AuthRoute = () => {
    const currentUser = useSelector((state: RootState) => state.auth.login.currentUser);
    return currentUser ? <Navigate to="/" /> : <Outlet />;
};

export default AuthRoute;
