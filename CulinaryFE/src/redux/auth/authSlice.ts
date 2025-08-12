import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import { type User } from "@/types/user";

interface authState {
  login: {
    // TODO: định nghĩa lại type cho User ở @/types/user
    currentUser?: User | null;
    isFetching: boolean;
    error: boolean;
  };
}

const initialState = {
  login: {
    currentUser: null,
    isFetching: false,
    error: false,
  },
} satisfies authState as authState;

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    loginStart: (state) => {
      state.login.isFetching = true;
    },
    loginSuccess: (state, action: PayloadAction<User>) => {
      state.login.isFetching = false;
      state.login.currentUser = action.payload;
      state.login.error = false;
    },
    loginFailure: (state) => {
      state.login.isFetching = false;
      state.login.error = true;
    },
  },
});

export const { loginStart, loginSuccess, loginFailure } = authSlice.actions;

export default authSlice.reducer;
