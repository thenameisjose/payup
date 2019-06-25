import { Component, OnInit } from "@angular/core";
import { EmployeeService } from "../../../services/employee.service";
import { Employee } from "../../../shared/employee.model";
import { FormGroup, FormArray, FormControl, Validators } from "@angular/forms";
import { Observable } from "rxjs";

@Component({
  selector: "app-employee-list",
  templateUrl: "./employee-list.component.html",
  styleUrls: ["./employee-list.component.css"]
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[];
  selectedEmployee: Employee;
  showForm: boolean;
  employeeForm: FormGroup;
  editMode: boolean;
  constructor(private employeeService: EmployeeService) {}

  onAddEmployee() {
    this.showForm = true;
  }

  handleEditEmployee(employee: Employee) {
    console.log(employee);
    this.editMode = true;
    this.selectedEmployee = employee;
    this.initForm();
    this.showForm = true;
  }

  handleDeleteEmployee(id: string) {
    this.employeeService.deleteEmployeeById(id).subscribe(_ => {
      console.log(this.employees);
      this.employees = this.employees.filter(e => e.id !== id);
      console.log(this.employees);
    });

    this.initForm();
  }

  onCancelForm() {
    this.showForm = false;
    this.editMode = false;
    this.selectedEmployee = null;
    this.initForm();
  }

  createEmployee(employee: Employee) {
    this.employeeService.create(employee).subscribe(emp => {
      this.employees = this.employees.concat(emp);
      this.onCancelForm();
    });
  }

  updateEmployee(employee: Employee) {
    this.employeeService.updateEmployee(employee).subscribe(emp => {
      let index = this.employees.findIndex(e => e.id == employee.id);
      if (index > -1) {
        this.employees = [
          ...this.employees.slice(0, index),
          emp,
          ...this.employees.slice(index + 1)
        ];
      }
      this.onCancelForm();
    });
  }

  onSubmitForm() {
    if (this.editMode) {
      this.updateEmployee(this.employeeForm.value);
    } else {
      this.createEmployee(this.employeeForm.value);
    }
  }

  private initForm() {
    let firstName = "";
    let lastName = "";
    let id = null;
    let employeeDependents = new FormArray([]);

    if (this.selectedEmployee) {
      id = this.selectedEmployee.id;
      firstName = this.selectedEmployee.firstName;
      lastName = this.selectedEmployee.lastName;
      if (this.selectedEmployee["dependents"]) {
        for (let dep of this.selectedEmployee.dependents) {
          employeeDependents.push(
            new FormGroup({
              firstName: new FormControl(dep.firstName, Validators.required),
              lastName: new FormControl(dep.lastName, Validators.required),
              relationship: new FormControl(
                dep.relationship,
                Validators.required
              )
            })
          );
        }
      }
    }

    this.employeeForm = new FormGroup({
      firstName: new FormControl(firstName, Validators.required),
      lastName: new FormControl(lastName, Validators.required),
      id: new FormControl(id),
      dependents: employeeDependents
    });
  }

  onAddDependent() {
    (<FormArray>this.employeeForm.get("dependents")).push(
      new FormGroup({
        firstName: new FormControl(null, Validators.required),
        lastName: new FormControl(null, Validators.required),
        relationship: new FormControl(null, Validators.required)
      })
    );
  }

  onDeleteDependent(index: number) {
    (<FormArray>this.employeeForm.get("dependents")).removeAt(index);
  }

  loadEmployees() {
    this.employeeService.getAllEmployees().subscribe(employees => {
      console.log({employees});
      this.employees = employees;
    });
  }

  ngOnInit() {
    this.loadEmployees();
    this.initForm();
  }
}
