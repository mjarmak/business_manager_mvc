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
var UserManagerService = /** @class */ (function () {
    function UserManagerService(http, alertService) {
        this.http = http;
        this.alertService = alertService;
        this.url = environment_1.environment.authentication_api_url;
    }
    UserManagerService.prototype.getUserRoles = function () {
        console.log('CALL TO ' + this.url + '/roles');
        return this.http.get(this.url + '/roles');
    };
    UserManagerService.prototype.searchUsers = function (role) {
        var path = '/user?';
        if (role) {
            path = path + "&role=" + role;
        }
        console.log('CALL TO ' + this.url + path);
        return this.http.get(this.url + path);
    };
    UserManagerService.prototype.refreshUserRoles = function () {
        var _this = this;
        if (this.userRoles === undefined) {
            this.userRoles = [];
            this.getUserRoles().subscribe(function (result) {
                _this.userRoles = result.data;
            }, function (error) {
                _this.userRoles = undefined;
                _this.alertService.error("Error loading bussiness types", error.message);
            });
        }
    };
    UserManagerService.prototype.changeUserRole = function (username, action) {
        console.log('CALL TO ' + this.url + '/user/' + action);
        return this.http.get(this.url + "/user/" + action + "?username=" + username);
    };
    UserManagerService = __decorate([
        core_1.Injectable({
            providedIn: 'root',
        })
    ], UserManagerService);
    return UserManagerService;
}());
exports.UserManagerService = UserManagerService;
//# sourceMappingURL=user-manager-svc.js.map