import { Component, OnInit } from "@angular/core";
import { environment } from "../../environments/environment";
import { BusinessDataModel } from "../../Model/business";
import { BusinessManagerService } from "../services/business-manager-svc";
import { AlertService } from "../services/alert-service";
import { ActivatedRoute } from "@angular/router";


@Component({
    selector: "app-business-detail",
  templateUrl: "./business-detail.component.html"
})
export class BusinessDetailComponent implements OnInit {

  business: BusinessDataModel;
  imagesUrl: string;

  constructor(private readonly businessManagerService: BusinessManagerService, private readonly alertService: AlertService, private readonly route: ActivatedRoute) {
      this.imagesUrl = environment.business_manager_api_url + "/images/";
      this.business = new BusinessDataModel();

    const businessId = this.route.snapshot.paramMap.get("businessId");

    if (businessId) {
      this.businessManagerService.getBusiness(Number(businessId)).subscribe(result => {
        //console.log(result.data);
        this.business = result.data;
      }, error => {
        this.alertService.error("Error loading business", error.message);
      });
    }
    }

    ngOnInit(): void {
        this.businessManagerService.refreshBusinessTypes();
    }

    onClickSave() {
    }
}
