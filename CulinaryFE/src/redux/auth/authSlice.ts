import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import type { UserSession } from "@/types/userSession";
import type { UserRegister } from "@/types/userRegister";

interface authState {
  login: {
    currentUser?: UserSession | null;
  };
  signup: {
    userSignupInfo?: UserRegister | null;
  };
}

const initialState = {
  login: {
    currentUser: null,
  },
  signup: {
    userSignupInfo: null,
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
    signupInProgress: (state, action: PayloadAction<UserRegister>) => {
      state.signup.userSignupInfo = action.payload;
    },
    signupSuccess: (state) => {
      state.signup.userSignupInfo = null;
    },
  },
});

export const {
  loginSuccess,
  logoutSuccess,
  signupInProgress,
  signupSuccess,
} = authSlice.actions;

export default authSlice.reducer;
