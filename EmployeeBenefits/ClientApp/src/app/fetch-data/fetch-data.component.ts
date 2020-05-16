import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EmployeeService } from '../api/services/employee.service';
import { WeatherForecastService } from '../api/services/weather-forecast.service';
import { Employee } from '../api/models/employee';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  public employees: Employee[];

  constructor(private employeeService: EmployeeService, private weatherForecastService: WeatherForecastService
    //http: HttpClient, @Inject('BASE_URL') baseUrl: string
    ) {
    //http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
    //  this.forecasts = result;
    //}, error => console.error(error));
  }

  async ngOnInit() {
    this.getWeatherForecasts();
    this.getAllEmployees();
  }

  async getWeatherForecasts() {
    await this.weatherForecastService.weatherForecastGet$Json().subscribe((forecasts: WeatherForecast[]) => {
      this.forecasts = forecasts;
    });
  }

  async getAllEmployees() {
    await this.employeeService.employeeGet$Json().subscribe((e: Employee[]) => {
      this.employees = e;
    });
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
