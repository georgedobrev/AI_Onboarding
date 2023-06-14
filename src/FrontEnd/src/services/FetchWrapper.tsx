import axios, { AxiosResponse } from 'axios';

interface ResponseData<T> {
  data: T;
  accessToken?: string;
  refreshToken?: string;
}

function handleResponse<T>(response: AxiosResponse<T>): Promise<ResponseData<T>> {
  if (response.status >= 400) {
    throw new Error(response.statusText);
  }

  const accessToken = response.headers['access-token'];
  const refreshToken = response.headers['refresh-token'];

  const responseData: ResponseData<T> = {
    data: response.data,
    accessToken,
    refreshToken,
  };

  return Promise.resolve(responseData);
}

export const fetchWrapper = {
  get,
  post,
  put,
  delete: _delete,
};

function get<T>(url: string, headers?: Record<string, string>): Promise<ResponseData<T>> {
  const config = headers;
  return axios.get<T>(url, config).then(handleResponse);
}

function post<T, B>(
  url: string,
  body: B,
  headers?: { headers: { 'Content-Type': string } }
): Promise<ResponseData<T>> {
  const config = headers;
  return axios.post<T>(url, body, config).then(handleResponse);
}

function put<T, B>(
  url: string,
  body: B,
  headers?: Record<string, string>
): Promise<ResponseData<T>> {
  const config = headers;
  return axios.put<T>(url, body, config).then(handleResponse);
}

function _delete<T>(url: string, headers?: Record<string, string>): Promise<ResponseData<T>> {
  const config = headers;
  return axios.delete<T>(url, config).then(handleResponse);
}
