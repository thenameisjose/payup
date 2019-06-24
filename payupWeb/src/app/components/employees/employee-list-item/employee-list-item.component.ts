import { Component, OnInit, Input, EventEmitter, Output } from "@angular/core";
import { Employee } from "src/app/shared/employee.model";

@Component({
  selector: "app-employee-list-item",
  templateUrl: "./employee-list-item.component.html",
  styleUrls: ["./employee-list-item.component.css"]
})
export class EmployeeListItemComponent implements OnInit {
  @Input() employee: Employee;
  @Output() onEditEmployee = new EventEmitter();
  @Output() onDeleteEmployee = new EventEmitter();
  showDetails: boolean = false;

  onToggleDetails() {
    this.showDetails = !this.showDetails;
  }

  constructor() {}

  ngOnInit() {}
}
