import { Component } from "@angular/core";
import { AuthService } from "../services/auth-service";
import { RouterService } from "../services/router-service";

@Component({
  selector: "app-nav-menu",
  templateUrl: "./nav-menu.component.html",
  styleUrls: ["./nav-menu.component.css"]
})
export class NavMenuComponent {

  isExpanded = false;

    constructor(private authService: AuthService) {
    }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
    }

    Logout() {
        this.authService.clearToken();
        this.authService.clearUserInfo();
        this.authService.clearRole();
        this.OpenLoginPage();
    }

    public OpenLoginPage() {
        RouterService.openLoginPage();
    }

}
