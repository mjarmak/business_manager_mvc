import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { UserAccountModel } from '../../Model/user';
import { BusinessManagerService } from '../services/business-manager-svc';

@Component({
    selector: 'app-user-account-create',
    templateUrl: './user-account-create.component.html'
})
export class UserAccountCreateComponent {

    public user: UserAccountModel;

    constructor(private businessManagerService: BusinessManagerService) {
        this.user = new UserAccountModel;
    }
    public onClickSave() {
        console.log('user is ' + this.user)
        this.businessManagerService.saveUser(this.user).subscribe(result => {
            this.user = result;
        });
    }
}
