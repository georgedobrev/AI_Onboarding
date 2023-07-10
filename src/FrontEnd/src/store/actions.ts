export interface AppState {
  conversations: Conversation[];
  aiSearchResponse: AISearchResponse;
}

export interface Conversation {
  id: number;
  name: string;
}

export interface AISearchResponse {
  id: string;
  answer: string;
}

const FETCH_CONVERSATIONS_SUCCESS = 'FETCH_CONVERSATIONS_SUCCESS';
const FETCH_AI_SEARCH_RESPONSE_SUCCESS = 'FETCH_AI_SEARCH_RESPONSE_SUCCESS';

export interface FetchConversationsSuccessAction {
  type: typeof FETCH_CONVERSATIONS_SUCCESS;
  payload: Conversation[];
}

export interface FetchAISearchResponseSuccessAction {
  type: typeof FETCH_AI_SEARCH_RESPONSE_SUCCESS;
  payload: AISearchResponse;
}

export type AppAction = FetchConversationsSuccessAction | FetchAISearchResponseSuccessAction;
