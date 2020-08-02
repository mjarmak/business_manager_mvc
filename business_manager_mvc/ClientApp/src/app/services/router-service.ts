import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class RouterService {

    public static OpenHomePage() {
        window.open("/", "_self");
    }
    public static OpenBusiness(businessId: number) {
        window.open("/business-detail/" + businessId, "_self")
    }

    public static openLoginPage() {
        window.open("/login", "_self");
    }
}
