"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var BusinessDataModel = /** @class */ (function () {
    function BusinessDataModel() {
        this.identification = new IdentificationData();
        this.businessInfo = new BusinessInfo();
    }
    return BusinessDataModel;
}());
exports.BusinessDataModel = BusinessDataModel;
var BusinessInfo = /** @class */ (function () {
    function BusinessInfo() {
        this.address = new AddressData();
        //photos: File[];
    }
    return BusinessInfo;
}());
exports.BusinessInfo = BusinessInfo;
var IdentificationData = /** @class */ (function () {
    function IdentificationData() {
    }
    return IdentificationData;
}());
exports.IdentificationData = IdentificationData;
var AddressData = /** @class */ (function () {
    function AddressData() {
    }
    return AddressData;
}());
exports.AddressData = AddressData;
//# sourceMappingURL=business.js.map