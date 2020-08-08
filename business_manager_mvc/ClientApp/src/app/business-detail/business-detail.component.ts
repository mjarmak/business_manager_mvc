import { Component, OnInit } from "@angular/core";
import { environment } from "../../environments/environment";
import { BusinessDataModel } from "../../Model/business";
import { BusinessManagerService } from "../services/business-manager-svc";
import { AlertService } from "../services/alert-service";
import { ActivatedRoute } from "@angular/router";
import { RouterService } from "../services/router-service";


@Component({
  selector: "app-business-detail",
  templateUrl: "./business-detail.component.html"
})
export class BusinessDetailComponent implements OnInit {

  business: BusinessDataModel;
  role: string;
  email: string;
  isOwner: boolean = false;
  imagesUrl: string;

  constructor(private readonly businessManagerService: BusinessManagerService, private readonly alertService: AlertService, private readonly route: ActivatedRoute) {
    this.imagesUrl = environment.business_manager_api_url + "/images/";
    this.business = new BusinessDataModel();

    const businessId = this.route.snapshot.paramMap.get("businessId");

    if (businessId) {
      this.businessManagerService.getBusiness(Number(businessId)).subscribe(result => {
        this.business = result.data;

        if (this.business) {
          this.role = localStorage.getItem("userrole");
          this.email = localStorage.getItem("useremail");

          if (this.business.identification && this.email && this.email === this.business.identification.emailPro && this.role && !this.role.includes("BLOCKED")) {
            this.isOwner = true;
          }
        }

      }, error => {
        this.alertService.error("Error loading business", error.message);
      });
    }
  }

  ngOnInit(): void {
    this.businessManagerService.refreshBusinessTypes();
  }

  public editBusiness() {
    if (this.business && this.business.id) {
      RouterService.openBusinessCreate(this.business.id);
    } else {
      this.alertService.error("Business doesn't exist", "Can't edit");
    }
  }
  public enableBusiness() {
    if (this.business && this.business.id) {
      return this.businessManagerService.enableBusiness(this.business.id).subscribe(result => {
        this.alertService.success("Enabled business " + this.business.id, result.data);
      }, error => {
        this.alertService.error("Error updating business " + this.business.id, error.error.data);
      });
    } else {
      this.alertService.error("Business doesn't exist", "Can't edit");
    }
  }
  public disableBusiness() {
    if (this.business && this.business.id) {
      return this.businessManagerService.disableBusiness(this.business.id).subscribe(result => {
        this.alertService.success("Disabled business " + this.business.id, result.data);
        RouterService.openBusinessOverview();
      }, error => {
        this.alertService.error("Error updating business " + this.business.id, error.error.data);
      });
    } else {
      this.alertService.error("Business doesn't exist", "Can't edit");
    }
  }
  public deleteBusiness() {
    if (this.business && this.business.id) {
      return this.businessManagerService.deleteBusiness(this.business.id).subscribe(result => {
        RouterService.openHomePage();
      }, error => {
        this.alertService.error("Error updating business " + this.business.id, error.error.data);
      });
    } else {
      this.alertService.error("Business doesn't exist", "Can't edit");
    }
  }
}
