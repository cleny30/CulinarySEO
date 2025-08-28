import type { BackendApiResponse } from "@/types/api";
import { doRequest } from "@/utils/config/doRequest";
import { errorMessage } from "@/utils/constants/error/errorMessage";

interface Category {
  categoryId: number;
  categoryName: string;
  categoryImage: string;
  description: string;
}
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