import { LoginSchema, type LoginSchemaType } from "@/schemas/auth";
import { loginUser, logoutUser } from "@/services/authService";
import type { AppDispatch } from "../store";
import { type NavigateFunction } from "react-router-dom";
import {
  loginSuccess,
  logoutSuccess,
} from "./authSlice";
import { type UserSession } from "@/types/userSession";

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

  // Gọi hàm loginUser từ authService để thực hiện đăng nhập
  const result = await loginUser({ email, password });

  if (result.error) {
    return { error: result.error };
  }

  dispatch(loginSuccess(result.data as UserSession));
  navigate("/");

  return { success: "Đăng nhập thành công!" };
};

export const logout = async (
  dispatch: AppDispatch,
  navigate: NavigateFunction
) => {
  // Gọi hàm loginUser từ authService để thực hiện đăng nhập
  const result = await logoutUser();

  if (result?.error) {
    return { error: result.error };
  }

  dispatch(logoutSuccess());
  navigate("/login");

  return { success: "Đăng xuất thành công!" };
};
