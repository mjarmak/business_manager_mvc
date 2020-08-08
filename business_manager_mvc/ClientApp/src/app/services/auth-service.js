"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../environments/environment");
var core_1 = require("@angular/core");
var AuthService = /** @class */ (function () {
    function AuthService(http, alertService) {
        this.http = http;
        this.alertService = alertService;
        this.url = environment_1.environment.authentication_api_url;
    }
    AuthService.prototype.getToken = function () {
        return localStorage.getItem('token');
    };
    AuthService.prototype.clearUserInfo = function () {
        localStorage.removeItem('token');
        localStorage.removeItem('username');
        localStorage.removeItem('useremail');
        localStorage.setItem('userrole', "NONE");
    };
    AuthService.prototype.getUsername = function () {
        return localStorage.getItem('username');
    };
    AuthService.prototype.setUserinfo = function (userinfo) {
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
    };
    AuthService.prototype.setToken = function (token) {
        localStorage.setItem('token', token);
    };
    AuthService.prototype.Connect = function (username, password) {
        var formData = new FormData();
        formData.append("client_id", username);
        formData.append("client_secret", password);
        formData.append("scope", "bm");
        formData.append("grant_type", "client_credentials");
        return this.http.post(this.url + '/connect/token', formData);
    };
    AuthService.prototype.Login = function (username, password) {
        var formData = new FormData();
        formData.append("client_id", environment_1.environment.client_id);
        formData.append("client_secret", environment_1.environment.client_secret);
        formData.append("username", username);
        formData.append("password", password);
        formData.append("scope", "bm");
        formData.append("grant_type", "password");
        return this.http.post(this.url + '/connect/token', formData);
    };
    AuthService.prototype.Register = function (user) {
        return this.http.post(this.url + '/register', user);
    };
    AuthService.prototype.GetToken = function (username, password, scope, grantType) {
        var formData = new FormData();
        formData.append("client_id", environment_1.environment.client_id);
        formData.append("client_secret", environment_1.environment.client_secret);
        formData.append("username", username);
        formData.append("password", password);
        formData.append("scope", scope);
        formData.append("grant_type", grantType);
        return this.http.post(this.url + '/connect/token', formData);
    };
    AuthService.prototype.GetUserInfo = function (token) {
        var headers = { "Authorization": "Bearer " + token };
        return this.http.get(this.url + '/connect/userinfo', { headers: headers });
    };
    AuthService.prototype.isAuthenticated = function () {
        return this.getToken() != undefined;
    };
    AuthService = __decorate([
        core_1.Injectable({
            providedIn: 'root',
        })
    ], AuthService);
    return AuthService;
}());
exports.AuthService = AuthService;
//# sourceMappingURL=auth-service.js.map