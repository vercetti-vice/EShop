// ====================================================


// ====================================================

import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { ConfigurationService } from '../../services/configuration.service';
import {BootstrapTabDirective} from '../../directives/bootstrap-tab.directive';
import {ActivatedRoute} from '@angular/router';
import {Permission} from '../../models/permission.model';
import {AccountService} from '../../services/account.service';


@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    animations: [fadeInOut]
})
export class HomeComponent implements OnInit, OnDestroy {

  isBrandActivated = true;
  isCategoryActivated = false;
  isProductActivated = false;

  fragmentSubscription: any;

  readonly brandTab = 'brands';
  readonly categoryTab = 'categories';
  readonly productTab = 'products';

  @ViewChild('tab')
  tab: BootstrapTabDirective;

    constructor(public configurations: ConfigurationService, private route: ActivatedRoute, private accountService: AccountService) {
    }

  ngOnInit() {
    this.fragmentSubscription = this.route.fragment.subscribe(anchor => this.showContent(anchor));
  }


  ngOnDestroy() {
    this.fragmentSubscription.unsubscribe();
  }

  showContent(anchor: string) {
    if ((this.isFragmentEquals(anchor, this.categoryTab) && !this.canViewUsers) ||
      (this.isFragmentEquals(anchor, this.productTab) && !this.canViewRoles)) {
      return;
    }

    this.tab.show(`#${anchor || this.brandTab}Tab`);
  }

  isFragmentEquals(fragment1: string, fragment2: string) {

    if (fragment1 == null) {
      fragment1 = '';
    }

    if (fragment2 == null) {
      fragment2 = '';
    }

    return fragment1.toLowerCase() == fragment2.toLowerCase();
  }

  onShowTab(event) {
    const activeTab = event.target.hash.split('#', 2).pop();

    this.isBrandActivated = activeTab == this.brandTab;
    this.isCategoryActivated = activeTab == this.categoryTab;
    this.isProductActivated = activeTab == this.productTab;
  }


  get canViewUsers() {
    return this.accountService.userHasPermission(Permission.viewProductsPermission);
  }

  get canViewRoles() {
    return this.accountService.userHasPermission(Permission.viewProductsPermission);
  }
}
