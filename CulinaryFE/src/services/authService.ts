import { type LoginSchemaType } from "@/schemas/auth";
import type { ApiResponse, BackendApiResponse } from "@/types/api";
import type { UserSession } from "@/types/userSession";
import { doRequest } from "@/utils/config/doRequest";
import { errorMessage } from "@/utils/constants/error/errorMessage";

export const loginUser = async (
  loginInfo: LoginSchemaType
): Promise<ApiResponse<UserSession>> => {
  // TODO: Replace 'any' with a proper user/session type
  try {
    const response = await doRequest<BackendApiResponse<UserSession>>(
      "post",
      "/api/auth/login-manager",
      {
        data: loginInfo,
      }
    );

    // Trả về dữ liệu khi đăng nhập thành công
    return { data: response.data.result };
  } catch (error) {
    return errorMessage(error);
  }
};

// ======= Logout
export const logoutUser = async () => {
  try {
    await doRequest("post", "/api/auth/logout-manager", {
      withCredentials: true,
    });
    return;
  } catch (error) {
    return errorMessage(error);
  }
};
