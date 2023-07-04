import { createStore, applyMiddleware, AnyAction } from 'redux';
import thunkMiddleware, { ThunkAction, ThunkDispatch } from 'redux-thunk';
import {loadAllChatMessages} from "../components/Chats/Chats.tsx";

// Define your initial state interface
interface AppState {
    conversations: Conversation[];
}

// Define your conversation interface
interface Conversation {
    text: string;
    questionAnswers: string[];
    id: number;
}

// Define your actions
const FETCH_CONVERSATIONS_SUCCESS = 'FETCH_CONVERSATIONS_SUCCESS';

interface FetchConversationsSuccessAction {
    type: typeof FETCH_CONVERSATIONS_SUCCESS;
    payload: Conversation[];
}

type AppAction = FetchConversationsSuccessAction;

const fetchConversationsSuccess = (conversations: Conversation[]): FetchConversationsSuccessAction => ({
    type: FETCH_CONVERSATIONS_SUCCESS,
    payload: conversations,
});

// Define your asynchronous action with Thunk
type AppThunk<ReturnType = void> = ThunkAction<ReturnType, AppState, unknown, AnyAction>;

export const fetchConversations = (): AppThunk => {
    return async (dispatch: ThunkDispatch<AppState, unknown, AppAction>): Promise<void> => {
        try {
            const response = await loadAllChatMessages();
            dispatch(fetchConversationsSuccess(response));
        } catch (error) {
            console.error('Error fetching conversations:', error);
        }
    };
};

// Define your reducer
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

// Create the Redux store
const store = createStore(reducer, applyMiddleware(thunkMiddleware));

export default store;
