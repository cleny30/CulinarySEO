import axiosClient from "./axios";
import { AxiosError, type AxiosResponse, type AxiosRequestConfig } from "axios";
import { logoutSuccess } from "@/redux/auth/authSlice";
import { store } from "@/redux/store";

// Type definitions
type HttpMethod = "get" | "post" | "put" | "delete";

interface RequestOptions {
  data?: unknown;
  isUploadImg?: boolean;
  token?: string;
  withCredentials?: boolean;
}

// Main function to perform requests
const doRequest = async <T>(
  method: HttpMethod,
  url: string,
  options: RequestOptions = {}
): Promise<AxiosResponse<T>> => {
  const { data, isUploadImg = false, token, withCredentials = true } = options;

  const reqConfig: AxiosRequestConfig = {
    headers: {
      "Content-Type": isUploadImg ? "multipart/form-data" : "application/json",
      ...(token && { Authorization: `Bearer ${token}` }),
      // "ngrok-skip-browser-warning": "true",
    },
    withCredentials,
  };

  try {
    let response: AxiosResponse<T>;

    switch (method.toLowerCase() as HttpMethod) {
      case "get":
        response = await axiosClient.get<T>(url, reqConfig);
        break;
      case "post":
        response = await axiosClient.post<T>(url, data, reqConfig);
        break;
      case "put":
        response = await axiosClient.put<T>(url, data, reqConfig);
        break;
      case "delete":
        response = await axiosClient.delete<T>(url, { ...reqConfig, data });
        break;
      default:
        throw new Error(`Unsupported method: ${method}`);
    }

    return response;
  } catch (error) {
    // Handle 401/403 - Unauthorized/Forbidden errors
    if (
      error instanceof AxiosError &&
      error.response &&
      [401, 403].includes(error.response.status)
    ) {
      handleUnauthorized();
      throw error; // Still throw so caller can handle
    }

    console.error("Request error:", error);
    throw error;
  }
};

// Handle unauthorized access
const handleUnauthorized = () => {
  store.dispatch(logoutSuccess());
  window.location.href = "/";
};

// Export utility functions for each HTTP method
export const apiService = {
  get: <T>(url: string, options?: Omit<RequestOptions, "data">) =>
    doRequest<T>("get", url, options),

  post: <T>(
    url: string,
    data?: unknown,
    options?: Omit<RequestOptions, "data">
  ) => doRequest<T>("post", url, { ...options, data }),

  put: <T>(
    url: string,
    data?: unknown,
    options?: Omit<RequestOptions, "data">
  ) => doRequest<T>("put", url, { ...options, data }),

  delete: <T>(
    url: string,
    data?: unknown,
    options?: Omit<RequestOptions, "data">
  ) => doRequest<T>("delete", url, { ...options, data }),
};

export { doRequest };
export default apiService;
