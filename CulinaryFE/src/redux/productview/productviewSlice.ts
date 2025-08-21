import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import type { Product } from "@/types/product"; // adjust path

// API result structure
interface ProductResult {
  items: Product[];
  totalItems: number;
  page: number;
  pageSize: number;
}

interface ProductState {
  products: ProductResult | null;
  fetching: boolean;
}

const initialState: ProductState = {
  products: null, // start with null (no data yet)
  fetching: false,
};

const productSlice = createSlice({
  name: "product",
  initialState,
  reducers: {
    setProducts: (state, action: PayloadAction<ProductResult>) => {
      state.products = action.payload;
    },
    addProduct: (state, action: PayloadAction<Product>) => {
      if (state.products) {
        state.products.items.push(action.payload);
        state.products.totalItems += 1;
      }
    },
    updateProduct: (state, action: PayloadAction<Product>) => {
      if (state.products) {
        const index = state.products.items.findIndex(
          (p) => p.productId === action.payload.productId
        );
        if (index !== -1) {
          state.products.items[index] = action.payload;
        }
      }
    },
    removeProduct: (state, action: PayloadAction<string>) => {
      if (state.products) {
        state.products.items = state.products.items.filter(
          (p) => p.productId !== action.payload
        );
        state.products.totalItems -= 1;
      }
    },
    setProductFetching: (state, action: PayloadAction<boolean>) => {
      state.fetching = action.payload;
    },
  },
});

export const {
  setProducts,
  addProduct,
  updateProduct,
  removeProduct,
  setProductFetching,

} = productSlice.actions;

export default productSlice.reducer;
