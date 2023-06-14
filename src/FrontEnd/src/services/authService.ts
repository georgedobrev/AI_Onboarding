import { fetchWrapper } from './FetchWrapper.tsx';
import config from '../config.json';
import { FormValues as SignInForms } from '../components/SignIn/types.ts';
import { FormValues as RegisterForms } from '../components/Register/types.ts';

export const authService = {
  login: async (formData: SignInForms) => {
    try {
      const url = `${config.baseUrl}${config.loginEndpoint}`;
      const response = await fetchWrapper.post(url, formData);

      if (!response) {
        throw new Error('Request failed');
      }
    } catch (error) {
      throw new Error('Login failed');
    }
  },

  register: async (formData: RegisterForms) => {
    const { confirmPassword, ...registerData } = formData;
    try {
      const url = `${config.baseUrl}${config.registerEndpoint}`;
      const response = await fetchWrapper.post(url, registerData);

      if (!response) {
        throw new Error('Request failed');
      }

      return response;
    } catch (error) {
      console.error(error);
      throw new Error('Registration failed');
    }
  },
};
