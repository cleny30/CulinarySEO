import type { ApiResponse, BackendApiResponse } from "@/types/api";
import type { Product, ProductResult } from "@/types/product";
import { doRequest } from "@/utils/config/doRequest";
import { errorMessage } from "@/utils/constants/error/errorMessage";

export const getProducts = async (): Promise<ApiResponse<ProductResult>> => {
    try {
        const response = await doRequest<BackendApiResponse<ProductResult>>(
            "get",
            "/api/products/filter-product",
        );
        return { data: response.data.result };
    } catch (error) {
        return errorMessage(error);
    }
}
