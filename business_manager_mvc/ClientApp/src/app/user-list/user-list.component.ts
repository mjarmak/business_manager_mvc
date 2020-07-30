import { Component, Inject, OnInit } from '@angular/core';
import { UserAccountModel } from '../../Model/user';
import { BusinessManagerService } from '../services/business-manager-svc';
import { AlertService } from '../services/alert-service';

@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html'
})
export class UserListComponent implements OnInit {

    public user: UserAccountModel;
    public passwordDuplicate: string;

    ngOnInit(): void {
        this.user = new UserAccountModel;
        this.businessManagerService.refreshUserGenders();
    }

    constructor(private businessManagerService: BusinessManagerService, private alertSerice: AlertService) {
    }

    public onClickSave() {
        console.log('user is ' + this.user.email)
        this.businessManagerService.saveUser(this.user).subscribe(result => {
            this.user = result.data;
        }, error => {
                this.alertSerice.error("Error saving user", error.message);
        });
    }
    public setUserGender(gender: string) {
        this.user.gender = gender;
    }
}
