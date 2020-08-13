import { Component, Inject, OnInit } from '@angular/core';
import { BusinessDataModel, WorkHoursData } from '../../Model/business';
import { BusinessManagerService } from '../services/business-manager-svc';
import { AlertService } from '../services/alert-service';
import { RouterService } from '../services/router-service';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../environments/environment';


@Component({
    selector: 'app-business-create',
    templateUrl: './business-create.component.html'
})
export class BusinessCreateComponent implements OnInit {

    public business: BusinessDataModel;
    public logo: File;
    public images: Map<number, File> = new Map;
    imagesUrl: string;

    public errors = []

    constructor(
        private businessManagerService: BusinessManagerService,
        private alertService: AlertService,
        private readonly route: ActivatedRoute
    ) {
        this.imagesUrl = environment.business_manager_api_url + "/images/";
    }

    ngOnInit(): void {
        this.business = new BusinessDataModel();

        const businessId = this.route.snapshot.paramMap.get("businessId");

        if (businessId) {
            this.businessManagerService.getBusiness(Number(businessId)).subscribe(result => {
                this.business = result.data;
            }, error => {
                    this.alertService.error("Error loading business", error.error.message);
            });
        }
        else {
            this.business.workHours = [
                new WorkHoursData("MONDAY", 9, 19, 0, 0, false),
                new WorkHoursData("TUESDAY", 9, 19, 0, 0, false),
                new WorkHoursData("WEDNESDAY", 9, 19, 0, 0, false),
                new WorkHoursData("THURSDAY", 9, 19, 0, 0, false),
                new WorkHoursData("FRIDAY", 9, 19, 0, 0, false),
                new WorkHoursData("SATURDAY", 9, 19, 0, 0, true),
                new WorkHoursData("SUNDAY", 9, 19, 0, 0, true)
            ]
        }

        this.businessManagerService.refreshBusinessTypes();
        this.businessManagerService.refreshDays();

        if (localStorage.getItem("useremail")) {
            this.business.identification.emailPro = localStorage.getItem("useremail");
        }
    }

    public onClickSave() {
        if (this.business.id) {
            this.businessManagerService.updateBusiness(this.business, this.business.id).subscribe(result => {
                this.errors = [];
                this.alertService.success("Business Updated", "");
            }, error => {
                this.errors = error.error.data;
                    this.alertService.error("Error creating business", error.error.message);
            });
        } else {
            this.businessManagerService.saveBusiness(this.business).subscribe(result => {
                this.business = result.data;
                this.errors = [];
                document.getElementById("btnSave").setAttribute("disabled", "disabled");

                if (this.logo) {
                    this.businessManagerService.uploadLogo(this.logo, this.business.id).subscribe(
                        result => {
                        },
                        error => {
                            this.alertService.error("Error adding logo", error.error.message);
                        });
                }
                if (this.images) {
                    this.images.forEach((image: File, index: number) => {
                        this.businessManagerService.uploadImage(image, this.business.id, index).subscribe(
                            result => {
                            },
                            error => {
                                this.alertService.error("Error adding image", error.error.message);
                            });
                    });
                }
                RouterService.openBusiness(this.business.id);
            }, error => {
                this.errors = error.error.data;
                    this.alertService.error("Error creating business", error.error.message);
            });
        }
    }

    processLogo(imageInput: any) {
        const file: File = imageInput.files[0];
        const reader = new FileReader();

        reader.addEventListener('load', (event: any) => {
            this.logo = file;
            if (this.business.id) {
                this.businessManagerService.updateLogo(this.logo, this.business.id).subscribe(
                    result => {
                        this.businessManagerService.getBusiness(this.business.id).subscribe(result => {
                            if (result.data && result.data.identification && result.data.identification.logoPath) {
                                this.business.identification.logoPath = result.data.identification.logoPath;
                            }
                        }, error => {
                            this.alertService.error("Error loading business", error.error.message);
                        });
                    },
                    error => {
                        if (error.status === 413) {
                            this.alertService.error("Logo too large", "Max size is 1MB.");
                        } else {
                            this.alertService.error("Error adding logo", error.message);
                        }
                    });
            }
        });
        reader.readAsDataURL(file);
    }

    processFile(imageInput: any, index: number) {
        const file: File = imageInput.files[0];
        const reader = new FileReader();

        reader.addEventListener('load', (event: any) => {
            if (this.images.size > 5 || index > 5 || index < 1) {
                this.alertService.warning("Can't add image", "Can't add more than 5 images");
            } else {
                this.images.set(index, file);
                if (this.business.id) {
                    this.businessManagerService.updateImage(this.images.get(index), this.business.id, index).subscribe(
                        result => {
                            this.businessManagerService.getBusiness(this.business.id).subscribe(result => {
                                if (result.data && result.data.businessInfo) {
                                    if (result.data.businessInfo.photoPath1) {
                                        this.business.businessInfo.photoPath1 = result.data.businessInfo.photoPath1;
                                    }
                                    if (result.data.businessInfo.photoPath1) {
                                        this.business.businessInfo.photoPath2 = result.data.businessInfo.photoPath2;
                                    }
                                    if (result.data.businessInfo.photoPath1) {
                                        this.business.businessInfo.photoPath3 = result.data.businessInfo.photoPath3;
                                    }
                                    if (result.data.businessInfo.photoPath1) {
                                        this.business.businessInfo.photoPath4 = result.data.businessInfo.photoPath4;
                                    }
                                    if (result.data.businessInfo.photoPath1) {
                                        this.business.businessInfo.photoPath5 = result.data.businessInfo.photoPath5;
                                    }
                                }
                            }, error => {
                                    this.alertService.error("Error loading business", error.error.message);
                            });
                        },
                        error => {
                            if (error.status === 413) {
                                this.alertService.error("Picture too large", "Max size is 5MB.");
                            } else {
                                this.alertService.error("Error adding Picture", error.message);
                            }
                        });
                }
            }
        });
        reader.readAsDataURL(file);
    }
    public setBusinessType(type: string) {
        this.business.identification.type = type;
    }
    public openBusiness(businessId: number) {
        RouterService.openBusiness(businessId);
    }
}
