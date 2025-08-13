import HeaderCard from "@/components/auth/header-card";
import LoginForm from "@/components/auth/login-form";

export default function LoginPage() {
  return (
    <div className="w-screen h-screen flex flex-col items-center justify-start bg-stone-100 pt-40 pb-24">
      <HeaderCard
        headerTitle="Login"
        headerSubTitle="More than 1203 around the world"
      />
      <LoginForm />
    </div>
  );
}
