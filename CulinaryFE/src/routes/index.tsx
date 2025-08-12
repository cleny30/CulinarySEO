import { Routes, Route } from "react-router-dom";
import publicRoutes from "./publicRoutes";
import privateRoutes from "./privateRoutes";
import ProtectedRoute from "../common/PrivateRoute";
import authRoutes from "./authRoutes";
import AuthRoute from "../common/AuthRoute";
export default function AppRouter() {
  return (
    <Routes>
      {/* Public routes */}
      {publicRoutes.map(({ path, component: Component }) => (
        <Route key={path} path={path} element={<Component />} />
      ))}

      {/* Auth routes */}
      <Route element={<AuthRoute />}>
        {authRoutes.map(({ path, component: Component }) => (
          <Route key={path} path={path} element={<Component />} />
        ))}
      </Route>

      {/* Private routes */}
      {privateRoutes.map(({ path, component: Component, allowPermissions }) => (
        <Route element={<ProtectedRoute allowPermissions={allowPermissions} />}>
          <Route key={path} path={path} element={<Component />} />
        </Route>
      ))}

      {/* Private routes */}
      {/* {privateRoutes.map(({ path, component: Component, allowPermissions }) => (
        <Route
          key={path}
          path={path}
          element={
            <ProtectedRoute allowPermissions={allowPermissions}>
              <Component />
            </ProtectedRoute>
          }
        />
      ))} */}
    </Routes>
  );
}
