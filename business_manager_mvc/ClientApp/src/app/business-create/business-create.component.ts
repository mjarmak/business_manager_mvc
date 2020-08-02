import { Component, Inject, OnInit } from '@angular/core';
import { BusinessDataModel, WorkHoursData } from '../../Model/business';
import { BusinessManagerService } from '../services/business-manager-svc';
import { AlertService } from '../services/alert-service';
import { RouterService } from '../services/router-service';


@Component({
    selector: 'app-business-create',
    templateUrl: './business-create.component.html'
})
export class BusinessCreateComponent implements OnInit {

    public business: BusinessDataModel;
    public logo: File;
    public images: File[];

    public errors = []

    constructor(private businessManagerService: BusinessManagerService, private alertSerice: AlertService) {

    }

    ngOnInit(): void {
        this.business = new BusinessDataModel();
        this.business.workHours = [
            new WorkHoursData("MONDAY", 9, 19, 0, 0, false),
            new WorkHoursData("TUESDAY", 9, 19, 0, 0, false),
            new WorkHoursData("WEDNESDAY", 9, 19, 0, 0, false),
            new WorkHoursData("THURSDAY", 9, 19, 0, 0, false),
            new WorkHoursData("FRIDAY", 9, 19, 0, 0, false),
            new WorkHoursData("SATURDAY", 9, 19, 0, 0, true),
            new WorkHoursData("SUNDAY", 9, 19, 0, 0, true)
        ]

        this.businessManagerService.refreshBusinessTypes();
        this.businessManagerService.refreshDays();

        if (localStorage.getItem("useremail")) {
            this.business.identification.emailPro = localStorage.getItem("useremail");
        }
    }

    public onClickSave() {
        this.businessManagerService.saveBusiness(this.business).subscribe(result => {
            console.log(result.data);
            this.business = result.data;
            this.errors = [];
            document.getElementById("btnSave").setAttribute("disabled", "disabled");

            if (this.logo) {
                this.businessManagerService.uploadLogo(this.logo, this.business.id).subscribe(
                    result => {
                    },
                    error => {
                        this.errors.concat(error.error.data);
                        this.alertSerice.error("Error adding logo", error.message);
                    });
            }
          if (this.images) {
            var i = 0;
            this.images.forEach((image: File) => {
              i++;
              this.businessManagerService.uploadImage(image, this.business.id, i).subscribe(
                result => {
                },
                  error => {
                      this.errors.concat(error.error.data);
                  this.alertSerice.error("Error adding image", error.message);
                });
            });
            }
            RouterService.openBusiness(this.business.id);
        }, error => {
            this.errors = error.error.data;
            this.alertSerice.error("Error creating business", error.message);
        });

  }

  processLogo(imageInput: any) {
        const file: File = imageInput.files[0];
        const reader = new FileReader();

        reader.addEventListener('load', (event: any) => {
            this.logo = file;
        });
        reader.readAsDataURL(file);
    }

    processFile(imageInput: any) {
        const file: File = imageInput.files[0];
        const reader = new FileReader();

        reader.addEventListener('load', (event: any) => {
            if (!this.images) {
                this.images = [];
            }
            if (this.images.length >= 5) {
                console.log("CANT PUT MORE THAN 5 IMAGES");
                this.alertSerice.warning("Can't add image", "Can't add more than 5 images");
            } else {
                this.images.push(file);
            }
        });
        reader.readAsDataURL(file);
    }
    public setBusinessType(type: string) {
        this.business.identification.type = type;
    }
}
