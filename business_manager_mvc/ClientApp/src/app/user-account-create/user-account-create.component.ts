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
    private url: string;

    constructor(private http: HttpClient) {
        this.url = environment.business_manager_orc_url + '/user';
        this.userAccount = new UserAccountModel;
    }
    public onClickSave() {
        console.log('CALL TO ' + this.url)
        console.log('user is ' + this.userAccount)
        this.http.post<UserAccountModel>(this.url, this.userAccount).subscribe(result => {
            this.userAccount = result;
        });
    }
}
