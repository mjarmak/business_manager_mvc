"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../environments/environment");
var BusinessManagerService = /** @class */ (function () {
    function BusinessManagerService(http) {
        this.http = http;
        this.url = environment_1.environment.business_manager_api_url;
    }
    BusinessManagerService.prototype.openHomePage = function () {
        console.log("open homepage");
        window.open("/", "_self");
    };
    BusinessManagerService.prototype.openLoginPage = function () {
        console.log("open homepage");
        window.open("/login", "_self");
    };
    BusinessManagerService.prototype.saveBusiness = function (businessModel) {
        console.log('CALL TO ' + this.url + '/business');
        return this.http.post(this.url + '/business', businessModel);
    };
    BusinessManagerService.prototype.searchBusinesses = function () {
        console.log('CALL TO ' + this.url + '/business');
        return this.http.get(this.url + '/business');
    };
    BusinessManagerService.prototype.getBusiness = function (businessId) {
        console.log('CALL TO ' + this.url + '/business/' + businessId);
        return this.http.get(this.url + '/business/' + businessId);
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