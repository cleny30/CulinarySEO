import { useState, useEffect, useTransition } from "react";
import CardWrapper from "./card-wrapper";
import { InputOTP, InputOTPGroup, InputOTPSlot } from "../ui/input-otp";
import { Button } from "../ui/button";
import { useNavigate } from "react-router-dom";
import { resendOtp, verifyOtpAndRegister } from "@/redux/auth/apiRequest";
import type { RootState } from "@/redux/store";
import { useDispatch, useSelector } from "react-redux";
import { printToast } from "@/utils/constants/toast/printToast";

export default function VerifyOtpContainer() {
  const [pending, startTransition] = useTransition();
  const [otp, setOtp] = useState("");
  const [countdown, setCountdown] = useState(60);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const userSignupInfo = useSelector(
    (state: RootState) => state.auth.signup.userSignupInfo
  );

  const handleVerify = () => {
    if (userSignupInfo) {
      startTransition(async () => {
        await verifyOtpAndRegister(
          {
            email: userSignupInfo.email,
            otp: otp,
          },
          dispatch,
          navigate
        ).then(printToast);
      });
    }
  };

  const handleResendOtp = async () => {
    if (userSignupInfo) {
      await resendOtp(userSignupInfo).then(printToast);
      setCountdown(60);
    }
  };

  useEffect(() => {
    if (countdown > 0) {
      const timer = setTimeout(() => setCountdown(countdown - 1), 1000);
      return () => clearTimeout(timer);
    }
  }, [countdown]);

  return (
    <CardWrapper backButtonHref="/register" backButtonLabel="Quay lại">
      <div className="flex flex-col items-center space-y-2">
        <InputOTP
          maxLength={6}
          onComplete={(values) => {
            setOtp(values);
            handleVerify();
          }}
          className="w-full"
        >
          <InputOTPGroup className="bg-transparent px-4 py-2 w-full">
            <InputOTPSlot index={0} />
            <InputOTPSlot index={1} />
            <InputOTPSlot index={2} />
            <InputOTPSlot index={3} />
            <InputOTPSlot index={4} />
            <InputOTPSlot index={5} />
          </InputOTPGroup>
        </InputOTP>
        <div className="flex justify-end mb-4 w-full">
          <Button
            className="px-0"
            variant="link"
            onClick={handleResendOtp}
            disabled={countdown > 0}
          >
            {countdown > 0 ? `Gửi lại otp sau ${countdown}s` : "Gửi lại OTP"}
          </Button>
        </div>
        <Button
          onClick={handleVerify}
          className="cursor-pointer w-full"
          variant={"default"}
          size={"lg"}
          disabled={pending}
        >
          Xác nhận OTP
        </Button>
      </div>
    </CardWrapper>
  );
}
