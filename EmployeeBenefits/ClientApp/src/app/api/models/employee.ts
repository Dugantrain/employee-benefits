/* tslint:disable */
import { IBeneficiary } from './i-beneficiary';
export interface Employee {
  baseRate?: number;
  dependents?: null | Array<IBeneficiary>;
  firstName?: null | string;
  id?: number;
  lastName?: null | string;
  spouse?: IBeneficiary;
  ssn?: null | string;
}
