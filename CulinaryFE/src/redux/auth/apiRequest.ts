import {
  LoginSchema,
  RegisterSchema,
  type LoginSchemaType,
  type RegisterSchemaType,
} from "@/schemas/auth";
import {
  loginUserService,
  logoutUserService,
  resendOtpService,
  signUpUserService,
  verifyOtpService,
} from "@/services/authService";
import type { AppDispatch } from "../store";
import { type NavigateFunction } from "react-router-dom";
import {
  loginSuccess,
  logoutSuccess,
  signupInProgress,
  signupSuccess,
} from "./authSlice";
import { type UserSession } from "@/types/userSession";
import type { UserRegister } from "@/types/userRegister";

// ===================================== Login Request =============================
export const login = async (
  values: LoginSchemaType,
  dispatch: AppDispatch,
  navigate: NavigateFunction
) => {
  const validatedFields = LoginSchema.safeParse(values);
  if (!validatedFields.success) {
    return { error: "Dữ liệu không hợp lệ!" };
  }

  const { email, password } = validatedFields.data;

  const result = await loginUserService({ email, password });

  if (result.error) {
    return { error: result.error };
  }

  dispatch(loginSuccess(result.data as UserSession));
  navigate("/");

  return { success: "Đăng nhập thành công!" };
};

// ===================================== Send OTP =============================
export const sentOtp = async (
  values: RegisterSchemaType,
  dispatch: AppDispatch,
  navigate: NavigateFunction
) => {
  const validatedFields = RegisterSchema.safeParse(values);
  if (!validatedFields.success) {
    return { error: "Dữ liệu không hợp lệ!" };
  }

  const { email, password, fullName, phone, repassword } = validatedFields.data;
  const localPart = email.split("@")[0];
  const parts = localPart.split(".");
  const username =
    parts[0] +
    parts
      .slice(1)
      .map((part) => part.charAt(0).toUpperCase() + part.slice(1))
      .join("");

  const result = await signUpUserService({
    email,
    password,
    fullName,
    phone,
    repassword,
    username
  });

  if (result.error) {
    return { error: result.error };
  }

  dispatch(
    signupInProgress({
      email,
      fullName,
      password,
      phone,
      username,
    } as UserRegister)
  );
  navigate("/verify-otp");

  return { success: "Hãy xác nhận otp để hoàn tất!" };
};
// ===================================== Verify Otp for Register =============================
export const verifyOtpAndRegister = async (
  values: { email: string; otp: string },
  dispatch: AppDispatch,
  navigate: NavigateFunction
) => {
  const result = await verifyOtpService({
    email: values.email,
    otp: values.otp,
  });

  if (result.error) {
    return { error: result.error };
  }

  dispatch(signupSuccess());
  navigate("/login");

  return { success: "Xác nhận email thành công, hãy đăng nhập vào nào!" };
};

// ===================================== Resend OTP =============================
export const resendOtp = async (userSignupInfo: UserRegister) => {
  const result = await resendOtpService(userSignupInfo);

  if (result.error) {
    return { error: result.error };
  }

  return { success: "Mã otp đã gửi, vui lòng kiểm tra hộp thư!" };
};

// ===================================== Log out ==================================
export const logout = async (
  dispatch: AppDispatch,
  navigate: NavigateFunction
) => {
  // Gọi hàm loginUser từ authService để thực hiện đăng nhập
  const result = await logoutUserService();

  if (result?.error) {
    return { error: result.error };
  }

  dispatch(logoutSuccess());
  navigate("/login");

  return { success: "Đăng xuất thành công!" };
};
