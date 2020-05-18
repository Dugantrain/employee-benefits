import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../api/services/employee.service';
import { Employee } from '../api/models/employee';
import { Dependent } from '../api/models/dependent';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl,Validators } from '@angular/forms';

@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html',
  styleUrls: ['./employee-create.component.css']
})
export class EmployeeCreateComponent implements OnInit {
  public isMarried = false;
  public newEmployee: Employee = <Employee>{
    spouse: <Dependent>{},
    dependents: <Dependent[]>[]
  };
  public newDependent = <Dependent>{ firstName: "", lastName: "" };
  public employeeIdentifierExists = false;
  public employeeForm: FormGroup;
  public formSubmitted = false;

  constructor(private employeeService: EmployeeService, private router: Router, private formBuilder: FormBuilder) {
  }

  async ngOnInit() {
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

  //// convenience getter for easy access to form fields
  //get f() { return this.employeeForm.controls; }

  public async onSubmit() {
    this.formSubmitted = true;
    if (this.employeeForm.invalid) return;
    if (!this.isMarried) {
      this.newEmployee.spouse = <Dependent>null;
    }
    if (this.newEmployee.dependents !== null && this.newEmployee.dependents.length === 0) {
      this.newEmployee.dependents = <Dependent[]>null;
    };
    this.employeeService.employeePost$Json({
      body: this.newEmployee
    }).subscribe((e: Employee) => {
      this.router.navigate(['/list']);
    });
  }

  public async getEmployeeIdentifierExists() {
    this.employeeService.employeeEmployeeIdentifierEmployeeIdentifierGet$Json(
      { employeeIdentifier: this.newEmployee.employeeIdentifier })
      .subscribe((e: Employee) => {
        if (e) {
          this.employeeIdentifierExists = true;
        }
        this.employeeIdentifierExists = false;
      });
  }

  public async addDependent() {
    this.newEmployee.dependents.push(this.newDependent);
    this.newDependent = <Dependent>{firstName: "", lastName: ""};
  }

  public async removeDependent(dependent: Dependent) {
    this.newEmployee.dependents = this.newEmployee.dependents.filter((d: Dependent) => !(d.firstName === dependent.firstName &&
      d.lastName === dependent.lastName));
  }
}
