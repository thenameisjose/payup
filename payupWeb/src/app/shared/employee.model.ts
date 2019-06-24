import { Dependent } from "./dependent.model";

export class Employee {
  constructor(
    public id: string,
    public firstName: string,
    public lastName: string,
    public dependents: Dependent[]
  ) {}
}
