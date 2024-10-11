import { createFeatureSelector, createSelector } from '@ngrx/store';

import { customerAdapter } from './customer.adapter';
import { CustomerState } from './customer.state';


export const getCustomersState = createFeatureSelector<CustomerState>('customers');

export const customerList = createSelector(
  getCustomersState,
  customerAdapter.getSelectors().selectAll
)
export const selectCustomerPagination = createSelector(
  getCustomersState,
  (state: CustomerState) => state.pagination
)
export const selectLoadingStatus = createSelector(
  getCustomersState,
  (state: CustomerState) => state.loading
)