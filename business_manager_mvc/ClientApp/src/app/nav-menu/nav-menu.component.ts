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
    role: string;

    constructor(private authService: AuthService) {
        this.role = localStorage.getItem("userrole");
    }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
    }

    Logout() {
        this.authService.clearUserInfo();
        this.OpenLoginPage();
    }

    public OpenLoginPage() {
        RouterService.openLoginPage();
    }

}
