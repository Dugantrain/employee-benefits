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
    spouse: <Dependent>{}
  };
  constructor(private employeeService: EmployeeService, private router: Router) {
  }

  async ngOnInit() {
  }

  public async onSubmit() {
    if (!this.isMarried) {
      this.newEmployee.spouse = <Dependent>null;
    }
    this.employeeService.employeePost$Json({
      body: this.newEmployee
    }).subscribe((e: Employee) => {
      this.router.navigate(['/list']);
    });
  }
}
