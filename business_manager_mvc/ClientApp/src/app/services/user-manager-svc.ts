import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { AlertService } from './alert-service';
import { Injectable } from '@angular/core';
import { ResponseEnvelope } from '../../Model/responseEnvelope';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class UserManagerService {

    private url: string;
    public userRoles: string[];

    constructor(private http: HttpClient, private alertService: AlertService) {
        this.url = environment.authentication_api_url;
    }

    public getUserRoles(): Observable<ResponseEnvelope> {
        console.log('CALL TO ' + this.url + '/roles')
        return this.http.get<ResponseEnvelope>(this.url + '/roles');
    }

    public searchUsers(role?: string): Observable<ResponseEnvelope> {
        let path = '/user?'
        if (role) {
            path = path + "&role=" + role
        }

        console.log('CALL TO ' + this.url + path)
        return this.http.get<ResponseEnvelope>(this.url + path)
    }

    public refreshUserRoles(): void {
        if (this.userRoles === undefined) {
            this.userRoles = []
            this.getUserRoles().subscribe(result => {
                this.userRoles = result.data;
            }, error => {
                    this.userRoles = undefined;
                this.alertService.error("Error loading bussiness types", error.message);
            });
        }
    }

    changeUserRole(username: string, action: string) {
        console.log('CALL TO ' + this.url + '/user/' + action)
        return this.http.get<ResponseEnvelope>(this.url + "/user/" + action + "?username=" + username);
    }
}
