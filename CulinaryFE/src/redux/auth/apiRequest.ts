import { LoginSchema, type LoginSchemaType } from "@/schemas/auth";
import { loginUser } from "@/services/authService";
import type { AppDispatch } from "../store";
import { type NavigateFunction } from "react-router-dom";
import { loginFailure, loginStart, loginSuccess } from "./authSlice";
import { type User } from "@/types/user";

export const login = async (
  values: LoginSchemaType,
  dispatch: AppDispatch,
  navigate: NavigateFunction
) => {
  dispatch(loginStart());

  const validatedFields = LoginSchema.safeParse(values);
  if (!validatedFields.success) {
    return { error: "Dữ liệu không hợp lệ!" };
  }

  const { email, password } = validatedFields.data;

  // Gọi hàm loginUser từ authService để thực hiện đăng nhập
  const result = await loginUser({ email, password });

  if (result.error) {
    dispatch(loginFailure());
    return { error: result.error};
  }

  dispatch(loginSuccess(result.data as User));
  navigate("/");

  console.log(result);
  return { success: "Đăng nhập thành công!" };
};
