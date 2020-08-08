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
    public static openBusinessEdit(businessId: number) {
      window.open("/business-edit/" + businessId, "_self")
    }

    public static openLoginPage() {
        window.open("/login", "_self");
    }
    public static openUserPage() {
        window.open("/user-detail", "_self");
    }
}
