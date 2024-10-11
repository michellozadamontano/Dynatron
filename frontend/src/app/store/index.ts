import { InjectionToken } from '@angular/core';
import { Action, ActionReducerMap, MetaReducer } from '@ngrx/store';

import { AppState } from './app.state';
import { customerReducer } from './customers/customer.reducer';
import { hydrationMetaReducer } from './hydration.reducer';

export const ROOT_REDUCERS = new InjectionToken<ActionReducerMap<AppState, Action>>('Root reducers token', {
  factory: () => ({
    customers: customerReducer
  })
});

export const metaReducers: MetaReducer<AppState>[] = [hydrationMetaReducer];
