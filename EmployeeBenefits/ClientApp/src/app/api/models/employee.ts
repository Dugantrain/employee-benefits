/* tslint:disable */
import { Dependent } from './dependent';
export interface Employee {
  dependents?: null | Array<Dependent>;
  discountApplied?: boolean;
  employeeIdentifier?: null | string;
  firstName?: null | string;
  id?: null | string;
  lastName?: null | string;
  payPeriodNetBenefitsCost?: number;
  spouse?: Dependent;
  weeklyPayRate?: number;
  yearlyBenefitsCost?: number;
  yearlyNetBenefitsCost?: number;
  yearlyPayRate?: number;
}
