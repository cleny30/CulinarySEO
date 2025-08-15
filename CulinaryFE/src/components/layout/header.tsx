import styles from "@/assets/css/home.module.css";
import { useSelector } from "react-redux";
import type { RootState } from "@/redux/store";
export default function Header() {
  const user = useSelector((state: RootState) => state.auth.login?.currentUser);
  // const handleLogout = async () => {
  //   await logout(dispatch, navigate).then(printToast);
  // };

  return (
    <header>
      <div className="">
        <div className={`${styles.headerLogo} p-4`}></div>
      </div>
    </header>
  );
}
