"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var BusinessDataModel = /** @class */ (function () {
    function BusinessDataModel() {
        this.identification = new IdentificationData();
        this.businessInfo = new BusinessInfo();
        this.workHours = [];
    }
    return BusinessDataModel;
}());
exports.BusinessDataModel = BusinessDataModel;
var BusinessInfo = /** @class */ (function () {
    function BusinessInfo() {
        this.address = new AddressData();
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
var WorkHoursData = /** @class */ (function () {
    function WorkHoursData(day, hourFrom, hourTo, minuteTo, minuteFrom, closed) {
        this.day = day;
        this.hourFrom = hourFrom;
        this.hourTo = hourTo;
        this.minuteTo = minuteTo;
        this.minuteFrom = minuteFrom;
        this.closed = closed;
    }
    return WorkHoursData;
}());
exports.WorkHoursData = WorkHoursData;
//# sourceMappingURL=business.js.map