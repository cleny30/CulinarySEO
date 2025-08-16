import { getProducts } from "@/services/productService";
import { setProductFetching, setProducts } from "./productviewSlice";
import type { ProductResult } from "@/types/product";
import type { AppDispatch } from "../store";



export const fetchProducts = async (
    dispatch: AppDispatch,

) => {
    dispatch(setProductFetching(true));

    const response = await getProducts();
    if (response.error) {
        dispatch(setProductFetching(false));
        return { error: response.error };
    }
    if (response.data) {
        const productResult: ProductResult = {
            items: response.data.items,
            totalItems: response.data.totalItems,
            page: response.data.page,
            pageSize: response.data.pageSize,
        };
        dispatch(setProducts(productResult));
    }
    dispatch(setProductFetching(false));
    return;
}