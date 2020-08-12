import { Component, Inject, OnInit } from '@angular/core';
import { AlertService } from '../services/alert-service';
import { AuthService } from '../services/auth-service';
import { BusinessManagerService } from '../services/business-manager-svc';
import { RouterService } from '../services/router-service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

    public username: string;
    public password: string;

    ngOnInit(): void {
    }

    constructor(private alertService: AlertService, private authService: AuthService, private businessManagerService: BusinessManagerService) {
    }

    public onClickLogin() {
        this.authService.Login(this.username, this.password).subscribe(
            result => {
                if (result.access_token) {
                    this.authService.setToken(result.access_token);

                    this.RefreshUserInfo(this.username, this.password);
                }
            },
            error => {
                this.alertService.warning("Login Failed", "Please enter the correct username and password");
            }
        );
    }

    public OpenRegisterPage() {
        window.open("/register", "_self");
    }


    public RefreshUserInfo(username: string, password: string) {
        this.authService.GetToken(username, password, "openid", "password").subscribe(result => {

            let headers = { "Authorization": "Bearer " + result.access_token }
            this.authService.GetUserInfo(result.access_token)
                .subscribe(userInfo => {
                    if (userInfo.role === "BLOCKED") {
                        this.alertService.warning("Access Denied", "This user is blocked");
                        this.authService.clearUserInfo();
                        this.username = "";
                        this.password = "";
                    } else {
                        this.authService.setUserinfo(userInfo);
                        RouterService.openHomePage();
                    }
                }, error => {
                        this.authService.clearUserInfo();
                        this.alertService.error("Error fetching user info", error.message);
                });

        }, error => {
                this.authService.clearUserInfo();
            this.alertService.error("Error fetching user token", error.message);
        });
    }
}
