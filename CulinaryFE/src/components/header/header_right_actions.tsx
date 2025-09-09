import type { UserSession } from "@/types/userSession";
import { Button } from "../ui/button";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { logout } from "@/redux/auth/apiRequest";
import { printToast } from "@/utils/constants/toast/printToast";
import HeaderCart from "./headerCart";

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
    <div className="flex space-x-2 items-center">
      <HeaderCart user={user} />
      <div className="w-[1px] h-[30px] bg-mau-do-color border-0 mr-3"></div>
      {user ? (
        <Button variant={"outline"} type="button" onClick={handleLogout} className="not-lg:hidden">
          Logout
        </Button>
      ) : (
        <Button
          variant={"default"}
          size={"lg"}
          className="font-Lucky tracking-wider text-base not-lg:hidden"
          type="button"
          onClick={() => navigate("/login")}
        >
          Đăng nhập
        </Button>
      )}
    </div>
  );
}
