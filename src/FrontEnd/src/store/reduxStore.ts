import { createStore, applyMiddleware } from 'redux';
import thunkMiddleware, { ThunkAction, ThunkDispatch } from 'redux-thunk';
import { loadAllChatMessages } from '../components/Chats/Chats.tsx';

interface AppState {
  conversations: Conversation[];
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
const FETCH_CONVERSATIONS_SUCCESS = 'FETCH_CONVERSATIONS_SUCCESS';

interface FetchConversationsSuccessAction {
  type: typeof FETCH_CONVERSATIONS_SUCCESS;
  payload: Conversation[];
}

type AppAction = FetchConversationsSuccessAction;

const fetchConversationsSuccess = (
  conversations: Conversation[]
): FetchConversationsSuccessAction => ({
  type: FETCH_CONVERSATIONS_SUCCESS,
  payload: conversations,
});

type AppThunk<ReturnType = void> = ThunkAction<ReturnType, AppState, unknown, AppAction>;

export const fetchConversations = (): AppThunk<void> => {
  return async (dispatch: ThunkDispatch<AppState, unknown, AppAction>): Promise<void> => {
    try {
      const response = await loadAllChatMessages();
      dispatch(fetchConversationsSuccess(response));
    } catch (error) {
      console.error('Error fetching conversations:', error);
    }
  };
};

const initialState: AppState = {
  conversations: [],
};

const reducer = (state = initialState, action: AppAction): AppState => {
  switch (action.type) {
    case FETCH_CONVERSATIONS_SUCCESS:
      return { ...state, conversations: action.payload };
    default:
      return state;
  }
};

const store = createStore(reducer, applyMiddleware(thunkMiddleware));

export default store;
