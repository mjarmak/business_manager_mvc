import { Component, OnInit } from "@angular/core";
import { environment } from "../../environments/environment";
import { BusinessDataModel } from "../../Model/business";
import { BusinessManagerService } from "../services/business-manager-svc";
import { AlertService } from "../services/alert-service";
import { ActivatedRoute } from "@angular/router";
import { RouterService } from "../services/router-service";
import { ResponseEnvelope } from "../../Model/responseEnvelope";


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
    lat: string;
    long: string;

    constructor(
        private readonly businessManagerService: BusinessManagerService,
        private readonly alertService: AlertService,
        private readonly route: ActivatedRoute
    ) {
        this.imagesUrl = environment.business_manager_api_url + "/images/";
        this.business = new BusinessDataModel();

        const businessId = this.route.snapshot.paramMap.get("businessId");

        if (businessId) {
            this.businessManagerService.getBusiness(Number(businessId)).subscribe((result: ResponseEnvelope<BusinessDataModel>) => {
                this.business = result.data;
                if (this.business) {
                    this.refreshCoordinates();
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
            RouterService.openBusinessEdit(this.business.id);
        } else {
            this.alertService.error("Business doesn't exist", "Can't edit");
        }
    }
    public enableBusiness() {
        if (this.business && this.business.id) {
            return this.businessManagerService.enableBusiness(this.business.id).subscribe(result => {
                this.alertService.success("Enabled business " + this.business.id, result.data);
                this.businessManagerService.getBusiness(this.business.id).subscribe(result => {
                    this.business = result.data;
                }, error => {
                    this.alertService.error("Error loading business", error.message);
                });
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
                RouterService.openBusinessOverview();
            }, error => {
                this.alertService.error("Error updating business " + this.business.id, error.error.data);
            });
        } else {
            this.alertService.error("Business doesn't exist", "Can't edit");
        }
    }
    public openNewTab(url: string) {
        RouterService.openNewTab(url);
    }
    public formatTimeNumber(num: number) {
        if (num.toString().length < 2) {
            return "0" + num;
        } else {
            return num;
        }
    }
    public refreshCoordinates() {
        if (this.business && this.getAddressFull()) {
            this.businessManagerService.getGeocoding(this.getAddressFull()).subscribe(result => {
                if (result) {
                    let results = result.results;
                    if (results && results.length > 0 && results[0].geometry && results[0].geometry.location) {
                        this.lat = results[0].geometry.location.lat;
                        this.long = results[0].geometry.location.lng;
                    }
                }
            }, error => {
                    this.alertService.error("Error loading map.", error.error.error_message);
            });
        }
    }

    public getAddressFull(): string {
        let addressFull = "";
        if (this.business && this.business.businessInfo && this.business.businessInfo.address) {
            if (this.business.businessInfo.address.street) {
                addressFull += this.business.businessInfo.address.street + " "
            }
            if (this.business.businessInfo.address.city) {
                addressFull += this.business.businessInfo.address.city + " "
            }
            if (this.business.businessInfo.address.country) {
                addressFull += this.business.businessInfo.address.country + " "
            }
            if (this.business.businessInfo.address.postalCode) {
                addressFull += this.business.businessInfo.address.postalCode + " "
            }
            return addressFull;
        }
        return null;
    }
}
