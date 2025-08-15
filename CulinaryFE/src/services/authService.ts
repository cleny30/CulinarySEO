import { type LoginSchemaType, type RegisterSchemaType } from "@/schemas/auth";
import type { ApiResponse, BackendApiResponse } from "@/types/api";
import type { UserRegister } from "@/types/userRegister";
import type { UserSession } from "@/types/userSession";
import { doRequest } from "@/utils/config/doRequest";
import { errorMessage } from "@/utils/constants/error/errorMessage";

export const loginUserService = async (
  loginInfo: LoginSchemaType
): Promise<ApiResponse<UserSession>> => {
  // TODO: Replace 'any' with a proper user/session type
  try {
    const response = await doRequest<BackendApiResponse<UserSession>>(
      "post",
      "/api/auth/login-customer",
      {
        data: loginInfo,
        withCredentials: true,
      }
    );

    // Trả về dữ liệu khi đăng nhập thành công
    return { data: response.data.result };
  } catch (error) {
    return errorMessage(error);
  }
};

export const signUpUserService = async (
  signUpInfo: RegisterSchemaType
): Promise<ApiResponse<UserRegister>> => {
  // TODO: Replace 'any' with a proper user/session type
 

  try {
    const response = await doRequest<BackendApiResponse<UserRegister>>(
      "post",
      "/api/auth/register",
      {
        data: signUpInfo,
      }
    );

    // Trả về dữ liệu khi đăng nhập thành công
    return { data: response.data.result };
  } catch (error) {
    return errorMessage(error);
  }
};

export const resendOtpService = async (
  userSignupInfo: UserRegister
): Promise<ApiResponse<string>> => {
  try {
    const response = await doRequest<BackendApiResponse<string>>(
      "post",
      "/api/auth/resend-otp-register",
      {
        data: userSignupInfo,
      }
    );
    return { data: response.data.result };
  } catch (error) {
    return errorMessage(error);
  }
};
export const verifyOtpService = async ({
  email,
  otp,
}: {
  email: string;
  otp: string;
}): Promise<ApiResponse<string>> => {
  try {
    const response = await doRequest<BackendApiResponse<string>>(
      "post",
      "/api/auth/verify-otp-register",
      {
        data: { email, otp },
      }
    );
    return { data: response.data.result };
  } catch (error) {
    return errorMessage(error);
  }
};

// ======= Logout
export const logoutUserService = async () => {
  try {
    await doRequest("post", "/api/auth/logout-customer", {
      withCredentials: true,
    });
    return;
  } catch (error) {
    return errorMessage(error);
  }
};
