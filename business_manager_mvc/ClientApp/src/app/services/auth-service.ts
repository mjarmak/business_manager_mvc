import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { TokenEnvelope } from '../../model/tokenEnvelope';
import { Observable } from 'rxjs';
import { ResponseEnvelope } from '../../Model/responseEnvelope';
import { UserAccountModel } from '../../Model/user';
import { UserInfo } from '../../Model/userInfo';
import { AlertService } from './alert-service';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class AuthService {

    private url: string;

    constructor(private http: HttpClient, private alertService: AlertService) {
        this.url = environment.authentication_api_url;
    }

    public getToken(): string {
        return localStorage.getItem('token');
    }
    public clearUserInfo(): void {
        localStorage.removeItem('token');
        localStorage.removeItem('username');
        localStorage.removeItem('useremail');
        localStorage.setItem('userrole', "NONE");
    }

    public getUsername(): string {
        return localStorage.getItem('username');
    }

    public setUserinfo(userinfo: UserInfo) {
        console.log(userinfo);
        if (userinfo.name) {
            localStorage.setItem('username', userinfo.name);
        }
        if (userinfo.email) {
            localStorage.setItem('useremail', userinfo.email);
        }
        if (userinfo.role) {
            localStorage.setItem('userrole', userinfo.role);
        }
    }

    public setToken(token: string) {
        localStorage.setItem('token', token);
    }

    public Connect(username: string, password: string): Observable<TokenEnvelope> {
        const formData = new FormData();
        formData.append("client_id", username);
        formData.append("client_secret", password);
        formData.append("scope", "bm");
        formData.append("grant_type", "client_credentials");
        return this.http.post<TokenEnvelope>(this.url + '/connect/token', formData);
    }
    public Login(username: string, password: string): Observable<TokenEnvelope> {
        const formData = new FormData();
        formData.append("client_id", environment.client_id);
        formData.append("client_secret", environment.client_secret);
        formData.append("username", username);
        formData.append("password", password);
        formData.append("scope", "bm");
        formData.append("grant_type", "password");

        return this.http.post<TokenEnvelope>(this.url + '/connect/token', formData);
    }

    public Register(user: UserAccountModel): Observable<ResponseEnvelope> {
        console.log('CALL TO ' + this.url + '/register')
        return this.http.post<ResponseEnvelope>(this.url + '/register', user)
    }

    public GetToken(username: string, password: string, scope: string, grantType: string): Observable<TokenEnvelope> {
        const formData = new FormData();
        formData.append("client_id", environment.client_id);
        formData.append("client_secret", environment.client_secret);
        formData.append("username", username);
        formData.append("password", password);
        formData.append("scope", scope);
        formData.append("grant_type", grantType);
        return this.http.post<TokenEnvelope>(this.url + '/connect/token', formData);
    }
    public GetUserInfo(token: string): Observable<UserInfo> {
        let headers = { "Authorization": "Bearer " + token }
        return this.http.get<UserInfo>(this.url + '/connect/userinfo', { headers });
    }

    public isAuthenticated(): boolean {
        return this.getToken() != undefined;
    }
}
