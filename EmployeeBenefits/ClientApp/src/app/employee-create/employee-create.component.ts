import { Component, OnInit, Input } from '@angular/core';
import { EmployeeService } from '../api/services/employee.service';
import { Employee } from '../api/models/employee';
import { Dependent } from '../api/models/dependent';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl,Validators } from '@angular/forms';

@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html',
  styleUrls: ['./employee-create.component.css']
})
export class EmployeeCreateComponent implements OnInit {
  public isLoading = false;
  public isMarried = false;
  public upsertEmployee: Employee = <Employee>{
    spouse: <Dependent>{},
    dependents: <Dependent[]>[]
  };
  public scenarioEmployee: Employee;
  public newDependent = <Dependent>{ firstName: "", lastName: "" };
  public employeeIdentifierExists = false;
  public employeeForm: FormGroup;
  public formSubmitted = false;
  public isUpdatingExistingEmployee = false;

  constructor(private employeeService: EmployeeService, private router: Router, private route: ActivatedRoute) {
  }

  async ngOnInit() {
    this.isLoading = true;
    this.employeeForm = new FormGroup({
      employeeIdentifier: new FormControl('',Validators.required),
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      isMarried: new FormControl(),
      spouseFirstName: new FormControl(),
      spouseLastName: new FormControl(),
      dependentFirstName: new FormControl(),
      dependentLastName: new FormControl()
    });
    this.subscribeIsMarriedOnChange();
    this.subscribeOnFormChange();
    this.determineCreateOrUpdateBasedOnRoute();
  }

  public determineCreateOrUpdateBasedOnRoute() {
    this.route.params.subscribe(params => {
      const id = params['id'];
      if (id) {
        this.isUpdatingExistingEmployee = true;
        this.employeeService.employeeIdGet$Json({ id: id }).subscribe((e) => {
          this.upsertEmployee = e;
          if (this.upsertEmployee.spouse) {
            this.isMarried = true;
          } else {
            this.upsertEmployee.spouse = <Dependent>{};
          }
          if (!this.upsertEmployee.dependents) {
            this.upsertEmployee.dependents = <Dependent[]>[];
          }
        });
      } else {
        this.upsertEmployee = <Employee>{
          spouse: <Dependent>{},
          dependents: <Dependent[]>[]
        };
      }
      this.isLoading = false;
    });
  }

  public subscribeIsMarriedOnChange() {
    const isMarriedFormControl = this.employeeForm.controls.isMarried;
    const changes$ = isMarriedFormControl.valueChanges;
    changes$.subscribe((isMarried: boolean) => {
      if (isMarried) {
        this.employeeForm.controls.spouseFirstName.setValidators([Validators.required]);
        this.employeeForm.controls.spouseLastName.setValidators([Validators.required]);
      } else {
        this.employeeForm.controls.spouseFirstName.setValidators(null);
        this.employeeForm.controls.spouseLastName.setValidators(null);
      }
      this.employeeForm.controls.spouseFirstName.updateValueAndValidity();
      this.employeeForm.controls.spouseLastName.updateValueAndValidity();
    });
  }

  public subscribeOnFormChange() {
    this.employeeForm.valueChanges.subscribe((val) => {
      if (this.employeeForm.valid) {
        this.calculateCostsAndDeductions();
      } else {
        this.scenarioEmployee = <Employee>null;
      }
    });
  }

  public async onSubmit() {
    this.formSubmitted = true;
    if (this.employeeForm.invalid) return;
    if (!this.isMarried) {
      this.upsertEmployee.spouse = <Dependent>null;
    }
    if (this.upsertEmployee.dependents !== null && this.upsertEmployee.dependents.length === 0) {
      this.upsertEmployee.dependents = <Dependent[]>null;
    };
    this.employeeService.employeePost$Json({
      body: this.upsertEmployee
    }).subscribe((e: Employee) => {
      this.router.navigate(['/list']);
    });
  }

  public async getEmployeeIdentifierExists() {
    this.employeeService.employeeEmployeeIdentifierEmployeeIdentifierGet$Json(
      { employeeIdentifier: this.upsertEmployee.employeeIdentifier })
      .subscribe((e: Employee) => {
        if (e) {
          this.employeeIdentifierExists = true;
          this.employeeForm.controls.employeeIdentifier.setErrors({ 'incorrect': true });
        } else {
          this.employeeIdentifierExists = false;
          this.employeeForm.controls.employeeIdentifier.setErrors(null);
        }
      });
  }

  public async calculateCostsAndDeductions() {
    this.employeeService.employeeBenefitsPatch$Json({ body: this.upsertEmployee })
      .subscribe((e: Employee) => {
        this.scenarioEmployee = e;
      });
  }

  public async addDependent() {
    if (!this.newDependent.firstName || this.newDependent.firstName.trim() === ""
      || !this.newDependent.lastName || this.newDependent.lastName.trim() === "") return;
    let existingDependent = this.upsertEmployee.dependents.find((d: Dependent) => d.firstName.toLowerCase() === this.newDependent.firstName.toLowerCase() &&
      d.lastName.toLowerCase() === this.newDependent.lastName.toLowerCase());
    if (existingDependent) return;
    this.upsertEmployee.dependents.push(this.newDependent);
    this.newDependent = <Dependent>{ firstName: "", lastName: "" };
    if (this.employeeForm.valid) {
      this.calculateCostsAndDeductions();
    }
  }

  public async removeDependent(dependent: Dependent) {
    this.upsertEmployee.dependents = this.upsertEmployee.dependents.filter((d: Dependent) => !(d.firstName === dependent.firstName &&
      d.lastName === dependent.lastName));
    if (this.employeeForm.valid) {
      this.calculateCostsAndDeductions();
    }
  }
}
