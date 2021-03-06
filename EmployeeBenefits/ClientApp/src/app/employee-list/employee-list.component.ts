import { Component } from '@angular/core';
import { EmployeeService } from '../api/services/employee.service';
import { Employee } from '../api/models/employee';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html'
})
export class EmployeeListComponent {
  public employees: Employee[];

  constructor(private employeeService: EmployeeService) {
  }

  public async ngOnInit() {
    this.getAllEmployees();
  }

  public async getAllEmployees() {
    await this.employeeService.employeeGet$Json().subscribe((e: Employee[]) => {
      this.employees = e;
    });
  }

  public async removeEmployee(id: string) {
    await this.employeeService.employeeDelete({id: id}).subscribe(() => {
      this.getAllEmployees();
    });
  }
}
