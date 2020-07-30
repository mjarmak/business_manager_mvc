import { Component, OnInit, ViewChild, OnDestroy } from "@angular/core";
import { AlertService } from "../services/alert-service";
import { UserListComponent } from "../user-list/user-list.component";
//import { UserListComponent } from "../user-list/user-list.component";

@Component({
    selector: 'app-user-overview',
  templateUrl: './user-overview.component.html'
})
export class UserOverviewComponent implements OnInit, OnDestroy {

    @ViewChild(UserListComponent, { static: false }) userListComponent: UserListComponent;
    type: string;
    name: string;

    interval: any;

  //constructor(private authService: AuthService) {
  //  }

    ngOnInit() {
    }

    ngOnDestroy(): void {
        clearInterval(this.interval);
    }

    public setUserType(type: string) {
        this.type = type;
    }
}
