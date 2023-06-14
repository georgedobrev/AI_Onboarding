import { fetchWrapper } from './FetchWrapper.tsx';
import config from '../config.json';
import { FormValues as SignInForms } from '../components/SignIn/types.ts';
import { FormValues as RegisterForms } from '../components/Register/types.ts';

export const authService = {
  login: async (formData: SignInForms) => {
    try {
      const url = `${config.baseUrl}${config.loginEndpoint}`;
      const headers = { headers: { 'Content-Type': 'application/json' } };
      const response = await fetchWrapper.post(url, formData, headers);

      const accessToken = response.accessToken;
      const refreshToken = response.refreshToken;

      if (!accessToken || !refreshToken) {
        throw new Error('Access or refresh token not found');
      }

      const expirationDate = new Date();
      expirationDate.setUTCDate(expirationDate.getUTCDate() + 5);
      localStorage.setItem('accessToken', accessToken);
      localStorage.setItem('refreshToken', refreshToken);

      // TODO to be refactored
      const tokenParts = accessToken.split('.');
      const tokenPayload = JSON.parse(atob(tokenParts[1]));
      const expirationTime = tokenPayload.exp * 1000;
      const currentTime = new Date().getTime();
      const remainingTime = expirationTime - currentTime;

      setTimeout(() => {
        // TODO in next branch
      }, remainingTime);
    } catch (error) {
      throw new Error('Login failed');
      console.error(error);
    }
  },

  register: async (formData: RegisterForms) => {
    delete formData['confirmPassword'];
    const { confirmPassword, ...registerData } = formData;
    try {
      const url = `${config.baseUrl}${config.registerEndpoint}`;
      const response = await fetchWrapper.post(url, registerData);
      if (!response) {
        throw new Error('Request failed');
      }
    } catch (error) {
      console.error(error);
      throw new Error('Registration failed');
    }
  },
};
