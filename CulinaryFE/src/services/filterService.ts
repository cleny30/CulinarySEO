import type { ApiResponse, BackendApiResponse } from "@/types/api";
import type { Category } from "@/types/filter";
import { doRequest } from "@/utils/config/doRequest";
import { errorMessage } from "@/utils/constants/error/errorMessage";

export const getCategories = async (): Promise<ApiResponse<Category[]>> => {
  // TODO: Replace 'any' with a proper user/session type
  try {
    const response = await doRequest<BackendApiResponse<Category[]>>(
      "get",
      "/api/auth/login-customer",
    );

    return { data: response.data.result };
  } catch (error) {
    return errorMessage(error);
  }
};