
import { Button } from "../ui/button";
import { Link } from "react-router-dom";

interface BackButtonProps {
  href: string;
  label: string;
}
export default function BackButton({ href, label }: BackButtonProps) {
  return (
    <Button variant="link" className="font-normal w-full" size="sm" asChild>
      <Link to={href}>{label}</Link>
    </Button>
  );
}
