import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import { type UserSession } from "@/types/userSession";

interface authState {
  login: {
    // TODO: định nghĩa lại type cho User ở @/types/user
    currentUser?: UserSession | null;
  };
}

const initialState = {
  login: {
    currentUser: null,
  },
} satisfies authState as authState;

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    loginSuccess: (state, action: PayloadAction<UserSession>) => {
      state.login.currentUser = action.payload;
    },
    logoutSuccess: (state) => {
      state.login.currentUser = null;
    },
  },
});

export const {
  loginSuccess,
  logoutSuccess,
} = authSlice.actions;

export default authSlice.reducer;
