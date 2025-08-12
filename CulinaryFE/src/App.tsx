import "./App.css";
import AppRouter from "./routes";
import { Provider } from "react-redux";
import { store } from "./redux/store";
import { Toaster } from "./components/ui/toaster";

function App() {
  return (
    <Provider store={store}>
      <Toaster />
      <AppRouter />
    </Provider>
  );
}

export default App;
