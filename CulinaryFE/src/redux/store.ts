import { combineReducers, configureStore } from "@reduxjs/toolkit";
import authReducer from "./auth/authSlice";
import productfilterReducer from "./product/productfilterSlice";
import productView from "./productview/productviewSlice";
import homeReducer from "./home/homeSlice";
import productDetail from "./productdetail/productdetailSlice"
import {
  persistStore,
  persistReducer,
  FLUSH,
  REHYDRATE,
  PAUSE,
  PERSIST,
  PURGE,
  REGISTER,
} from "redux-persist";
import storage from "redux-persist/lib/storage";

const authPersistConfig = {
  key: "auth",
  storage,
  blacklist: ["signup"], // item will not be persisted
};

const rootReducer = combineReducers({
  auth: persistReducer(authPersistConfig, authReducer),
  productfilter: productfilterReducer,
  productview: productView,
  home: homeReducer,
  productdetail: productDetail
});

export const store = configureStore({
  reducer: rootReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      },
    }),
});

export const persistor = persistStore(store);

export type RootState = ReturnType<typeof store.getState>;

export type AppDispatch = typeof store.dispatch;
