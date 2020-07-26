import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { TokenEnvelope } from '../../model/tokenEnvelope';
import { Observable } from 'rxjs';

export class AuthService {

    private url: string;

    constructor(private http: HttpClient) {
        this.url = environment.authentication_api_url;
    }

    public getToken(): string {
        return localStorage.getItem('token');
    }

    public getUsername(): string {
        return localStorage.getItem('username');
    }

    public setToken(token: string, username: string) {
        localStorage.setItem('token', token);
        localStorage.setItem('username', username);
    }

    public Connect(username: string, password: string): Observable<TokenEnvelope> {
        const formData = new FormData();
        formData.append("client_id", username);
        formData.append("client_secret", password);
        formData.append("scope", "business_manager_api");
        formData.append("grant_type", "client_credentials");
        return this.http.post<TokenEnvelope>(this.url + '/connect/token', formData);
    }

    public isAuthenticated(): boolean {
        // get the token
        const token = this.getToken();
        // return a boolean reflecting 
        // whether or not the token is expired
        return true;
    }
}
