import type { CategoryCount } from "@/types/category";
import type { NavItem } from "@/types/home";
import { menuNav } from "@/utils/config/navMenu";
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";

interface HomeState {
  header: {
    categoryItem: Array<NavItem>;
    loading: boolean;
  };
  home: {
    categoryList?: Array<CategoryCount> | null;
    loading: boolean;
  };
}

const initialState: HomeState = {
  header: {
    categoryItem: menuNav,
    loading: false,
  },
  home: {
    categoryList: null,
    loading: false,
  },
};

const homeSlice = createSlice({
  name: "home",
  initialState,
  reducers: {
    loadingCate: (state) => {
      state.header.loading = true;
    },
    loadedCate: (
      state,
      action: PayloadAction<HomeState["header"]["categoryItem"]>
    ) => {
      state.header.loading = false;
      state.header.categoryItem = action.payload;
    },
    loadingHomeCate: (state) => {
      state.home.loading = true;
    },
    loadedHomeCate: (state, action: PayloadAction<CategoryCount[]>) => {
      state.home.loading = false;
      state.home.categoryList = action.payload;
    },
  },
});

export const { loadingCate, loadedCate, loadingHomeCate, loadedHomeCate } =
  homeSlice.actions;

export default homeSlice.reducer;
