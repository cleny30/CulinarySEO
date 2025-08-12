import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import { type UserSession } from "@/types/userSession";

interface authState {
  login: {
    // TODO: định nghĩa lại type cho User ở @/types/user
    currentUser?: UserSession | null;
    isFetching: boolean;
  };
  logout: {
    isFetching: boolean;
  };
}

const initialState = {
  login: {
    currentUser: null,
    isFetching: false,
  },
  logout: {
    isFetching: false,
  },
} satisfies authState as authState;

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    loginStart: (state) => {
      state.login.isFetching = true;
    },
    loginSuccess: (state, action: PayloadAction<UserSession>) => {
      state.login.isFetching = false;
      state.login.currentUser = action.payload;
    },

    logoutStart: (state) => {
      state.logout.isFetching = true;
    },
    logoutSuccess: (state) => {
      state.logout.isFetching = false;
      state.login.currentUser = null;
    },
  },
});

export const {
  loginStart,
  loginSuccess,
  logoutStart,
  logoutSuccess,
} = authSlice.actions;

export default authSlice.reducer;
