import { Component, Inject, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { AlertService } from '../services/alert-service';
import { BusinessManagerService } from '../services/business-manager-svc';
import { UserListComponent } from "../user-list/user-list.component";
import { BusinessListComponent } from "../business-list/business-list.component";

@Component({
    selector: 'app-user-overview',
  templateUrl: './user-overview.component.html'
})
export class UserOverviewComponent implements OnInit, OnDestroy {

    @ViewChild(UserListComponent, { static: false }) userListComponent: BusinessListComponent;
    type: string;
    city: string;
    country: string;
    openNow: string;

    interval: any;

  //constructor(private authService: AuthService) {
  //  }

    ngOnInit() {
    }

    ngOnDestroy(): void {
        clearInterval(this.interval);
    }

    public setBusinessType(type: string) {
        this.type = type;
    }
}
