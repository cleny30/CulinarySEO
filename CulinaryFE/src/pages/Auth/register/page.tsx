import HeaderCard from "@/components/auth/header-card";
import SignUpForm from "@/components/auth/signup-form";

export default function RegisterPage() {
  return (
    <div className="w-screen h-screen flex flex-col items-center justify-center bg-mau-sua-bo">
      <HeaderCard headerTitle="Create your Account" />
      <SignUpForm />
    </div>
  );
}
