import { Component, OnInit } from "@angular/core";
import { AuthService } from "../services/auth-service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
})
export class HomeComponent implements OnInit {

    name: string;

    ngOnInit(): void {
        this.name = localStorage.getItem("username");
    }

    constructor() {
    }

}
