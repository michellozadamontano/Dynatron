import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { ICustomer } from 'src/app/models/customer.model';

@Component({
  selector: 'app-customer-form',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    ReactiveFormsModule],
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.scss']
})
export class CustomerFormComponent {

  customerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CustomerFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ICustomer | null
  ) {
    this.customerForm = this.fb.group({
      firstName: [data?.firstName || '', Validators.required],
      lastName: [data?.lastName || '', Validators.required],
      email: [data?.email || '', [Validators.required, Validators.email]]
    });
  }

  onSubmit() {
    if (this.customerForm.valid) {
      this.dialogRef.close(this.customerForm.value);
    }
  }

  onCancel() {
    this.dialogRef.close(null);
  }

}
