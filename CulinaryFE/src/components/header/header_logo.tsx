import { Logo } from "@/assets/svg/logo";
import { Link } from "react-router-dom";

export default function HeaderLogo() {
  return (
    <Link to={"/"} className={`ml-2`}>
      <Logo color="#222222" className="not-lg:h-10"/>
    </Link>
  );
}

