import { Component, OnInit, ViewChild, OnDestroy } from "@angular/core";
import { AlertService } from "../services/alert-service";
import { UserListComponent } from "../user-list/user-list.component";
import { UserManagerService } from "../services/user-manager-svc";
//import { UserListComponent } from "../user-list/user-list.component";

@Component({
    selector: 'app-user-overview',
  templateUrl: './user-overview.component.html'
})
export class UserOverviewComponent implements OnInit {

    @ViewChild(UserListComponent, { static: false }) userListComponent: UserListComponent;

    role: string;
    gender: string;
    name: string;

    constructor(private userManagerService: UserManagerService) {
    }

    ngOnInit() {
        this.userManagerService.refreshUserRoles();
    }

    setRole(role: string) {
        this.role = role;
    }
}
