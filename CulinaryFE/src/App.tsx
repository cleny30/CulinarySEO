import "./App.css";
import AppRouter from "./routes";
import { Provider } from "react-redux";
import { persistor, store } from "./redux/store";
import { Toaster } from "./components/ui/toaster";
import { PersistGate } from "redux-persist/integration/react";

function App() {
  return (
    <Provider store={store}>
      <PersistGate loading={null} persistor={persistor}>
        <Toaster />
        <AppRouter />
      </PersistGate>
    </Provider>
  );
}

export default App;
