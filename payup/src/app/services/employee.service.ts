import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { throwError, of } from "rxjs";
import { catchError } from "rxjs/operators";

import { Employee } from "../shared/employee.model";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root"
})
export class EmployeeService {
  url = environment.apiUrl;
  private employees: Employee[] = [];

  constructor(private http: HttpClient) {}

  getAllEmployees(): Observable<Employee[]> {
    return this.http
      .get<Employee[]>(this.url + "/benefitsummary")
      .pipe(catchError(this.handleError));
  }

  updateEmployee(employee: Employee): Observable<Employee> {
    const httpOptions = {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    };
    return this.http
      .put<Employee>(this.url + "/benefitsummary", employee, httpOptions)
      .pipe(catchError(this.handleError));
  }

  create(employee: Employee): Observable<Employee> {
    const httpOptions = {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    };
    return this.http
      .post<Employee>(this.url + "/benefitsummary", employee, httpOptions)
      .pipe(catchError(this.handleError));
  }

  deleteEmployeeById(id: string): Observable<number> {
    const httpOptions = {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    };
    return this.http
      .delete<number>(this.url + "/BenefitSummary/" + id, httpOptions)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: Response | any) {
    console.error("EmployeeService::handleError", error);
    return Observable.throw(error);
  }
}
