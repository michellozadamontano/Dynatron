import * as fromCustomer from './customers/customer.state';

export interface AppState {
  customers: fromCustomer.CustomerState
}

export const initialAppState: AppState = {
  customers: fromCustomer.initialCustomerState
}

export function getInitialState(): AppState {
  return initialAppState;
}