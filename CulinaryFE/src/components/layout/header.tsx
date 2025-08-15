import { useSelector } from "react-redux";
import type { RootState } from "@/redux/store";
import HeaderLogo from "../ui/header/header_logo";
import HeaderNav from "../ui/header/header_nav";
import HeaderRightActions from "../ui/header/header_right_actions";
export default function Header() {
  const user =
    useSelector((state: RootState) => state.auth.login?.currentUser) || null;

  return (
    <header className="w-screen justify-center flex border-b-2 border-b-mau-do-color  absolute top-0">
      <div className="flex items-center w-full justify-between max-w-[1200px]">
        <HeaderLogo />
        <HeaderNav />
        <HeaderRightActions user={user}/>
      </div>
    </header>
  );
}
