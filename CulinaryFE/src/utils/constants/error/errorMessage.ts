import type { BackendErrorResponse } from "@/types/api";
import { AxiosError } from "axios";

export const errorMessage = (error: unknown) => {
  if (error instanceof AxiosError) {
    const errorData = error.response?.data as BackendErrorResponse;
    const errorMessage =
      errorData?.message || error.message || "Đã có lỗi xảy ra!";
    return { error: errorMessage };
  }
  return { error: "Đã có lỗi xảy ra!" };
};
