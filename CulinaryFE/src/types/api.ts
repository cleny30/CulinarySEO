// TODO: Consider moving these interfaces to a shared types file
export interface BackendApiResponse<T> {
    result: T;
    isSuccess: boolean;
    message: string;
}

export interface BackendErrorResponse {
    message: string;
}

export interface ApiResponse<T> {
    data?: T;
    error?: string;
}