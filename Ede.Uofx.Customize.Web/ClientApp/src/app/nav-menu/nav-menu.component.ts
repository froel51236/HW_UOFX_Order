import { ClickEventArgs, MenuEventArgs, MenuItemModel, SidebarComponent } from '@syncfusion/ej2-angular-navigations';
import { Component, Inject, ViewChild } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }


  @ViewChild('sidebarMenuInstance')
  public sidebarMenuInstance: SidebarComponent;
  public menuItems: MenuItemModel[] = [
    {
      text: 'Template Plugin',
      iconCss: 'e-timeline-week e-icons',
      items: [
        { text: 'Design', url: 'template-field/design' },
        { text: 'Props', url:  'template-field/props' },
        { text: 'Write', url:  'template-field/write' },
        { separator: true },
        { text: 'App' },
      ]
    },
  ];
  public enableDock: boolean = true;
  public dockSize: string = '50px';
  public width: string = '220px';
  public target: string = '.main-menu-content';

  constructor() {

  }

  toolbarCliked(args: ClickEventArgs) {
    if (args.item.tooltipText == "Menu") {
      this.sidebarMenuInstance.toggle();
    }
  }

  onMenuItemSelect(ev: MenuEventArgs) {
    console.log(ev.item);
  }
}
