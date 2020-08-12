import { Component, Inject, OnInit, Input, ViewChild } from "@angular/core";
import { AlertService } from "../services/alert-service";
import { UserManagerService } from "../services/user-manager-svc";
import { MatTableDataSource, MatPaginator, MatSort } from "@angular/material";

@Component({
    selector: "app-user-list",
    templateUrl: "./user-list.component.html"
})
export class UserListComponent implements OnInit {

    displayedColumns: string[] = ['userName', 'more'];
    dataSource = new MatTableDataSource<any>();
    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;

    @Input() role: string;
    @Input() gender: string;

    constructor(private userManagerService: UserManagerService, private readonly alertService: AlertService) {
    }

    ngOnInit() {
        this.refresh();
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    refresh() {
        this.userManagerService.searchUsers(this.role).subscribe(result => {
            this.dataSource.data = result.data;
        }, error => {
            this.alertService.error("Error loading users", error.message);
        });
    }

    changeUserRole(username:string, action: string) {
        return this.userManagerService.changeUserRole(username, action).subscribe(result => {
            this.refresh();

            switch (action) {
                case "make-admin":
                    this.alertService.success("User updated" + action, "User is now an admin");
                    break;
                case "validate":
                    this.alertService.success("User updated" + action, "User is now validated");
                    break;
                case "block":
                    this.alertService.success("User updated" + action, "User is now blocked");
                    break;
            }

        }, error => {
            this.alertService.error("Error updating user: " + action, error.error.data);
        });
    }

}
