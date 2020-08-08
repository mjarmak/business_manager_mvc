import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class RouterService {

    public static openHomePage() {
        window.open("/", "_self");
  }

    public static openBusinessOverview() {
      window.open("/business-overview", "_self");
    }

    public static openBusiness(businessId: number) {
        window.open("/business-detail/" + businessId, "_self")
  }
    public static openBusinessCreate(businessId: number) {
      window.open("/business-create/" + businessId, "_self")
    }

    public static openLoginPage() {
        window.open("/login", "_self");
    }
}
