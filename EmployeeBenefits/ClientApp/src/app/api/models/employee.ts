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
  periodPayRate?: number;
  spouse?: Dependent;
  totalYearlyNetBenefitsCost?: number;
  yearlyBaseBenefitsCost?: number;
  yearlyNetBenefitsCost?: number;
  yearlyPayRate?: number;
}
