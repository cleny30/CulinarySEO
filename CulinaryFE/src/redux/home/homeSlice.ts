import type { Category } from "@/types/category";
import type { NavItem } from "@/types/home";
import type { Product } from "@/types/product";
import { menuNav } from "@/utils/config/navMenu";
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";

interface HomeState {
  header: {
    categoryItem: Array<NavItem>;
  };
  home: {
    featuredProduct?: Array<Product> | null;
    categoryList?: Array<Category> | null;
    loading: boolean;
  };
}

const initialState: HomeState = {
  header: {
    categoryItem: menuNav,
  },
  home: {
    featuredProduct: null,
    categoryList: null,
    loading: true,
  },
};

const homeSlice = createSlice({
  name: "home",
  initialState,
  reducers: {
    loadedCate: (
      state,
      action: PayloadAction<HomeState["header"]["categoryItem"]>
    ) => {
      state.header.categoryItem = action.payload;
    },
    loadedHomeCate: (state, action: PayloadAction<Category[]>) => {
      state.home.loading = false;
      state.home.categoryList = action.payload;
    },
    loadedProduct: (state, action: PayloadAction<Product[]>) => {
      state.home.featuredProduct = action.payload;
    },
  },
});

export const { loadedCate, loadedHomeCate, loadedProduct } = homeSlice.actions;

export default homeSlice.reducer;
