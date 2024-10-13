import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MockStore, provideMockStore } from '@ngrx/store/testing';
import { of } from 'rxjs';
import { AppState, getInitialState } from 'src/app/store/app.state';
import { CustomerListComponent } from './customer-list.component';

import { ICustomer } from 'src/app/models/customer.model';
import { customerActions } from '../../../store/customers/customer.action';
import { CustomerFormComponent } from '../../components/customer-form/customer-form.component';

describe('CustomerListComponent', () => {
  let component: CustomerListComponent;
  let fixture: ComponentFixture<CustomerListComponent>;

  let mockDialog: jasmine.SpyObj<MatDialog>;
  let dialogRefSpy: jasmine.SpyObj<MatDialogRef<any>>;
  let store: MockStore<AppState>;
  const initialState = getInitialState();

  beforeEach(async () => {
    // Creating a mock MatDialogRef to control behavior of the dialog
    dialogRefSpy = jasmine.createSpyObj<MatDialogRef<any>>('MatDialogRef', ['afterClosed', 'close']);

    await TestBed.configureTestingModule({
      imports: [
        CustomerListComponent,
        MatDialogModule,
        BrowserAnimationsModule,
        CustomerFormComponent
      ],
      providers: [
        { provide: MatDialog, useValue: mockDialog },
        provideMockStore({ initialState })
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(CustomerListComponent);
    component = fixture.componentInstance;
    store = TestBed.inject(MockStore);
    spyOn(store, 'dispatch');
    spyOn(store, 'select').and.returnValue(of([]));
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should open add customer dialog and dispatch createCustomer action on close', () => {
    const customer: ICustomer = {
      id: '1',
      firstName: 'John',
      lastName: 'Doe',
      email: 'john@example.com',
      created: undefined,
      lastUpdated: undefined
    };

    dialogRefSpy.afterClosed.and.returnValue(of(customer));

    spyOn(component.getDialog(), 'open').and.returnValue(dialogRefSpy);

    component.openAddCustomerDialog();

    expect(store.dispatch).toHaveBeenCalledWith(customerActions.createCustomer({ customer }));
  });
});
