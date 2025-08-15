import "./App.css";
import AppRouter from "./routes";
import { Provider } from "react-redux";
import { persistor, store } from "./redux/store";
import { Toaster } from "./components/ui/toaster";
import { PersistGate } from "redux-persist/integration/react";
import { GoogleOAuthProvider } from "@react-oauth/google";

function App() {
  return (
    <GoogleOAuthProvider clientId={import.meta.env.VITE_GOOGLE_CLIENT_ID as string}>
      <Provider store={store}>
        <PersistGate loading={null} persistor={persistor}>
          <Toaster />
          <AppRouter />
        </PersistGate>
      </Provider>
    </GoogleOAuthProvider>
  );
}

export default App;
