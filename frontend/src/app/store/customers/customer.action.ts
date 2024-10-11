import { createActionGroup, emptyProps, props } from '@ngrx/store';
import { ICustomer } from '../../models/customer.model';
import { IPaginationResponse } from '../../models/response.model';

export const customerActions = createActionGroup({
  source: 'Customers',
  events: {
    'Load Customers': props<{ pageNumber: number, pageSize: number }>(),
    'Load Customers Success': props<{ response: IPaginationResponse<ICustomer> }>(),
    'Load Customers Failure': props<{ error: any }>(),
    'Filter Customers': props<{ nameOrEmail: string, pageNumber: number, pageSize: number }>(),
    'Create Customer': props<{ customer: ICustomer }>(),
    'Create Customer Success': emptyProps(),
    'Create Customer Failure': props<{ error: any }>(),
    'Update Customer': props<{ customer: ICustomer }>(),
    'Update Customer Success': emptyProps(),
    'Update Customer Failure': props<{ error: any }>(),
    'Delete Customer': props<{ id: string }>(),
    'Delete Customer Success': props<{ id: string }>(),
    'Delete Customer Failure': props<{ error: any }>()
  }
});