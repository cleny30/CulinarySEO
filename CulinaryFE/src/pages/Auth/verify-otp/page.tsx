"use client";
import HeaderCard from "@/components/auth/header-card";
import VerifyOtpContainer from "@/components/auth/verify-otp-container";

const VerifyOtpPage = () => {
  return (
    <div className="w-screen h-screen flex flex-col items-center justify-center bg-mau-sua-bo">
      <HeaderCard
        headerTitle="Verify OTP"
        headerSubTitle="OTP đã được gửi đến email của bạn, hãy kiểm tra và xác nhận."
      />
      <VerifyOtpContainer />
    </div>
  );
};

export default VerifyOtpPage;
