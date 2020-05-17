/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';

import { Employee } from '../models/employee';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation employeeGet
   */
  static readonly EmployeeGetPath = '/Employee';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `employeeGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  employeeGet$Plain$Response(params?: {

  }): Observable<StrictHttpResponse<Array<Employee>>> {

    const rb = new RequestBuilder(this.rootUrl, EmployeeService.EmployeeGetPath, 'get');
    if (params) {


    }
    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<Employee>>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `employeeGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  employeeGet$Plain(params?: {

  }): Observable<Array<Employee>> {

    return this.employeeGet$Plain$Response(params).pipe(
      map((r: StrictHttpResponse<Array<Employee>>) => r.body as Array<Employee>)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `employeeGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  employeeGet$Json$Response(params?: {

  }): Observable<StrictHttpResponse<Array<Employee>>> {

    const rb = new RequestBuilder(this.rootUrl, EmployeeService.EmployeeGetPath, 'get');
    if (params) {


    }
    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<Employee>>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `employeeGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  employeeGet$Json(params?: {

  }): Observable<Array<Employee>> {

    return this.employeeGet$Json$Response(params).pipe(
      map((r: StrictHttpResponse<Array<Employee>>) => r.body as Array<Employee>)
    );
  }

  /**
   * Path part for operation employeePost
   */
  static readonly EmployeePostPath = '/Employee';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `employeePost$Plain()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  employeePost$Plain$Response(params?: {
      body?: Employee
  }): Observable<StrictHttpResponse<Employee>> {

    const rb = new RequestBuilder(this.rootUrl, EmployeeService.EmployeePostPath, 'post');
    if (params) {


      rb.body(params.body, 'application/*+json');
    }
    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Employee>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `employeePost$Plain$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  employeePost$Plain(params?: {
      body?: Employee
  }): Observable<Employee> {

    return this.employeePost$Plain$Response(params).pipe(
      map((r: StrictHttpResponse<Employee>) => r.body as Employee)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `employeePost$Json()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  employeePost$Json$Response(params?: {
      body?: Employee
  }): Observable<StrictHttpResponse<Employee>> {

    const rb = new RequestBuilder(this.rootUrl, EmployeeService.EmployeePostPath, 'post');
    if (params) {


      rb.body(params.body, 'application/*+json');
    }
    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Employee>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `employeePost$Json$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  employeePost$Json(params?: {
      body?: Employee
  }): Observable<Employee> {

    return this.employeePost$Json$Response(params).pipe(
      map((r: StrictHttpResponse<Employee>) => r.body as Employee)
    );
  }

  /**
   * Path part for operation employeeDelete
   */
  static readonly EmployeeDeletePath = '/Employee';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `employeeDelete()` instead.
   *
   * This method doesn't expect any request body.
   */
  employeeDelete$Response(params?: {
    id?: null | string;

  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, EmployeeService.EmployeeDeletePath, 'delete');
    if (params) {

      rb.query('id', params.id, {});

    }
    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `employeeDelete$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  employeeDelete(params?: {
    id?: null | string;

  }): Observable<void> {

    return this.employeeDelete$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation employeePatch
   */
  static readonly EmployeePatchPath = '/Employee';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `employeePatch$Plain()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  employeePatch$Plain$Response(params?: {
      body?: Employee
  }): Observable<StrictHttpResponse<Employee>> {

    const rb = new RequestBuilder(this.rootUrl, EmployeeService.EmployeePatchPath, 'patch');
    if (params) {


      rb.body(params.body, 'application/*+json');
    }
    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Employee>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `employeePatch$Plain$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  employeePatch$Plain(params?: {
      body?: Employee
  }): Observable<Employee> {

    return this.employeePatch$Plain$Response(params).pipe(
      map((r: StrictHttpResponse<Employee>) => r.body as Employee)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `employeePatch$Json()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  employeePatch$Json$Response(params?: {
      body?: Employee
  }): Observable<StrictHttpResponse<Employee>> {

    const rb = new RequestBuilder(this.rootUrl, EmployeeService.EmployeePatchPath, 'patch');
    if (params) {


      rb.body(params.body, 'application/*+json');
    }
    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Employee>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `employeePatch$Json$Response()` instead.
   *
   * This method sends `application/*+json` and handles request body of type `application/*+json`.
   */
  employeePatch$Json(params?: {
      body?: Employee
  }): Observable<Employee> {

    return this.employeePatch$Json$Response(params).pipe(
      map((r: StrictHttpResponse<Employee>) => r.body as Employee)
    );
  }

  /**
   * Path part for operation employeeIdGet
   */
  static readonly EmployeeIdGetPath = '/Employee/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `employeeIdGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  employeeIdGet$Plain$Response(params: {
    id: null | string;

  }): Observable<StrictHttpResponse<Employee>> {

    const rb = new RequestBuilder(this.rootUrl, EmployeeService.EmployeeIdGetPath, 'get');
    if (params) {

      rb.path('id', params.id, {});

    }
    return this.http.request(rb.build({
      responseType: 'text',
      accept: 'text/plain'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Employee>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `employeeIdGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  employeeIdGet$Plain(params: {
    id: null | string;

  }): Observable<Employee> {

    return this.employeeIdGet$Plain$Response(params).pipe(
      map((r: StrictHttpResponse<Employee>) => r.body as Employee)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `employeeIdGet$Json()` instead.
   *
   * This method doesn't expect any request body.
   */
  employeeIdGet$Json$Response(params: {
    id: null | string;

  }): Observable<StrictHttpResponse<Employee>> {

    const rb = new RequestBuilder(this.rootUrl, EmployeeService.EmployeeIdGetPath, 'get');
    if (params) {

      rb.path('id', params.id, {});

    }
    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'text/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Employee>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `employeeIdGet$Json$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  employeeIdGet$Json(params: {
    id: null | string;

  }): Observable<Employee> {

    return this.employeeIdGet$Json$Response(params).pipe(
      map((r: StrictHttpResponse<Employee>) => r.body as Employee)
    );
  }

}
