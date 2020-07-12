"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
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
    AlertService.prototype.waring = function (title, message) {
        this.toastr.warning(message, title);
    };
    return AlertService;
}());
exports.AlertService = AlertService;
//# sourceMappingURL=alert-service.js.map