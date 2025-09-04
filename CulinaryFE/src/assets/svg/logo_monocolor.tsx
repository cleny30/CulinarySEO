import { motion, type HTMLMotionProps } from "framer-motion";

export const LogoMonoColor = (props: HTMLMotionProps<"img">) => {
  return (
    <motion.img src="/img/Simplification.webp" alt="bg_logo_img" {...props} />
  );
};
