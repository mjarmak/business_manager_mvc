"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var AlertService = /** @class */ (function () {
    function AlertService(toastr) {
        this.toastr = toastr;
    }
    AlertService.prototype.success = function (title, message) {
        this.toastr.success(message, title);
    };
    AlertService.prototype.error = function (title, message) {
        this.toastr.error(message, title);
    };
    AlertService.prototype.info = function (title, message) {
        this.toastr.info(message, title);
    };
    AlertService.prototype.warning = function (title, message) {
        this.toastr.warning(message, title);
    };
    AlertService = __decorate([
        core_1.Injectable({
            providedIn: 'root',
        })
    ], AlertService);
    return AlertService;
}());
exports.AlertService = AlertService;
//# sourceMappingURL=alert-service.js.map