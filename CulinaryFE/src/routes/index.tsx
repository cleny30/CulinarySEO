import { Routes, Route } from "react-router-dom";
import publicRoutes from "./publicRoutes";
import privateRoutes from "./privateRoutes";
import ProtectedRoute from "../components/common/ProtectedRoute";
export default function AppRouter() {
  return (
    <Routes>
      {/* Public routes */}
      {publicRoutes.map(({ path, component: Component }) => (
        <Route key={path} path={path} element={<Component />} />
      ))}

      {/* Private routes */}
      {privateRoutes.map(({ path, component: Component, allowPermissions }) => (
        <Route
          key={path}
          path={path}
          element={
            <ProtectedRoute allowPermissions={allowPermissions}>
              <Component />
            </ProtectedRoute>
          }
        />
      ))}
    </Routes>
  );
}
