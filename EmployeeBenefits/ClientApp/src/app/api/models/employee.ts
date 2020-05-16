/* tslint:disable */
import { Dependent } from './dependent';
import { ObjectId } from './object-id';
export interface Employee {
  baseRate?: number;
  dependents?: null | Array<Dependent>;
  employeeNumber?: number;
  firstName?: null | string;
  id?: ObjectId;
  lastName?: null | string;
  spouse?: Dependent;
  ssn?: null | string;
}
