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
  MapPin,
  Menu,
  Plus
} from "lucide-react";
import { GoogleIcon } from "@/assets/svg/google";
import type { SVGProps, ImgHTMLAttributes } from "react";
import { SocialBg } from "@/assets/svg/socialBg";
import { FacebookIcon } from "@/assets/svg/facebook";
import { InstagramIcon } from "@/assets/svg/instagram";

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
  MapPin: (props: LucideProps) => <MapPin {...props} />,
  Menu: (props: LucideProps) => <Menu {...props} />,
  Plus: (props: LucideProps) => <Plus {...props} />,
  Authen: (props: ImgHTMLAttributes<HTMLImageElement>) => (
    <img src={"/svg/authen.svg"} {...props} />
  ),
  SocialBg: (props: SVGProps<SVGSVGElement>) => <SocialBg {...props} />,
  Facebook: (props: SVGProps<SVGSVGElement>) => <FacebookIcon {...props} />,
  Instagram: (props: SVGProps<SVGSVGElement>) => <InstagramIcon {...props} />,
};
