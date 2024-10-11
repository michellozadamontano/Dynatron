import { EntityState } from "@ngrx/entity";
import { ICustomer } from "../../models/customer.model";

export interface CustomerState extends EntityState<ICustomer> {
  loading: boolean;
  pagination: {
    currentPage: number;
    pageSize: number;
    totalPages: number;
    totalItems: number;
  };
}

export const initialCustomerState: CustomerState = {
  ids: [],
  entities: {},
  loading: false,
  pagination: {
    currentPage: 1,
    pageSize: 10,
    totalPages: 0,
    totalItems: 0
  }
}