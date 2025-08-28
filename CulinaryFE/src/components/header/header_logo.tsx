import { Logo } from "@/assets/svg/logo";
import { Link } from "react-router-dom";

export default function HeaderLogo() {
  return (
    <Link to={"/"} className={``}>
      <Logo color="#222222" className="not-lg:h-12"/>
    </Link>
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
