import { type LoginSchemaType } from "@/schemas/auth";
import type {
  ApiResponse,
  BackendApiResponse,
  BackendErrorResponse,
} from "@/types/api";
import { doRequest } from "@/utils/config/doRequest";
import { AxiosError } from "axios";

export const loginUser = async (
  loginInfo: LoginSchemaType
): Promise<ApiResponse<unknown>> => {
  // TODO: Replace 'any' with a proper user/session type
  try {
    const response = await doRequest<BackendApiResponse<unknown>>(
      "post",
      "Auth",
      {
        data: loginInfo,
      }
    );

    // Trả về dữ liệu khi đăng nhập thành công
    return { data: response.data.result };
  } catch (error) {
    if (error instanceof AxiosError) {
      const errorData = error.response?.data as BackendErrorResponse;
      const errorMessage =
        errorData?.message || error.message || "Đã có lỗi xảy ra!";
      return { error: errorMessage };
    }
    return { error: "Đã có lỗi xảy ra!" };
  }
};
