import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import { type Category } from "@/types/filter";

type SortBy =
  | "Featured"
  | "Price: Low to High"
  | "Price: High to Low"
  | "Newest"
  | "Oldest";

interface ProductFilterState {
  productfilter: {
    categories: Array<{
      categoryId: number;
      categoryName: string;
      productCount: number;
    }>;
    price: { from: number; to: number };
    availability: boolean;
    sortBy?: SortBy | null;
  };
  fetching: boolean;
}

const initialState: ProductFilterState = {
  productfilter: {
    categories: [],
    price: { from: 0, to: 1000 },
    availability: false,
    sortBy: null,
  },
  fetching: false,
};

const productSlice = createSlice({
  name: "productfilter",
  initialState,
  reducers: {
    setCategories: (state, action: PayloadAction<Category[]>) => {
      state.productfilter.categories = action.payload;
    },
    setPrice: (state, action: PayloadAction<{ from: number; to: number }>) => {
      state.productfilter.price = action.payload;
    },
    setAvailability: (state, action: PayloadAction<boolean>) => {
      state.productfilter.availability = action.payload;
    },
    setSortBy: (state, action: PayloadAction<SortBy | null>) => {
      state.productfilter.sortBy = action.payload;
    },
    setFetching: (state, action: PayloadAction<boolean>) => {
      state.fetching = action.payload;
    },
  },
});

export const {
  setCategories,
  setPrice,
  setAvailability,
  setSortBy,
  setFetching,
} = productSlice.actions;
export default productSlice.reducer;
