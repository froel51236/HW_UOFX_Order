import { BasicApiService } from './basic-api.service';
import { Employees } from '../web/hw-order-field/write/hw-order-field.write.component';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService extends BasicApiService{
  getemployees()
  {
    return this.http.get<Employees[]>("~/api/Employees/GetEmployees")
  }
}
