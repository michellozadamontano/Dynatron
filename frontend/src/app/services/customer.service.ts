import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ICustomer } from '../models/customer.model';
import { IPaginationResponse } from '../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCustomers(pageNumber: number, pageSize: number): Observable<IPaginationResponse<ICustomer>> {
    return this.http.get<IPaginationResponse<ICustomer>>(`${this.apiUrl}/customers?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getCustomer(id: string): Observable<ICustomer> {
    return this.http.get<ICustomer>(`${this.apiUrl}/customers/${id}`);
  }

  filterCustomers(nameOrEmail: string, pageNumber: number, pageSize: number): Observable<IPaginationResponse<ICustomer>> {
    return this.http.get<IPaginationResponse<ICustomer>>(`${this.apiUrl}/customers-filter?nameOrEmail=${nameOrEmail}&pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  createCustomer(customer: ICustomer): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/customers`, customer);
  }

  updateCustomer(customer: ICustomer): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/customers/${customer.id}`, customer);
  }

  deleteCustomer(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/customers/${id}`);
  }
}
