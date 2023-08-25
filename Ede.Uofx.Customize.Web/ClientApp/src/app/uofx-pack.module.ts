import { NgModule } from '@angular/core';
import { UofxAvatarModule } from '@uofx/web-components/avatar';
import { UofxCardModule } from '@uofx/web-components/card';
import { UofxChipModule } from '@uofx/web-components/chip';
import { UofxDateRangeModule } from '@uofx/web-components/date-range';
import { UofxDialogModule } from '@uofx/web-components/dialog';
import { UofxDirectiveModule } from '@uofx/web-components';
import { UofxDropdownButtonModule } from '@uofx/web-components/dropdown-button';
import { UofxEmptyStatusModule } from '@uofx/web-components';
import { UofxFormFieldBaseModule, UofxFormModule } from '@uofx/web-components/form';
import { UofxIconModule } from '@uofx/web-components/icon';
import {
  UofxLoadingModule,
  UofxPipeModule,
  UofxRedirectModule
} from '@uofx/web-components';
import { UofxSearchBarModule } from '@uofx/web-components/search-bar';
import { UofxSelectModule } from '@uofx/web-components/select';
import { UofxSpinnerModule } from '@uofx/web-components';
import { UofxTagSelectModule } from '@uofx/web-components/tag-select';
import { UofxTextareaModule } from '@uofx/web-components/textarea';
import { UofxTextEllipsisModule } from '@uofx/web-components/text-ellipsis';
import { UofxTranslateModule } from '@uofx/web-components';
import { UofxToastModule } from '@uofx/web-components/toast';
import { UofxTooltipModule } from '@uofx/web-components/tooltip';
import { UofxTreeModule } from '@uofx/web-components/tree';
import { UofxUserSelectModule } from '@uofx/web-components/user-select';

@NgModule({
  exports: [
    UofxAvatarModule,
    UofxCardModule,
    UofxChipModule,
    UofxDateRangeModule,
    UofxDialogModule,
    UofxDirectiveModule,
    UofxDropdownButtonModule,
    UofxEmptyStatusModule,
    UofxFormFieldBaseModule,
    UofxFormModule,
    UofxIconModule,
    UofxLoadingModule,
    UofxPipeModule,
    UofxRedirectModule,
    UofxSearchBarModule,
    UofxSelectModule,
    UofxSpinnerModule,
    UofxTagSelectModule,
    UofxTextareaModule,
    UofxTextEllipsisModule,
    UofxToastModule,
    UofxTooltipModule,
    UofxTranslateModule,
    UofxTreeModule,
    UofxUserSelectModule
  ]
})
export class UofxPackageModule { }
