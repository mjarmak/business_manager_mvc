import { Component, Inject, OnInit } from '@angular/core';
import { UserAccountModel } from '../../Model/user';
import { AlertService } from '../services/alert-service';
import { AuthService } from '../services/auth-service';
import { RouterService } from '../services/router-service';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html'
})
export class UserAccountCreateComponent implements OnInit {

    public user: UserAccountModel;
    public passwordDuplicate: string;
    public userGenders = ["MALE", "FEMALE", "OTHER"];

    public errors = []

    ngOnInit(): void {
        this.user = new UserAccountModel;
    }

    constructor(private authService: AuthService, private alertSerice: AlertService) {
    }

    public onClickSave() {
        this.authService.Register(this.user).subscribe(result => {
            this.user = result.data;
            this.errors = [];
            RouterService.openHomePage();
        }, error => {
                this.errors = error.error.data;
                //this.alertSerice.error("Error registering user", error.message);
        });
    }
    public setUserGender(gender: string) {
        this.user.gender = gender;
    }
}
