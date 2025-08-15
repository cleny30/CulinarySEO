import { store_logo } from "@/utils/config/storeInfo";
import type { ImgHTMLAttributes } from "react";

export const Logo = (props: ImgHTMLAttributes<HTMLImageElement>) => {
  return <img {...props} src={store_logo} />;
};
