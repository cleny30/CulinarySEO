import type { UserSession } from "@/types/userSession";
import { Button } from "../button";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { logout } from "@/redux/auth/apiRequest";
import { printToast } from "@/utils/constants/toast/printToast";

interface HeaderRightActionsProps {
  user: UserSession | null;
}
export default function HeaderRightActions({ user }: HeaderRightActionsProps) {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const handleLogout = async () => {
    await logout(dispatch, navigate).then(printToast);
  };

  return (
    <div>
      {user && (
        <Button variant={"secondary"} type="button" onClick={handleLogout}>
          Logout
        </Button>
      )}
    </div>
  );
}
