import styles from "@/assets/css/home.module.css";
import { Button } from "../ui/button";
import { logout } from "@/redux/auth/apiRequest";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import type { RootState } from "@/redux/store";
import { printToast } from "@/utils/constants/toast/printToast";
export default function Header() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const user = useSelector((state: RootState) => state.auth.login?.currentUser);
  const handleLogout = async () => {
    await logout(dispatch, navigate).then(printToast);
  };

  return (
    <header>
      <div className="">
        <div className={`${styles.headerLogo} p-4`}>
          Hello {user?.fullName} 
          {user && (
            <Button variant={"secondary"} type="button" onClick={handleLogout}>
              Logout
            </Button>
          )}
        </div>
      </div>
    </header>
  );
}
