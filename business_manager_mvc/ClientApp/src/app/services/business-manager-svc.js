"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../environments/environment");
var BusinessManagerService = /** @class */ (function () {
    function BusinessManagerService(http, alertService) {
        this.http = http;
        this.alertService = alertService;
        this.url = environment_1.environment.business_manager_api_url;
    }
    BusinessManagerService.prototype.openHomePage = function () {
        window.open("/", "_self");
    };
    BusinessManagerService.prototype.openLoginPage = function () {
        window.open("/login", "_self");
    };
    BusinessManagerService.prototype.openBusiness = function (businessId) {
        window.open("/business-detail/" + businessId, "_self");
    };
    BusinessManagerService.prototype.saveBusiness = function (businessModel) {
        console.log('CALL TO ' + this.url + '/business');
        return this.http.post(this.url + '/business', businessModel);
    };
    BusinessManagerService.prototype.searchBusinesses = function (country, city, openNow, type) {
        var path = '/business/search?';
        if (country) {
            path = path + "&country=" + country;
        }
        if (city) {
            path = path + "&city=" + city;
        }
        if (openNow) {
            path = path + "&openNow=" + openNow;
        }
        if (type) {
            path = path + "&type=" + type;
        }
        console.log('CALL TO ' + this.url + path);
        return this.http.get(this.url + path);
    };
    BusinessManagerService.prototype.getBusiness = function (businessId) {
        console.log('CALL TO ' + this.url + '/business/' + businessId);
        return this.http.get(this.url + '/business/' + businessId);
    };
    BusinessManagerService.prototype.getBusinessTypes = function () {
        console.log('CALL TO ' + this.url + '/business/types');
        return this.http.get(this.url + '/business/types');
    };
    BusinessManagerService.prototype.getUserGenders = function () {
        console.log('CALL TO ' + this.url + '/user/genders');
        return this.http.get(this.url + '/user/genders');
    };
    BusinessManagerService.prototype.refreshBusinessTypes = function () {
        var _this = this;
        if (this.businessTypes === undefined) {
            this.businessTypes = [];
            this.getBusinessTypes().subscribe(function (result) {
                _this.businessTypes = result.data;
            }, function (error) {
                _this.businessTypes = undefined;
                _this.alertService.error("Error loading bussiness types", error.message);
            });
        }
    };
    BusinessManagerService.prototype.refreshUserGenders = function () {
        var _this = this;
        if (this.userGenders === undefined) {
            this.userGenders = [];
            this.getUserGenders().subscribe(function (result) {
                _this.userGenders = result.data;
            }, function (error) {
                _this.userGenders = undefined;
                _this.alertService.error("Error loading bussiness types", error.message);
            });
        }
    };
    BusinessManagerService.prototype.saveUser = function (user) {
        console.log('CALL TO ' + this.url + '/user');
        return this.http.post(this.url + '/user', user);
    };
    BusinessManagerService.prototype.uploadLogo = function (logo, businessId) {
        if (logo === undefined) {
            return null;
        }
        var formData = new FormData();
        formData.append('image', logo);
        console.log('CALL TO ' + this.url + '/business/' + businessId + '/logo');
        return this.http.post(this.url + '/business/' + businessId + '/logo', formData);
    };
    BusinessManagerService.prototype.uploadImage = function (image, businessId, imageId) {
        if (image === undefined) {
            return null;
        }
        var formData = new FormData();
        formData.append('image', image);
        console.log('CALL TO ' + this.url + '/business/' + businessId + '/photo/' + imageId);
        return this.http.post(this.url + '/business/' + businessId + '/photo/' + imageId, formData);
    };
    BusinessManagerService.prototype.getBusinessImages = function (businessId) {
        console.log('CALL TO ' + this.url + '/business/' + businessId + '/photo');
        return this.http.get(this.url + '/business/' + businessId + '/photo');
    };
    BusinessManagerService.prototype.getBusinessLogo = function (businessId) {
        console.log('CALL TO ' + this.url + '/business/' + businessId + '/logo');
        return this.http.get(this.url + '/business/' + businessId + '/logo');
    };
    return BusinessManagerService;
}());
exports.BusinessManagerService = BusinessManagerService;
//# sourceMappingURL=business-manager-svc.js.map