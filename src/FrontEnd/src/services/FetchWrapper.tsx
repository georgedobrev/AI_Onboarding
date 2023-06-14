import axios, { AxiosResponse } from 'axios';
import { extendSessionFormValues, FormValues } from '../components/SignIn/types.ts';

function handleResponse<T>(
  response: AxiosResponse<T>
): Promise<{ data: T; accessToken?: string; refreshToken?: string }> {
  if (response.status >= 400) {
    throw new Error(response.statusText);
  }

  const accessToken = response.headers['access-token'];
  const refreshToken = response.headers['refresh-token'];

  return Promise.resolve({
    data: response.data,
    error: response.status,
    accessToken,
    refreshToken,
  });
}

export const fetchWrapper = {
  get,
  post,
  put,
  delete: _delete,
};

function get<T>(
  url: string,
  headers?: Record<string, string>
): Promise<{ data: T; accessToken?: string; refreshToken?: string }> {
  const config = headers;
  return axios.get<T>(url, config).then(handleResponse);
}

function post<T>(
  url: string,
  body: FormValues | extendSessionFormValues | FormData,
  headers?: { headers: { 'Content-Type': string } }
): Promise<{ data: T; accessToken?: string; refreshToken?: string }> {
  const config = headers;
  return axios.post<T>(url, body, config).then(handleResponse);
}

function put<T, B>(
  url: string,
  body: B,
  headers?: Record<string, string>
): Promise<{ data: T; accessToken?: string; refreshToken?: string }> {
  const config = headers;
  return axios.put<T>(url, body, config).then(handleResponse);
}

function _delete<T>(
  url: string,
  headers?: Record<string, string>
): Promise<{ data: T; accessToken?: string; refreshToken?: string }> {
  const config = headers;
  return axios.delete<T>(url, config).then(handleResponse);
}
