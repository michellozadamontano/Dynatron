import {
  ActionReducer,
  INIT,
  UPDATE
} from "@ngrx/store";
//-------------------------------------------------------------------------------------
// Imports State
//-------------------------------------------------------------------------------------
import { AppState } from './app.state';

export const hydrationMetaReducer = (
  reducer: ActionReducer<AppState>
): ActionReducer<AppState> => {
  return (state, action) => {
    if (action.type === INIT || action.type === UPDATE) {
      const storageValue = localStorage.getItem("dynatron-state");
      if (storageValue) {
        try {
          return JSON.parse(storageValue);
        } catch {
          localStorage.removeItem("dynatron-state");
        }
      }
    }
    const nextState = reducer(state, action);
    localStorage.setItem("dynatron-state", JSON.stringify(nextState));
    return nextState;
  };
};
