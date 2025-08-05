import axiosClient from "./axios";
import type { AxiosResponse } from 'axios';

// Type definitions
type HttpMethod = 'get' | 'post' | 'put' | 'delete';

interface RequestOptions {
  data?: any;
  isUploadImg?: boolean;
  token?: string;
}

interface RequestHeaders {
  headers: {
    'Content-Type': string;
    Authorization?: string;
  };
}

// Main function to perform requests
const doRequest = async <T = any>(
  method: HttpMethod,
  url: string,
  options: RequestOptions = {}
): Promise<AxiosResponse<T>> => {
  const { data, isUploadImg = false, token } = options;

  const reqHeader: RequestHeaders = {
    headers: {
      'Content-Type': isUploadImg ? 'multipart/form-data' : 'application/json',
      ...(token && { Authorization: `Bearer ${token}` })
    }
  };

  try {
    let response: AxiosResponse<T>;

    switch (method.toLowerCase() as HttpMethod) {
      case 'get':
        response = await axiosClient.get<T>(url, reqHeader);
        break;
      case 'post':
        response = await axiosClient.post<T>(url, data, reqHeader);
        break;
      case 'put':
        response = await axiosClient.put<T>(url, data, reqHeader);
        break;
      case 'delete':
        response = await axiosClient.delete<T>(url, { ...reqHeader, data });
        break;
      default:
        throw new Error(`Unsupported method: ${method}`);
    }

    return response;
  } catch (error: any) {
    // Handle 401/403 - Unauthorized/Forbidden errors
    if (error.response && [401, 403].includes(error.response.status)) {
      handleUnauthorized();
      throw error; // Still throw so caller can handle
    }

    console.error('Request error:', error);
    throw error;
  }
};

// Handle unauthorized access
const handleUnauthorized = (): void => {
  // Clear cookies and session

  // Redirect to login page
  window.location.href = '/Login';
};

// Export utility functions for each HTTP method
export const apiService = {
  get: <T = any>(url: string, options?: Omit<RequestOptions, 'data'>) =>
    doRequest<T>('get', url, options),

  post: <T = any>(url: string, data?: any, options?: Omit<RequestOptions, 'data'>) =>
    doRequest<T>('post', url, { ...options, data }),

  put: <T = any>(url: string, data?: any, options?: Omit<RequestOptions, 'data'>) =>
    doRequest<T>('put', url, { ...options, data }),

  delete: <T = any>(url: string, data?: any, options?: Omit<RequestOptions, 'data'>) =>
    doRequest<T>('delete', url, { ...options, data }),
};

export { doRequest };
export default apiService;