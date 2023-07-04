import axios, { AxiosResponse } from 'axios';

function handleResponse<T>(response: AxiosResponse<T>): Promise<AxiosResponse<T>> {
  if (response.status >= 400) {
    throw new Error(response.statusText);
  }

  if (!response.headers) {
    throw new Error('Headers not found in the response');
  }

  return Promise.resolve(response);
}

export const fetchWrapper = {
  get,
  post,
  put,
  delete: _delete,
};

function get<T>(
  url: string,
  headers?: { headers: { Authorization: string } }
): Promise<AxiosResponse<T>> {
  const config = headers;
  return axios.get<T>(url, config).then(handleResponse);
}

function post<T, B>(
  url: string,
  body: B,
  headers?: { headers: { 'Content-Type': string } }
): Promise<AxiosResponse<T>> {
  const config = headers ? { headers: headers.headers } : undefined;
  return axios.post<T>(url, body, config).then(handleResponse);
}

function put<T, B>(
  url: string,
  body: B,
  headers?: Record<string, string>
): Promise<AxiosResponse<T>> {
  const config = headers;
  return axios.put<T>(url, body, config).then(handleResponse);
}

function _delete<T>(
  url: string,
  headers?: { headers: { Authorization: string } }
): Promise<AxiosResponse<T>> {
  const config = headers;
  return axios.delete<T>(url, config).then(handleResponse);
}
