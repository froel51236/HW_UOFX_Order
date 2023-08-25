import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { FormDirtyConfirm } from '@uofx/core';
import { UofxDialog, UofxDialogController } from '@uofx/web-components/dialog';
import {
  Grid,
  GridComponent,
  PageSettingsModel,
} from '@syncfusion/ej2-angular-grids';
import { Employees } from '../write/hw-order-field.write.component';
import { EmployeeService } from '@service/employee.service';

@Component({
  selector: 'app-select-employees',
  templateUrl: './select-employees.component.html',
  styleUrls: ['./select-employees.component.css'],
})
export class SelectEmployeesComponent extends UofxDialog implements OnInit {
  constructor(
    private dialogCtrl: UofxDialogController,
    private cs: EmployeeService
  ) {
    super();
  }
  ngOnInit(): void {
    this.cs.serverUrl = this.params.apiurl;
    this.cs.getemployees().subscribe((res) => {
      this.searchResult = [...res];
      this.pageModel.totalCount = this.searchResult.length;
    });
  }

  /*any 為grid item的inerface物件*/
  searchResult: Array<any> = [];
  /*searchResult Bind完後記得要再重設資料筆數*/
  /*this.pageModel.totalCount = this.searchResult.length; */
  pageModel = { currentPage: 1, pageCount: 5, pageSize: 5, totalCount: 30 };
  initialPage = <PageSettingsModel>{
    currentPage: 1,
    pageCount: 2,
    pageSize: 5,
    totalRecordsCount: 20
  };

  /*分頁器事件*/
  onPagerClick(event) {
    if (event.isTrusted) return;
    if (this.pageModel.currentPage === event.currentPage) return;

    this.pageModel.currentPage = event.currentPage;

    this.initialPage = <PageSettingsModel>{
      currentPage: event.currentPage,
      pageCount: 2,
      pageSize: 5,
      totalRecordsCount: 20,
    };
  }

  selectItem(item:Employees)
  {
    console.log(item);
    this.close(item);
  }

  DoClose() {
    this.close();
  }

}
