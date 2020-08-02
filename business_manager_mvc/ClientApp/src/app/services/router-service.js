"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var RouterService = /** @class */ (function () {
    function RouterService() {
    }
    RouterService.OpenHomePage = function () {
        window.open("/", "_self");
    };
    RouterService.OpenBusiness = function (businessId) {
        window.open("/business-detail/" + businessId, "_self");
    };
    RouterService.openLoginPage = function () {
        window.open("/login", "_self");
    };
    RouterService = __decorate([
        core_1.Injectable({
            providedIn: 'root',
        })
    ], RouterService);
    return RouterService;
}());
exports.RouterService = RouterService;
//# sourceMappingURL=router-service.js.map