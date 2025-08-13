import { Mail, Lock, Eye, EyeClosed, type LucideProps } from "lucide-react";
import { GoogleIcon } from "@/assets/svg/google";
import type { SVGProps } from "react";

export const Icon = {
  Email: (props: LucideProps) => <Mail {...props} />,
  Lock: (props: LucideProps) => <Lock {...props} />,
  Eye: (props: LucideProps) => <Eye {...props} />,
  EyeClosed: (props: LucideProps) => <EyeClosed {...props} />,
  Google: (props: SVGProps<SVGSVGElement>) => <GoogleIcon {...props} />,
};
