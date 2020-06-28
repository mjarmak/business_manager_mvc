import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { UserAccountModel } from '../../Model/user';

@Component({
    selector: 'app-user-account-create',
    templateUrl: './user-account-create.component.html'
})
export class UserAccountCreateComponent {

    public userAccount: UserAccountModel;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        let url = environment.business_manager_api_url + 'user-account';
        this.userAccount = new UserAccountModel;
        console.log('CALL TO ' + url)
        console.log('user is ' + this.userAccount)
    }

}
