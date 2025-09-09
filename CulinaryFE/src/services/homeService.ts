import type { BackendApiResponse } from "@/types/api";
import type { Category } from "@/types/category";
import { doRequest } from "@/utils/config/doRequest";
import { errorMessage } from "@/utils/constants/error/errorMessage";


export const getCateList = async () => {
  try {
    const res = await doRequest<BackendApiResponse<Category[]>>(
      "get",
      "/api/category/get-category"
    );
    return  res.data.result;
  } catch (error) {
    return errorMessage(error);
  }
};