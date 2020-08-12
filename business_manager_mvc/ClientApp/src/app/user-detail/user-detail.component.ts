import { Component, Inject, OnInit } from '@angular/core';
import { UserAccountModel } from '../../Model/user';
import { AlertService } from '../services/alert-service';
import { AuthService } from '../services/auth-service';
import { RouterService } from '../services/router-service';
import { UserInfo } from '../../Model/userInfo';
import { UserUpdateModel } from '../../Model/user-update';

@Component({
    selector: 'app-user-detail',
    templateUrl: './user-detail.component.html'
})
export class UserAccountDetailComponent implements OnInit {

    public user: UserAccountModel;
    public userUpdate: UserUpdateModel;
    public passwordDuplicate: string;
    public userGenders = ["MALE", "FEMALE", "OTHER"];
    public editMode: boolean = false;
    public role: string;

    public errors = []

    ngOnInit(): void {
        this.role = localStorage.getItem("userrole");

        this.user = new UserAccountModel;
        this.user.name = localStorage.getItem("username");
        this.user.surname = localStorage.getItem("userfamily_name");
        this.user.email = localStorage.getItem("useremail");
        this.user.gender = localStorage.getItem("usergender");
        this.user.phone = localStorage.getItem("userphone_number");
        this.user.professional = Boolean(Number(localStorage.getItem("userprofessional")));
        this.user.birthday = localStorage.getItem("userbirthdate");
    }

    constructor(private authService: AuthService, private alertSerice: AlertService) {
    }

    public onClickSave() {
        this.authService.Update(this.user.email, this.userUpdate).subscribe(result => {
            this.errors = [];
            localStorage.setItem("username", this.userUpdate.nameNew);
            localStorage.setItem("userfamily_name", this.userUpdate.surnameNew);
            localStorage.setItem("userphone_number", this.userUpdate.phoneNew);
            localStorage.setItem("userprofessional", this.userUpdate.professionalNew + "");

            this.user.name = localStorage.getItem("username");
            this.user.surname = localStorage.getItem("userfamily_name");
            this.user.phone = localStorage.getItem("userphone_number");
            this.user.professional = Boolean(Number(localStorage.getItem("userprofessional")));

            this.editMode = false;

            this.alertSerice.success("User Updated", result.data);
        }, error => {
                this.errors = error.error.data;
                //this.alertSerice.error("Error registering user", error.message);
        });
    }
    public onClickEdit() {
        this.userUpdate = new UserUpdateModel;

        this.userUpdate.namePrevious = this.user.name;
        this.userUpdate.surnamePrevious = this.user.surname;
        this.userUpdate.phonePrevious = this.user.phone;
        this.userUpdate.professionalPrevious = this.user.professional;

        this.userUpdate.nameNew = this.user.name;
        this.userUpdate.surnameNew = this.user.surname;
        this.userUpdate.phoneNew = this.user.phone;
        this.userUpdate.professionalNew = this.user.professional;
        this.editMode = true;
    }
    public onClickCancel() {
        this.editMode = false;
    }
}
