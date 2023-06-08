import axios, { AxiosResponse } from 'axios';

export const fetchWrapper = {
  get,
  post,
  put,
  delete: _delete,
};

function get<T>(url: string): Promise<T> {
  return axios.get<T>(url).then(handleResponse);
}

function post<T>(url: string, body: any): Promise<T> {
  return axios.post<T>(url, body).then(handleResponse);
}

function put<T>(url: string, body: any): Promise<T> {
  return axios.put<T>(url, body).then(handleResponse);
}

function _delete<T>(url: string): Promise<T> {
  return axios.delete<T>(url).then(handleResponse);
}

function handleResponse<T>(response: AxiosResponse<T>): Promise<T> {
  const data = response.config.data;

  if (response.status >= 400) {
    const error = data && data.message ? data.message : response.statusText;
    return Promise.reject(error);
  }

  return Promise.resolve(data);
}
