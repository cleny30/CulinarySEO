import {
  Mail,
  Lock,
  Eye,
  EyeClosed,
  type LucideProps,
  UserLock,
  User,
  Smartphone,
  CornerLeftUp,
  CornerDownRight,
  ShoppingCart,
} from "lucide-react";
import { GoogleIcon } from "@/assets/svg/google";
import type { SVGProps, ImgHTMLAttributes } from "react";

export const Icon = {
  Email: (props: LucideProps) => <Mail {...props} />,
  Lock: (props: LucideProps) => <Lock {...props} />,
  Eye: (props: LucideProps) => <Eye {...props} />,
  EyeClosed: (props: LucideProps) => <EyeClosed {...props} />,
  Google: (props: SVGProps<SVGSVGElement>) => <GoogleIcon {...props} />,
  UserLock: (props: LucideProps) => <UserLock {...props} />,
  User: (props: LucideProps) => <User {...props} />,
  Smartphone: (props: LucideProps) => <Smartphone {...props} />,
  CornerLeftUp: (props: LucideProps) => <CornerLeftUp {...props} />,
  CornerDownRight: (props: LucideProps) => <CornerDownRight {...props} />,
  ShoppingCart: (props: LucideProps) => <ShoppingCart {...props} />,
  Authen: (props: ImgHTMLAttributes<HTMLImageElement>) => (
    <img src={"/authen.svg"} {...props} />
  ),
};
