import {
  CUSTOM_ELEMENTS_SCHEMA,
  NO_ERRORS_SCHEMA,
  NgModule,
} from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import {
  MenuModule,
  SidebarModule,
  ToolbarModule,
} from '@syncfusion/ej2-angular-navigations';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';

import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { Helper } from '@uofx/core';
import { HomeComponent } from './home/home.component';
import { IconModule } from './icon.module';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { RouterModule } from '@angular/router';
import { TemplateFieldWriteComponent } from './web/template-field/write/template-field.write.component';
import { DemoFieldWriteComponent } from './web/demo-field/write/demo-field.write.component';
import { HwOrderFieldWriteComponent } from './web/hw-order-field/write/hw-order-field.write.component';
import { UofxTranslateLoader } from './translate-loader';


// #region i18n services
export function I18nHttpLoaderFactory(http: HttpClient) {
  return new UofxTranslateLoader(http);
}

const I18NSERVICE_MODULES = [
  TranslateModule.forRoot({
    loader: {
      provide: TranslateLoader,
      useFactory: I18nHttpLoaderFactory,
      deps: [HttpClient],
    },
    defaultLanguage: 'zh-TW',
    useDefaultLang: true,
  }),
];

//#endregion

const EJS_MODULES = [MenuModule, SidebarModule, ToolbarModule];

/*修改*/
/*新增RouterModule.forRoot的path，並在裡面import module的路徑和載入的mocule className*/
@NgModule({
  declarations: [AppComponent, NavMenuComponent, HomeComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: TemplateFieldWriteComponent, pathMatch: 'full' },
      {
        path: 'template-field',
        loadChildren: () => import('./web/template-field/template-field.module').then((m) => m.TemplateFieldModule)
      },
      { path: '', component: DemoFieldWriteComponent, pathMatch: 'full' },
      {
        path: 'demo-field',
        loadChildren: () => import('./web/demo-field/demo-field.module').then((m) => m.DemoFieldModule)
      },
      { path: '', component: HwOrderFieldWriteComponent, pathMatch: 'full' },
      {
        path: 'hw-order-field',
        loadChildren: () => import('./web/hw-order-field/hw-order-field.module').then((m) => m.HwOrderFieldModule)
      }
    ]),
    ...I18NSERVICE_MODULES,
    ...EJS_MODULES,
    IconModule.forRoot(),
  ],
  providers: [{ provide: 'BASE_HREF', useFactory: Helper.getBaseHref }],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA],
})
export class AppModule {}
