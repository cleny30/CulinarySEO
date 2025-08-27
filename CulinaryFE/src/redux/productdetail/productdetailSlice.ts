import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import type { RootState } from "../store";
import type { ProductDetail } from "@/types/productdetail";


interface ProductDetailState {
  product: ProductDetail | null;
  loading: boolean;
  error: string | null;
}

const initialState: ProductDetailState = {
  product: null,
  loading: false,
  error: null,
};

const productDetailSlice = createSlice({
  name: "productDetail",
  initialState,
  reducers: {
    setProductDetailFetching: (state, action: PayloadAction<boolean>) => {
      state.loading = action.payload;
    },
    setProductDetail: (state, action: PayloadAction<ProductDetail | null>) => {
      state.product = action.payload;
      state.loading = false;
      state.error = null;
    },
    setProductDetailError: (state, action: PayloadAction<string>) => {
      state.error = action.payload;
      state.loading = false;
    },
  },
});

export const { setProductDetailFetching, setProductDetail, setProductDetailError } =
  productDetailSlice.actions;

export default productDetailSlice.reducer;


