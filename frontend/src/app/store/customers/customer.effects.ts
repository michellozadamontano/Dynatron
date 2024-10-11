import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, concatMap, map, mergeMap, switchMap } from 'rxjs/operators';
import { CustomerService } from '../../services/customer.service';
import { customerActions } from './customer.action';

@Injectable()
export class CustomerEffects {

  loadCustomer$ = createEffect(() =>
    this.actions$.pipe(
      ofType(customerActions.loadCustomers),
      switchMap((action) => {
        return this.customerService.getCustomers(action.pageNumber, action.pageSize).pipe(
          map(response => customerActions.loadCustomersSuccess({ response })),
          catchError(error => of(customerActions.loadCustomersFailure({ error })))
        );
      })
    )
  );

  filterCustomer$ = createEffect(() =>
    this.actions$.pipe(
      ofType(customerActions.filterCustomers),
      switchMap((action) => {
        return this.customerService.filterCustomers(action.nameOrEmail, action.pageNumber, action.pageSize).pipe(
          map(response => customerActions.loadCustomersSuccess({ response })),
          catchError(error => of(customerActions.loadCustomersFailure({ error })))
        );
      })
    )
  );


  createCustomer$ = createEffect(() =>
    this.actions$.pipe(
      ofType(customerActions.createCustomer),
      concatMap((action) => {
        return this.customerService.createCustomer(action.customer).pipe(
          mergeMap(() => [
            customerActions.createCustomerSuccess(),
            customerActions.loadCustomers({ pageNumber: 1, pageSize: 5 })
          ]),
          catchError(error => of(customerActions.createCustomerFailure({ error })))
        );
      })
    )
  );

  updateCustomer$ = createEffect(() =>
    this.actions$.pipe(
      ofType(customerActions.updateCustomer),
      concatMap((action) => {
        return this.customerService.updateCustomer(action.customer).pipe(
          mergeMap(() => [
            customerActions.updateCustomerSuccess(),
            customerActions.loadCustomers({ pageNumber: 1, pageSize: 5 })
          ]),
          catchError(error => of(customerActions.updateCustomerFailure({ error })))
        );
      })
    )
  );

  deleteCustomer$ = createEffect(() =>
    this.actions$.pipe(
      ofType(customerActions.deleteCustomer),
      concatMap((action) => {
        return this.customerService.deleteCustomer(action.id).pipe(
          mergeMap(() => [
            customerActions.deleteCustomerSuccess({ id: action.id }),
            customerActions.loadCustomers({ pageNumber: 1, pageSize: 5 })
          ]),
          catchError(error => of(customerActions.deleteCustomerFailure({ error })))
        );
      })
    )
  );

  constructor(
    private actions$: Actions,
    private customerService: CustomerService
  ) {
  }
}