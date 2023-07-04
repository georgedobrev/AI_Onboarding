import jwt_decode from 'jwt-decode';
import { errorNotifications } from '../components/Notifications/Notifications.tsx';
import config from '../config.json';
import { authHeaderAI, authHeaderAIGetConversations } from './commonConfig.ts';
import { fetchWrapper } from './FetchWrapper.tsx';
import { FormValues as SignInForms } from '../components/SignIn/types.ts';
import { FormValues as RegisterForms } from '../components/Register/types.ts';
import { ExtendSessionFormValues } from '../components/SignIn/types.ts';

interface LoginResponse {
  accessToken?: string;
  refreshToken?: string;
}

interface RequestLoginBody {
  email: string;
  password: string;
}

interface ExtendSessionRequestBody {
  token: string;
  refreshToken: string;
}

interface GoogleLoginRequestBody {
  token: string;
}

interface AISearchRequestBody {
  id: number;
  question: string;
}

interface AISearchResponse {
  id: string;
  answer: string;
}

interface AIGetConversationsResponse {
  conversations: AIGetConversationsResponse[];
}

interface AIGetConversationByIdResponse {
    id: string;
    questionAnswers: string[];
}

interface forgotPasswordRequestBody {
  email: string;
}

interface passwordResetResponse {
  message: string;
}

interface resetPasswordRequestBody {
  email: string;
  token: string;
  newPassword: string;
  confirmPassword: string;
}

interface validateResetTokenRequestBody {
  token: string;
  email: string;
}

interface AISearch {
  question: string;
  id?: string;
}

interface AIDeleteRequestBody {
  id: number;
}

export const authService = {
  login: async (formData: SignInForms) => {
    try {
      const url = `${config.baseUrl}${config.loginEndpoint}`;
      const headers = { headers: { 'Content-Type': 'application/json' } };
      const body: RequestLoginBody = formData;
      const response = await fetchWrapper.post<LoginResponse, RequestLoginBody>(url, body, headers);
      const accessToken = response.headers.get('access-token');
      const refreshToken = response.headers.get('refresh-token');

      if (!accessToken || !refreshToken) {
        throw new Error('Access or refresh token not found');
      }
      const expirationDate = new Date();
      expirationDate.setUTCDate(expirationDate.getUTCDate() + 5);
      localStorage.setItem('accessToken', accessToken);
      localStorage.setItem('refreshToken', refreshToken);

      // TODO to be refactored
      const tokenPayload = jwt_decode(accessToken);
      const userRole = tokenPayload[config.JWTUserRole];
      localStorage.setItem('userRole', userRole);

      const expirationTime = tokenPayload.exp * 1000;
      const currentTime = new Date().getTime();
      const remainingTime = expirationTime - currentTime;

      const role = setTimeout(() => {
        // TODO in next branch
      }, remainingTime);

      return response;
    } catch (error) {
      errorNotifications('Wrong email or password');
      throw new Error('Login failed');
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

  extendSession: async (formData: ExtendSessionFormValues) => {
    try {
      const url = `${config.baseUrl}${config.refreshTokenEndpoint}`;
      const headers = { headers: { 'Content-Type': 'application/json' } };
      const response = await fetchWrapper.post<LoginResponse, ExtendSessionRequestBody>(
        url,
        formData,
        headers
      );
      if (!response) {
        throw new Error('Request failed');
      }
      return response;
    } catch (error) {
      console.error(error);
      throw new Error('Session extension failed');
    }
  },

  googleLogin: async (formData: string | undefined) => {
    try {
      const headers = { headers: { 'Content-Type': 'application/json' } };
      const url = `${config.baseUrl}${config.googleLoginEndpoint}`;
      const response = await fetchWrapper.post<LoginResponse, GoogleLoginRequestBody>(
        url,
        formData,
        headers
      );
      if (!response) {
        throw new Error('Request failed');
      }
      localStorage.setItem('accessToken', response.headers.get('access-token'));
      localStorage.setItem('refreshToken', response.headers.get('refresh-token'));
      return response;
    } catch (error) {
      console.error(error);
      throw new Error('Google login failed');
    }
  },

  forgotPassword: async (formData: object | undefined) => {
    try {
      const headers = { headers: { 'Content-Type': 'application/json' } };
      const url = `${config.baseUrl}${config.forgotPasswordEndpoint}`;
      const response = await fetchWrapper.post<passwordResetResponse, forgotPasswordRequestBody>(
        url,
        formData,
        headers
      );
      if (!response) {
        throw new Error('Request failed');
      }
      return response;
    } catch (error) {
      errorNotifications('Wrong email or non-existing email');
      console.error(error);
      throw new Error('Forget password failed');
    }
  },

  resetPassword: async (formData: object | undefined) => {
    try {
      const headers = { headers: { 'Content-Type': 'application/json' } };
      const url = `${config.baseUrl}${config.resetPasswordEndpoint}`;
      const response = await fetchWrapper.post<passwordResetResponse, resetPasswordRequestBody>(
        url,
        formData,
        headers
      );
      if (!response) {
        throw new Error('Request failed');
      }
      return response;
    } catch (error) {
      console.error(error);
      throw new Error('Reset password failed');
    }
  },

  validateResetToken: async (formData: object | undefined) => {
    try {
      const headers = { headers: { 'Content-Type': 'application/json' } };
      const url = `${config.baseUrl}${config.validateResetTokenEndpoint}`;
      const response = await fetchWrapper.post<
        passwordResetResponse,
        validateResetTokenRequestBody
      >(url, formData, headers);
      if (!response) {
        throw new Error('Request failed');
      }
      return response;
    } catch (error) {
      console.error(error);
      throw new Error('Validate reset token failed');
    }
  },

  AISearchResponse: async (formData: AISearch | undefined) => {
    try {
      const headers = authHeaderAI();
      const url = `${config.baseUrl}${config.AISearchEndpoint}`;
      const response = await fetchWrapper.post<AISearchResponse, AISearchRequestBody>(
        url,
        formData,
        headers
      );
      if (!response) {
        throw new Error('Request failed');
      }
      return response;
    } catch (error) {
      console.error(error);
      throw new Error('AI Search failed');
    }
  },

  AIGetAllConversations: async () => {
    try {
      const headers = authHeaderAIGetConversations();
      const url = `${config.baseUrl}${config.AIConversations}`;
      const response = await fetchWrapper.get<AIGetConversationsResponse>(url, headers);
      if (!response) {
        throw new Error('Request failed');
      }
      return response;
    } catch (error) {
      console.error(error);
      throw new Error('AI Get All Conversations failed');
    }
  },

  AIGetConversationById: async (id: number) => {
    try {
      const headers = authHeaderAIGetConversations();
      const url = `${config.baseUrl}${config.AIConversation}${id}`;
      const response = await fetchWrapper.get<AIGetConversationByIdResponse>(url, headers);
      if (!response) {
        throw new Error('Request failed');
      }
      return response;
    } catch (error) {
      console.error(error);
      throw new Error('AI Get Conversation By Id failed');
    }
  },

  AIDeleteConversationById: async (id: number) => {
    try {
      const headers = authHeaderAIGetConversations();
      const url = `${config.baseUrl}${config.AIConversation}${id}`;
      const response = await fetchWrapper.delete<AIDeleteRequestBody>(url, headers);
      if (!response) {
        throw new Error('Request failed');
      }
      return response;
    } catch (error) {
      console.error(error);
      throw new Error('AI Delete Conversation By Id failed');
    }
  },
};
