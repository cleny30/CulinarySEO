import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import { type Category } from "@/types/filter";


// match <SelectItem value="x">

interface ProductFilterState {
  productfilter: {
    selectedCategories?: number[] | null;
    categories: Category[];
    price?: { from: number; to: number } | null;
    availability?: boolean | null;
    sortBy?: string | null;
    WarehouseId? :string | null;
  };
  fetching: boolean;
}

const initialState: ProductFilterState = {
  productfilter: {
    selectedCategories: null,
    categories: [],
    price: null,
    availability: null,
    sortBy: null,
    WarehouseId: null,
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
    setAvailability: (state, action: PayloadAction<boolean | null>) => {
      state.productfilter.availability = action.payload;
    },
    setSortBy: (state, action: PayloadAction<string | null>) => {
      state.productfilter.sortBy = action.payload;
    },
    setFetching: (state, action: PayloadAction<boolean>) => {
      state.fetching = action.payload;
    },
    setSelectedCategories: (state, action: PayloadAction<number[] | null>) => {
      state.productfilter.selectedCategories = action.payload;
    },
    setWarehouseId: (state, action: PayloadAction<string | null>) => {
      state.productfilter.WarehouseId = action.payload;
    }
  },
});

export const {
  setCategories,
  setPrice,
  setAvailability,
  setSortBy,
  setFetching,
  setSelectedCategories,
  setWarehouseId,
} = productSlice.actions;
export default productSlice.reducer;
