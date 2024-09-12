export class Contact {
  id: number;
  name: string;
  birthDate: string;
  married: boolean;
  phone: string;
  salary: number;

  constructor(
    id: number,
    name: string,
    birthDate: string,
    married: boolean,
    phone: string,
    salary: number
  ) {
    this.id = id;
    this.name = name;
    this.birthDate = birthDate;
    this.married = married;
    this.phone = phone;
    this.salary = salary;
  }
}
