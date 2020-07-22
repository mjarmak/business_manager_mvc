import { Component, Inject, OnInit } from '@angular/core';
import { AlertService } from '../services/alert-service';
import { AuthService } from '../services/auth-service';
import { BusinessManagerService } from '../services/business-manager-svc';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

    public username: string;
    public password: string;

    ngOnInit(): void {
    }

    constructor(private alertSerice: AlertService, private authService: AuthService, private businessManagerService: BusinessManagerService) {
    }

    public onClickLogin() {
        this.authService.Connect(this.username, this.password).subscribe(
            result => {
                console.log(result.access_token);
                if (result.access_token) {
                    this.authService.setToken(result.access_token, this.username);
                    this.businessManagerService.openHomePage();
                }
            },
            error => {
                this.alertSerice.warning("Login Failed", "Please enter the correct username and password");
            }
        );
    }
}
