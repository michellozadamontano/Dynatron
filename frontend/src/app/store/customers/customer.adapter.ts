import { createEntityAdapter, EntityAdapter } from '@ngrx/entity';

import { ICustomer } from '../../models/customer.model';

export const customerAdapter: EntityAdapter<ICustomer> = createEntityAdapter<ICustomer>({
  selectId: (customer: ICustomer) => customer.id
});