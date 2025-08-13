import { createSlice, type PayloadAction } from "@reduxjs/toolkit";

// Define a type for the product data if you have one, otherwise use 'any'
// For example:
// interface Product {
//   id: string;
//   name: string;
//   price: number;
// }

interface ProductState {
  products: unknown; // Replace 'any' with your Product type
  loading: boolean;
  error: string | null;
}

const initialState: ProductState = {
  products: [],
  loading: false,
  error: null,
};

const productSlice = createSlice({
  name: "product",
  initialState,
  reducers: {
    setProducts: (state, action: PayloadAction<unknown>) => {
      state.products = action.payload;
    },
    setLoading: (state, action: PayloadAction<boolean>) => {
      state.loading = action.payload;
    },
    setError: (state, action: PayloadAction<string | null>) => {
      state.error = action.payload;
    },
  },
});

export const { setProducts, setLoading, setError } = productSlice.actions;
export default productSlice.reducer;
