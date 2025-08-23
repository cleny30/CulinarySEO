import { getProductById } from "@/services/productService"
import type { AppDispatch } from "../store"
import { setProductDetail, setProductDetailFetching } from "./productdetailSlice"
import type { ProductDetail } from "@/types/productdetail"


export const selectProductDetail = async (
    dispatch: AppDispatch,
    productId: string
) => {
    dispatch(setProductDetailFetching(true))
    const response = await getProductById(productId)
    if (response.error) {
        dispatch(setProductDetailFetching(false));
        return { error: response.error };
    }
    if (response.data) {
        const productDetail: ProductDetail = {
            productId: response.data.productId,
            productName: response.data.productName,
            shortDescription: response.data.shortDescription,
            longDescription: response.data.longDescription,
            price: response.data.price,
            discount: response.data.discount,
            finalPrice: response.data.finalPrice,
            categoryName: response.data.categoryName,
            productImages: response.data.productImages,
            reviews: response.data.reviews,
        }
        dispatch(setProductDetail(productDetail));
    }
    dispatch(setProductDetailFetching(false));
    return;
}