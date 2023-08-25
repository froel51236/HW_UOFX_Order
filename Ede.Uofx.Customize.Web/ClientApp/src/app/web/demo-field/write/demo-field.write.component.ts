/*
此為外掛欄位wtite mode的樣板，修改/置換的項事如下
修改import 擴充屬性(ExProps)的interface
修改selector和templateUrl路徑
修改classname
修改 @Input() exProps 的interface
*/

import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';
import { BpmFwWriteComponent, UofxFormTools } from '@uofx/web-components/form';
import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import {
  Grid,
  GridComponent,
  PageSettingsModel,
} from '@syncfusion/ej2-angular-grids';

import { DemoFieldExProps } from '../props/demo-field.props.component';
import { SelectCustomerComponent } from '../select-customer/select-customer.component';
import { UofxDialogController } from '@uofx/web-components/dialog';
import { CuseromerService } from '@service/cuseromer.service';

/*修改*/
/*↑↑↑↑修改import 各模式的Component↑↑↑↑*/

/*修改*/
/*置換selector和templateUrl*/
@Component({
  selector: 'uofx-demo-field-write-component',
  templateUrl: './demo-field.write.component.html',
})

/*修改*/
/*置換className*/
export class DemoFieldWriteComponent
  extends BpmFwWriteComponent
  implements OnInit
{

  /*修改*/
  /*置換className*/
  @Input() exProps: DemoFieldExProps;

  value: DemoFieldInfo;
  form: FormGroup;
  constructor(
    private cdr: ChangeDetectorRef,
    private fb: FormBuilder,
    private tools: UofxFormTools,
    private dialogCtrl: UofxDialogController,
    private cs: CuseromerService
  ) {
    super();
  }



  ngOnInit(): void {

    this.initForm();

    this.parentForm.statusChanges.subscribe((res) => {
      if (res === 'INVALID' && this.selfControl.dirty) {
        if (!this.form.dirty) {
          Object.keys(this.form.controls).forEach((key) => {
            this.tools.markFormControl(this.form.get(key));
          });
          this.form.markAsDirty();
        }
      }
    });

    this.form.valueChanges.subscribe((res) => {
      this.selfControl?.setValue(res);
      /*真正送出欄位值變更的函式*/
      this.valueChanges.emit(res);
    });
    this.cdr.detectChanges();
  }

  initForm() {
    this.form = this.fb.group({
      // message: this.value?.message || '',
      companyName: '',
      address: '',
      phone: ''
    });

    if (this.value) {
      this.form.setValue(this.value);
    }

    // if (this.selfControl) {
    //   // 在此便可設定自己的驗證器
    //   this.selfControl.setValidators(validateSelf(this.form));
    //   this.selfControl.updateValueAndValidity();
    // }
  }

  onOpen() {
    this.dialogCtrl.createFlexibleScreen({
      component: SelectCustomerComponent,
      params: {
        /*開窗要帶的參數*/
        apiurl:this.pluginSetting.entryHost
      }
    }).afterClose.subscribe({
      next: res => {
        /*關閉視窗後處理的訂閱事件*/
        if (res) {
          console.log(res);
          this.form.controls.companyName.setValue(res.companyName);
          this.form.controls.phone.setValue(res.phone);
          this.form.controls.address.setValue(res.address);

          this.valueChanges.emit(this.form.value);
        }
      }
    })
  }

  /*判斷如果是儲存不用作驗證*/
  checkBeforeSubmit(): Promise<boolean> {
    return new Promise((resolve) => {
      const value = this.form.value;
      resolve(true);
    });
  }
}


/*外掛欄位自訂的證器*/
function validateSelf(form: FormGroup): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    return form.valid ? null : { formInvalid: true };
  };
}

export interface DemoFieldInfo {
  companyName: string;
  address: string;
  phone: string;
}
