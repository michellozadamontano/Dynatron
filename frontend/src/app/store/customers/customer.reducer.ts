import { Action, createReducer, on } from '@ngrx/store';
import { customerActions } from './customer.action';
import { customerAdapter } from './customer.adapter';
import { CustomerState, initialCustomerState } from './customer.state';

export const customerReducer = createReducer(
  initialCustomerState,
  on(customerActions.loadCustomers, state => ({ ...state, loading: true })),
  on(customerActions.loadCustomersSuccess, (state, { response }) => {
    return {
      ...customerAdapter.setAll(response.result, state),
      pagination: {
        currentPage: response.currentPage,
        pageSize: response.pageSize,
        totalPages: response.totalPages,
        totalItems: response.totalItems
      },
      loading: false
    }
  }),
  on(customerActions.deleteCustomerSuccess, (state, { id }) => customerAdapter.removeOne(id, state))
);

export const reducer = (state: CustomerState | undefined, action: Action) => customerReducer(state, action);