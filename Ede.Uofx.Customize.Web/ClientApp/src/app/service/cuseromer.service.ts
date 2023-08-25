import { BasicApiService } from './basic-api.service';
import { DemoFieldInfo } from '../web/demo-field/write/demo-field.write.component';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CuseromerService extends BasicApiService{
  getbanks()
  {
    return this.http.get<DemoFieldInfo[]>("~/api/Banks/GetBank")
  }
}
