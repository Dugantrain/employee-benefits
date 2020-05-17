import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../api/services/employee.service';
import { Employee } from '../api/models/employee';
import { Dependent } from '../api/models/dependent';
import { Router } from '@angular/router';

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
  constructor(private employeeService: EmployeeService, private router: Router) {
  }

  async ngOnInit() {
  }

  public async onSubmit() {
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

  public async addDependent() {
    this.newEmployee.dependents.push(this.newDependent);
    this.newDependent = <Dependent>{firstName: "", lastName: ""};
  }

  public async removeDependent(dependent: Dependent) {
    this.newEmployee.dependents = this.newEmployee.dependents.filter((d: Dependent) => !(d.firstName === dependent.firstName &&
      d.lastName === dependent.lastName));
  }
}
