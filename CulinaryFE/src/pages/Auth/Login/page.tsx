import HeaderCard from "@/components/auth/header-card";
import LoginForm from "@/components/auth/login-form";

export default function LoginPage() {
  return (
    <div className="w-screen h-screen flex flex-col items-center justify-center">
      <HeaderCard headerTitle="Sign In Account"/>
      <LoginForm />
    </div>
  );
}
