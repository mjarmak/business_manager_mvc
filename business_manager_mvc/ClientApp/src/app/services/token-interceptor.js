"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var http_1 = require("@angular/common/http");
var operators_1 = require("rxjs/operators");
var TokenInterceptor = /** @class */ (function () {
    function TokenInterceptor(auth, alertSerice, businessManagerService) {
        this.auth = auth;
        this.alertSerice = alertSerice;
        this.businessManagerService = businessManagerService;
    }
    TokenInterceptor.prototype.intercept = function (request, next) {
        var _this = this;
        request = request.clone({
            setHeaders: {
                Authorization: "Bearer " + this.auth.getToken()
            }
        });
        return next.handle(request).pipe(operators_1.tap(function (event) { return console.log(event instanceof http_1.HttpResponse ? 'dope' : 'not dope'); }, function (error) {
            if (error.status === 401) {
                _this.alertSerice.warning("Login expired", "Please login again");
                _this.businessManagerService.openLoginPage();
            }
            else if (error.status === 500) {
                _this.alertSerice.error(error.statusText, error.message);
            }
        }));
    };
    return TokenInterceptor;
}());
exports.TokenInterceptor = TokenInterceptor;
//# sourceMappingURL=token-interceptor.js.map