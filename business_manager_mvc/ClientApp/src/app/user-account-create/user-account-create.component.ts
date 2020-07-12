import { Component, Inject, OnInit } from '@angular/core';
import { UserAccountModel } from '../../Model/user';
import { BusinessManagerService } from '../services/business-manager-svc';

@Component({
    selector: 'app-user-account-create',
    templateUrl: './user-account-create.component.html'
})
export class UserAccountCreateComponent implements OnInit {

    public user: UserAccountModel;

    ngOnInit(): void {
        this.user = new UserAccountModel;
    }

    constructor(private businessManagerService: BusinessManagerService) {
    }
    public onClickSave() {
        console.log('user is ' + this.user.email)
        this.businessManagerService.saveUser(this.user).subscribe(result => {
            this.user = result;
        });
    }
}
