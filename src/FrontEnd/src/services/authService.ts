import jwt_decode from 'jwt-decode';
import { errorNotifications } from '../components/Notifications/Notifications.tsx';
import config from '../config.json';
import { authHeaderAI, authHeaderAIGetConversations } from './commonConfig.ts';
import { fetchWrapper } from './fetchWrapper.tsx';
import { FormValues as SignInForms } from '../components/SignIn/types.ts';
import { FormValues as RegisterForms } from '../components/Register/types.ts';
import { ExtendSessionFormValues } from '../components/SignIn/types.ts';
import { AISearch } from '../common/interfaces.ts';

interface LoginResponse {
  accessToken?: string;
  refreshToken?: string;
}

interface RequestLoginBody {
  email?: string;
  password?: string;
}

interface ExtendSessionRequestBody {
  token: string;
  refreshToken: string;
}

interface GoogleLoginRequestBody {
  token: string | undefined;
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

interface validateConfirmTokenRequestBody {
  token: string;
  email: string;
}

interface validateConfirmTokenResponse {
  message: string;
}

interface AIDeleteRequestBody {
  id: number;
}

interface TokenPayload {
  aud: string;
  exp: number;
  [key: string]: string | number;
}
interface errorResponse {
  response: {
    data: string;
  };
}

export const authService = {
  login: async (formData: SignInForms) => {
    try {
      const url = `${config.baseUrl}${config.loginEndpoint}`;
      const headers = { headers: { 'Content-Type': 'application/json' } };
      const body: RequestLoginBody = formData;
      const response = await fetchWrapper.post<LoginResponse, RequestLoginBody>(url, body, headers);
      const accessToken = response.headers['access-token'];
      const refreshToken = response.headers['refresh-token'];

      if (!accessToken || !refreshToken) {
        throw new Error('Access or refresh token not found');
      }
      const expirationDate = new Date();
      expirationDate.setUTCDate(expirationDate.getUTCDate() + 5);
      localStorage.setItem('accessToken', accessToken);
      localStorage.setItem('refreshToken', refreshToken);

      const tokenPayload: TokenPayload = jwt_decode(accessToken);
      const userRole: string = tokenPayload[config.JWTUserRole] as string;
      localStorage.setItem('userRole', userRole);

      const expirationTime = tokenPayload.exp * 1000;
      const currentTime = new Date().getTime();
      const remainingTime = expirationTime - currentTime;

      setTimeout(() => {
        // TODO in next branch
      }, remainingTime);

      return response;
    } catch (error) {
      if (
        (error as errorResponse).response?.data ===
        'Email is not confirmed. Please confirm your email.'
      ) {
        errorNotifications('Email is not confirmed. Please confirm your email.', {
          autoClose: 3000,
        });
      } else {
        errorNotifications('Login failed', { autoClose: 3000 });
        throw new Error('Login failed');
      }
    }
  },

  register: async (formData: RegisterForms) => {
    delete formData['confirmPassword'];
    const { ...registerData } = formData;
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

  googleLogin: async (formData: GoogleLoginRequestBody) => {
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
      localStorage.setItem('accessToken', response.headers['access-token']);
      localStorage.setItem('refreshToken', response.headers['refresh-token']);
      return response;
    } catch (error) {
      console.error(error);
      throw new Error('Google login failed');
    }
  },

  forgotPassword: async (formData: forgotPasswordRequestBody) => {
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

  resetPassword: async (formData: resetPasswordRequestBody) => {
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

  validateResetToken: async (formData: validateResetTokenRequestBody) => {
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

  validateConfirmToken: async (formData: validateConfirmTokenRequestBody) => {
    try {
      const headers = { headers: { 'Content-Type': 'application/json' } };
      const url = `${config.baseUrl}${config.validateConfirmTokenEndpoint}`;
      const response = await fetchWrapper.post<
        validateConfirmTokenResponse,
        validateConfirmTokenRequestBody
      >(url, formData, headers);
      if (!response) {
        throw new Error('Request failed');
      }
      return response;
    } catch (error) {
      console.error(error);
      throw new Error('Validate confirm token failed');
    }
  },

  AISearchResponse: async (formData: AISearch) => {
    try {
      const headers = authHeaderAI();
      const url = `${config.baseUrl}${config.AISearchEndpoint}`;
      const response = await fetchWrapper.post<AISearchResponse, AISearch>(url, formData, headers);
      if (!response) {
        throw new Error('Request failed');
      }
      return response.data;
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
