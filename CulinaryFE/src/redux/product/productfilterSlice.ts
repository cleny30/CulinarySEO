import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import { type Category } from "@/types/filter";
//Đại đại
type SortBy =
  | "Featured"
  | "Price: Low to High"
  | "Price: High to Low"
  | "Newest"
  | "Oldest";

interface ProductFilterState {
  productfilter: {
    categories: Array<{
      categoryID: string;
      name: string;
      count: number;
      checked: boolean;
    }>;
    price: { from: number; to: number };
    availability: boolean;
    sortBy?: SortBy | null; // Optional sorting option
  };
}

const initialState: ProductFilterState = {
  productfilter: {
    categories: [],
    price: { from: 0, to: 1000 }, // Default price range
    availability: false, // Default availability filter
    sortBy: null, // Default sorting option
  }
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
    setChangeCategoryCheck: (state, action: PayloadAction<{ categoryID: string; checked: boolean }>) => {
      const { categoryID, checked } = action.payload;
      const category = state.productfilter.categories.find(cat => cat.categoryID === categoryID);
      if (category) {
        category.checked = checked;
      }
    },
  }
});

export const { setCategories,setPrice,setAvailability,setSortBy,setChangeCategoryCheck } = productSlice.actions;
export default productSlice.reducer;
