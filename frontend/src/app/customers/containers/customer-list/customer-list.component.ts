import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { Store } from '@ngrx/store';
import { Subject, takeUntil } from 'rxjs';
import { ICustomer } from 'src/app/models/customer.model';
import Swal from 'sweetalert2';
import { AppState } from '../../../store/app.state';
import { customerActions } from '../../../store/customers/customer.action';
import * as selectors from '../../../store/customers/customer.selector';
import { CustomerFormComponent } from '../../components/customer-form/customer-form.component';


@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatIconModule,
    MatPaginatorModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
    CustomerFormComponent
  ],
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.scss']
})
export class CustomerListComponent implements OnInit, OnDestroy {
  unsubscribe$ = new Subject<void>();
  totalCustomers = 0;
  pageNumber: number = 1;
  pageSize: number = 5;
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'email', 'created', 'lastUpdated', 'actions'];
  customers: ICustomer[] = [];
  loading$ = this.store.select(selectors.selectLoadingStatus);

  constructor(
    private dialog: MatDialog,
    private store: Store<AppState>
  ) {

  }

  /**
   * Called when the component is initialized.
   * Loads the customers from the store and sets up the observables to
   * watch for changes in the customers list and the pagination.
   */
  ngOnInit(): void {
    // Load the customers from the store
    this.loadCustomers();

    // Watch for changes in the customers list and update the component
    this.store.select(selectors.customerList).pipe(takeUntil(this.unsubscribe$)).subscribe(customers => {
      this.customers = customers;
    });

    // Watch for changes in the pagination and update the component
    this.store.select(selectors.selectCustomerPagination).pipe(takeUntil(this.unsubscribe$)).subscribe(pagination => {
      this.totalCustomers = pagination.totalItems;
      this.pageNumber = pagination.currentPage;
      this.pageSize = pagination.pageSize;
    });
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public getDialog(): MatDialog {
    return this.dialog;
  }

  /**
   * Opens the add customer dialog and dispatches the createCustomer action
   * when the dialog is closed with a valid customer object.
   */
  openAddCustomerDialog() {
    const dialogRef = this.dialog.open(CustomerFormComponent, {
      width: '400px',
      data: null
    });

    dialogRef.afterClosed().subscribe((result: ICustomer | null) => {
      if (result) {
        // Dispatch the createCustomer action with the customer object
        this.store.dispatch(customerActions.createCustomer({ customer: result }));
      }
    });
  }

  /**
   * Opens the edit customer dialog and dispatches the updateCustomer action
   * when the dialog is closed with a valid customer object.
   * @param customer The customer object to be edited.
   */
  openEditCustomerDialog(customer: ICustomer) {
    const dialogRef = this.dialog.open(CustomerFormComponent, {
      width: '400px',
      data: customer // Pass the customer object to the dialog
    });

    dialogRef.afterClosed().subscribe((result: ICustomer | null) => {
      if (result) {
        // Dispatch the updateCustomer action with the customer object
        // and the new values from the dialog.
        this.store.dispatch(customerActions.updateCustomer({ customer: { ...customer, ...result } }));
      }
    });
  }

  loadCustomers() {
    this.store.dispatch(customerActions.loadCustomers({ pageNumber: this.pageNumber, pageSize: this.pageSize }));
  }

  onPageChange(event: PageEvent) {
    this.pageNumber = event.pageIndex + 1; // Angular paginator is zero-indexed
    this.pageSize = event.pageSize;
    this.loadCustomers();
  }

  applyFilter(event: any) {
    const filterValue = (event.target as HTMLInputElement).value;

    if (filterValue) {
      this.pageNumber = 1;
      this.pageSize = 100;
      this.store.dispatch(customerActions.filterCustomers({ nameOrEmail: filterValue, pageNumber: this.pageNumber, pageSize: this.pageSize }));
    } else {
      this.pageNumber = 1;
      this.pageSize = 5;
      this.store.dispatch(customerActions.loadCustomers({ pageNumber: this.pageNumber, pageSize: this.pageSize }));
    }

  }

  deleteCustomer(id: string) {
    console.log(id);
    Swal.fire({
      title: 'Warning!',
      text: 'Do you want to delete this customer',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Accept',
    }).then((result) => {
      if (result.isConfirmed) {
        this.store.dispatch(customerActions.deleteCustomer({ id }));
      }
    })
  }
}
