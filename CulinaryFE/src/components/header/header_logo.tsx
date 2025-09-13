import { Logo } from "@/assets/svg/logo";
import { Link } from "react-router-dom";
import { motion } from "framer-motion";
import type { SVGMotionProps } from "framer-motion";

const MotionLink = motion(Link);

const HeaderLogo = (props: SVGMotionProps<SVGSVGElement>) => {
  return (
    <MotionLink to={"/"}>
      <Logo color="#222222" className="" {...props} />
    </MotionLink>
  );
};

export default HeaderLogo;
