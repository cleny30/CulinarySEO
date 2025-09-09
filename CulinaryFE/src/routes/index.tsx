import { Routes, Route } from "react-router-dom";
import publicRoutes from "./publicRoutes";
import privateRoutes from "./privateRoutes";
import ProtectedRoute from "../common/PrivateRoute";
import authRoutes from "./authRoutes";
import AuthRoute from "../common/AuthRoute";
import MainLayout from "@/components/layout/mainLayout";

export default function AppRouter() {
  return (
    <Routes>
      {/* Public routes */}
      {publicRoutes.map(({ path, component: Component, layout: Layout }) => {
        const PageLayout = Layout || MainLayout;
        return (
          <Route
            key={path}
            path={path}
            element={
              <PageLayout>
                <Component />
              </PageLayout>
            }
          />
        );
      })}

      {/* Auth routes */}
      <Route element={<AuthRoute />}>
        {authRoutes.map(({ path, component: Component, layout: Layout }) => {
          const PageLayout = Layout || MainLayout;
          return (
            <Route
              key={path}
              path={path}
              element={
                <PageLayout>
                  <Component />
                </PageLayout>
              }
            />
          );
        })}
      </Route>

      {/* Private routes */}
      {privateRoutes.map(
        ({ path, component: Component, allowPermissions, layout: Layout }) => {
          const PageLayout = Layout || MainLayout;
          return (
            <Route
              element={<ProtectedRoute allowPermissions={allowPermissions} />}
            >
              <Route
                key={path}
                path={path}
                element={
                  <PageLayout>
                    <Component />
                  </PageLayout>
                }
              />
            </Route>
          );
        }
      )}
    </Routes>
  );
}
