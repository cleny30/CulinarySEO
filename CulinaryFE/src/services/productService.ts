import type { ApiResponse, BackendApiResponse } from "@/types/api";
import type { Product, ProductResult } from "@/types/product";
import type { ProductDetail } from "@/types/productdetail";
import { doRequest } from "@/utils/config/doRequest";
import { errorMessage } from "@/utils/constants/error/errorMessage";

export const getProducts = async (
    pageNumber: number,
    CategoryIds?: number[] | null,
    MinPrice?: number | null,
    MaxPrice?: number | null,
    IsAvailable?: boolean | null,
    SortBy?: string | null,
    WarehouseId?: string | null
): Promise<ApiResponse<ProductResult>> => {
    try {
        const params = new URLSearchParams();

        params.append("Page", String(pageNumber));
        params.append("PageSize", "20");

        if (CategoryIds && CategoryIds.length > 0) {
            // If backend expects CSV
            params.append("CategoryIds", CategoryIds.join(","));
        }
        if (MinPrice !== null && MinPrice !== undefined) {
            params.append("MinPrice", String(MinPrice));
        }
        if (MaxPrice !== null && MaxPrice !== undefined) {
            params.append("MaxPrice", String(MaxPrice));
        }
        if (IsAvailable !== null && IsAvailable !== undefined) {
            params.append("IsAvailable", String(IsAvailable));
        }
        if (SortBy) {
            params.append("SortBy", SortBy);
        }
        if(WarehouseId){
            params.append("WarehouseId",WarehouseId)
        }

        const response = await doRequest<BackendApiResponse<ProductResult>>(
            "get",
            `/api/products/filter-product?${params.toString()}`
        );

        return { data: response.data.result };
    } catch (error) {
        return errorMessage(error);
    }
};

export const getProductById = async (
    productId: string
): Promise<ApiResponse<ProductDetail>> => {
    try {
        const response = await doRequest<BackendApiResponse<ProductDetail>>(
            "get",
            `/api/products/${productId}`
        );

        return { data: response.data.result };
    } catch (error) {
        return errorMessage(error);
    }
};

