"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../environments/environment");
var BusinessManagerService = /** @class */ (function () {
    function BusinessManagerService(http) {
        this.http = http;
        this.url = environment_1.environment.business_manager_orc_url;
    }
    BusinessManagerService.prototype.saveBusiness = function (businessModel) {
        console.log('CALL TO ' + this.url + '/business');
        return this.http.post(this.url + '/business', businessModel);
    };
    BusinessManagerService.prototype.saveUser = function (user) {
        console.log('CALL TO ' + this.url + '/user');
        return this.http.post(this.url + '/user', user);
    };
    BusinessManagerService.prototype.uploadImage = function (image, businessId) {
        var formData = new FormData();
        formData.append('image', image);
        console.log('CALL TO ' + this.url + '/image/business/' + businessId);
        return this.http.post(this.url + '/image', formData);
    };
    return BusinessManagerService;
}());
exports.BusinessManagerService = BusinessManagerService;
//# sourceMappingURL=business-manager-svc.js.map