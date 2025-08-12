import styles from "@/assets/css/home.module.css";
import { Button } from "../ui/button";
import { logout } from "@/redux/auth/apiRequest";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import type { RootState } from "@/redux/store";
import toast from "@/utils/toast";
export default function Header() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const user = useSelector((state: RootState) => state.auth.login?.currentUser);
  const handleLogout = async () => {
    await logout(dispatch, navigate).then((result) => {
      if (result?.error) toast.error(result.error as string);
      if (result?.success) toast.success(result.success as string);
    });
  };

  return (
    <header>
      <div className="">
        <div className={`${styles.headerLogo} p-4`}>
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
