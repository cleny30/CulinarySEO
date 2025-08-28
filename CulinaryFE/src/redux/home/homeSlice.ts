import type { NavItem } from "@/types/home";
import { menuNav } from "@/utils/config/navMenu";
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";

interface HomeState {
  header: {
    categoryItem: Array<NavItem>;
    loading: boolean;
  };
}

const initialState: HomeState = {
  header: {
    categoryItem: menuNav,
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
  },
});

export const { loadingCate, loadedCate } = homeSlice.actions;

export default homeSlice.reducer;
