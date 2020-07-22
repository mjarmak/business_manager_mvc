"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../environments/environment");
var AuthService = /** @class */ (function () {
    function AuthService(http) {
        this.http = http;
        this.url = environment_1.environment.authentication_api_url;
    }
    AuthService.prototype.getToken = function () {
        return localStorage.getItem('token');
    };
    AuthService.prototype.getUsername = function () {
        return localStorage.getItem('username');
    };
    AuthService.prototype.setToken = function (token, username) {
        localStorage.setItem('token', token);
        localStorage.setItem('username', username);
    };
    AuthService.prototype.Connect = function (username, password) {
        var formData = new FormData();
        formData.append("client_id", username);
        formData.append("client_secret", password);
        formData.append("scope", "business_manager_api");
        formData.append("grant_type", "client_credentials");
        return this.http.post(this.url + '/connect/token', formData);
    };
    AuthService.prototype.isAuthenticated = function () {
        // get the token
        var token = this.getToken();
        // return a boolean reflecting 
        // whether or not the token is expired
        return true;
    };
    return AuthService;
}());
exports.AuthService = AuthService;
//# sourceMappingURL=auth-service.js.map