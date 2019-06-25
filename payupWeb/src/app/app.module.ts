import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { EmployeeListComponent } from "./components/employees/employee-list/employee-list.component";
import { EmployeeListItemComponent } from "./components/employees/employee-list-item/employee-list-item.component";
import { HeaderComponent } from "./components/header/header.component";

@NgModule({
  declarations: [
    AppComponent,
    EmployeeListComponent,
    EmployeeListItemComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
