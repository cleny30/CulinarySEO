import { Logo } from "@/assets/svg/logo";

export default function HeaderLogo() {
  return (
    <div className={``}>
      <Logo color="#222222"/>
    </div>
  );
}

// import { useDispatch } from "react-redux";
// import styles from "@/assets/css/home.module.css";
// import { Button } from "../button";
// import { useNavigate } from "react-router-dom";
// import { logout } from "@/redux/auth/apiRequest";
// import { printToast } from "@/utils/constants/toast/printToast";

// const dispatch = useDispatch();
// const navigate = useNavigate();
// const handleLogout = async () => {
//   await logout(dispatch, navigate).then(printToast);
// };
{
  /* Hello {user?.fullName}
{user && (
  <Button variant={"secondary"} type="button" onClick={handleLogout}>
    Logout
  </Button>
)} */
}
