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
var BusinessManagerService = /** @class */ (function () {
    function BusinessManagerService(http, alertService) {
        this.http = http;
        this.alertService = alertService;
        this.url = environment_1.environment.business_manager_api_url;
    }
    BusinessManagerService.prototype.getGeocoding = function (address) {
        return this.http.get("https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyArzIrvHH6ei9-x5FLY66LwBn0uhv8LWBs");
    };
    BusinessManagerService.prototype.saveBusiness = function (businessModel) {
        return this.http.post(this.url + '/business', businessModel);
    };
    BusinessManagerService.prototype.updateBusiness = function (businessModel, id) {
        return this.http.put(this.url + '/business/' + id, businessModel);
    };
    BusinessManagerService.prototype.searchBusinesses = function (country, city, openNow, type, onlyDisabled) {
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
        if (onlyDisabled) {
            path = path + "&onlyDisabled=" + onlyDisabled;
        }
        return this.http.get(this.url + path);
    };
    BusinessManagerService.prototype.getBusiness = function (businessId) {
        return this.http.get(this.url + '/business/' + businessId);
    };
    BusinessManagerService.prototype.getBusinessTypes = function () {
        return this.http.get(this.url + '/business/types');
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
    BusinessManagerService.prototype.getDays = function () {
        return this.http.get(this.url + '/business/days');
    };
    BusinessManagerService.prototype.enableBusiness = function (id) {
        return this.http.get(this.url + '/business/' + id + "/enable");
    };
    BusinessManagerService.prototype.disableBusiness = function (id) {
        return this.http.get(this.url + '/business/' + id + "/disable");
    };
    BusinessManagerService.prototype.deleteBusiness = function (id) {
        return this.http.delete(this.url + '/business/' + id);
    };
    BusinessManagerService.prototype.refreshDays = function () {
        var _this = this;
        if (this.days === undefined) {
            this.days = [];
            this.getDays().subscribe(function (result) {
                _this.days = result.data;
            }, function (error) {
                _this.days = undefined;
                _this.alertService.error("Error loading days", error.message);
            });
        }
    };
    BusinessManagerService.prototype.uploadLogo = function (logo, businessId) {
        if (logo === undefined) {
            return null;
        }
        var formData = new FormData();
        formData.append('image', logo);
        return this.http.post(this.url + '/business/' + businessId + '/logo', formData);
    };
    BusinessManagerService.prototype.uploadImage = function (image, businessId, imageId) {
        if (image === undefined) {
            return null;
        }
        var formData = new FormData();
        formData.append('image', image);
        return this.http.post(this.url + '/business/' + businessId + '/photo/' + imageId, formData);
    };
    BusinessManagerService.prototype.updateLogo = function (logo, businessId) {
        if (logo === undefined) {
            return null;
        }
        var formData = new FormData();
        formData.append('image', logo);
        return this.http.put(this.url + '/business/' + businessId + '/logo', formData);
    };
    BusinessManagerService.prototype.updateImage = function (image, businessId, imageId) {
        if (image === undefined) {
            return null;
        }
        var formData = new FormData();
        formData.append('image', image);
        return this.http.put(this.url + '/business/' + businessId + '/photo/' + imageId, formData);
    };
    BusinessManagerService.prototype.getBusinessImages = function (businessId) {
        return this.http.get(this.url + '/business/' + businessId + '/photo');
    };
    BusinessManagerService.prototype.getBusinessLogo = function (businessId) {
        return this.http.get(this.url + '/business/' + businessId + '/logo');
    };
    BusinessManagerService = __decorate([
        core_1.Injectable({
            providedIn: 'root',
        })
    ], BusinessManagerService);
    return BusinessManagerService;
}());
exports.BusinessManagerService = BusinessManagerService;
//# sourceMappingURL=business-manager-svc.js.map