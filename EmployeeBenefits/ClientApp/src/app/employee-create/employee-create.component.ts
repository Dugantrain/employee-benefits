import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../api/services/employee.service';
import { Employee } from '../api/models/employee';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html',
  styleUrls: ['./employee-create.component.css']
})
export class EmployeeCreateComponent implements OnInit {
  public isMarried = false;
  constructor(private employeeService: EmployeeService, private router: Router) {
  }

  async ngOnInit() {
  }

  public async onSubmit() {
    this.router.navigate(['/list']);
  }
}
