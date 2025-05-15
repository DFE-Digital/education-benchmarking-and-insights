interface ApiError {
  error?: Error;
}

type ApiResponse<T> = T[] | ApiError;
