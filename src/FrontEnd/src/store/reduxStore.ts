import { createStore, applyMiddleware } from 'redux';
import thunkMiddleware, { ThunkDispatch } from 'redux-thunk';
import { loadAllChatMessages } from '../components/Chats/Chats.tsx';
import { AISearch } from '../common/interfaces.ts';
import { authService } from '../services/authService.ts';

interface AppState {
  conversations: Conversation[];
  aiSearchResponse: AISearchResponse;
}

interface Conversation {
  id: number;
  questionAnswers: [
    {
      question: string;
      answer: string;
    }
  ];
}

interface AISearchResponse {
  id: string;
  answer: string;
}

const FETCH_CONVERSATIONS_SUCCESS = 'FETCH_CONVERSATIONS_SUCCESS';
const FETCH_AI_SEARCH_RESPONSE_SUCCESS = 'FETCH_AI_SEARCH_RESPONSE_SUCCESS';

interface FetchConversationsSuccessAction {
  type: typeof FETCH_CONVERSATIONS_SUCCESS;
  payload: Conversation[];
}

interface FetchAISearchResponseSuccessAction {
  type: typeof FETCH_AI_SEARCH_RESPONSE_SUCCESS;
  payload: AISearchResponse;
}

type AppAction = FetchConversationsSuccessAction | FetchAISearchResponseSuccessAction;

export const fetchConversationsSuccess = (
  conversations: Conversation[]
): FetchConversationsSuccessAction => ({
  type: FETCH_CONVERSATIONS_SUCCESS,
  payload: conversations,
});

export const fetchAISearchResponseSuccess = (
  aiSearchResponse: AISearchResponse
): FetchAISearchResponseSuccessAction => ({
  type: FETCH_AI_SEARCH_RESPONSE_SUCCESS,
  payload: aiSearchResponse,
});

// type AppThunk<ReturnType = void> = ThunkAction<ReturnType, AppState, unknown, AppAction>;

export const fetchConversations = () => {
  return async (dispatch: ThunkDispatch<AppState, unknown, AppAction>): Promise<void> => {
    try {
      const response = await loadAllChatMessages();
      dispatch(fetchConversationsSuccess(response));
    } catch (error) {
      console.error('Error fetching conversations:', error);
    }
  };
};

export const fetchAISearchResponse = (search: AISearch) => {
  return async (dispatch: ThunkDispatch<AppState, unknown, AppAction>): Promise<void> => {
    try {
      const response = await authService.AISearchResponse(search);
      localStorage.setItem('conversationId', response.id);
      dispatch(fetchAISearchResponseSuccess(response));
    } catch (error) {
      console.error('Error fetching conversations:', error);
    }
  };
};

const initialState: AppState = {
  conversations: [],
  aiSearchResponse: { id: '', answer: '' },
};

const reducer = (state = initialState, action: AppAction): AppState => {
  switch (action.type) {
    case FETCH_CONVERSATIONS_SUCCESS:
      return { ...state, conversations: action.payload };
    case FETCH_AI_SEARCH_RESPONSE_SUCCESS:
      return { ...state, aiSearchResponse: action.payload };
    default:
      return state;
  }
};

const store = createStore(reducer, applyMiddleware(thunkMiddleware));

export default store;
