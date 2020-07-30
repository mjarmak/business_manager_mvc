import { Component, Inject, OnInit } from "@angular/core";
import { UserAccountModel } from "../../Model/user";
import { UserManagerService } from "../services/auth-service";
import { AlertService } from "../services/alert-service";

@Component({
    selector: "app-user-list",
    templateUrl: "./user-list.component.html"
})
export class UserListComponent implements OnInit {

    user: UserAccountModel;
    passwordDuplicate: string;

    ngOnInit(): void {
        this.user = new UserAccountModel;
        this.userManagerService.refreshUserGenders();
    }

    constructor(private userManagerService: UserManagerService, private readonly alertService: AlertService) {
    }

    onClickSave() {
        console.log(`user is ${this.user.email}`);
        this.userManagerService.saveUser(this.user).subscribe(result => {
            this.user = result.data;
        }, error => {
                this.alertService.error("Error saving user", error.message);
        });
    }
    setUserGender(gender: string) {
        this.user.gender = gender;
    }
}
