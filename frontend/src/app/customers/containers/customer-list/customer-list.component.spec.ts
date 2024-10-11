import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
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
  let store: MockStore<AppState>;
  const initialState = getInitialState();

  beforeEach(async () => {
    mockDialog = jasmine.createSpyObj('MatDialog', ['open']);
    store = jasmine.createSpyObj('Store', ['dispatch', 'select']);

    await TestBed.configureTestingModule({
      imports: [
        CustomerListComponent,
        MatDialogModule,
        NoopAnimationsModule,
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
      created: new Date(),
      lastUpdated: new Date()
    };


    const dialogRefSpy = jasmine.createSpyObj<MatDialogRef<any>>('MatDialogRef', ['afterClosed', 'close']);
    dialogRefSpy.afterClosed.and.returnValue(of(customer));

    mockDialog.open.and.returnValue(dialogRefSpy);

    component.openAddCustomerDialog();

    expect(mockDialog.open).toHaveBeenCalled();
    expect(store.dispatch).toHaveBeenCalledWith(customerActions.createCustomer({ customer }));
  });

});
