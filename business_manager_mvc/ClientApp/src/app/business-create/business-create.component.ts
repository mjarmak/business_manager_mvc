import { Component, Inject, OnInit } from '@angular/core';
import { BusinessDataModel } from '../../Model/business';
import { BusinessManagerService } from '../services/business-manager-svc';
import { AlertService } from '../services/alert-service';

@Component({
    selector: 'app-business-create',
    templateUrl: './business-create.component.html'
})
export class BusinessCreateComponent implements OnInit {

    public business: BusinessDataModel;
    public logo: File;
    public images: File[];

    constructor(private businessManagerService: BusinessManagerService, private alertSerice: AlertService) {

    }

    ngOnInit(): void {
        this.business = new BusinessDataModel();
        this.businessManagerService.refreshBusinessTypes();
    }

    public onClickSave() {
        this.businessManagerService.saveBusiness(this.business).subscribe(result => {
            console.log(result.data)
            this.business = result.data;
            document.getElementById("btnSave").setAttribute("disabled", "disabled");

            if (this.logo) {
                this.businessManagerService.uploadLogo(this.logo, this.business.id).subscribe(
                    result => {
                    },
                    error => {
                        this.alertSerice.error("Error adding logo", error.message);
                    })
            }
          if (this.images) {
            var i = 0;
            this.images.forEach((image: File) => {
              i++;
              this.businessManagerService.uploadImage(image, this.business.id, i).subscribe(
                result => {
                },
                error => {
                  this.alertSerice.error("Error adding image", error.message);
                })
            })
          }
        }, error => {
            this.alertSerice.error("Error creating business", error.message);
        });

    }
    processLogo(imageInput: any) {
        const file: File = imageInput.files[0];
        const reader = new FileReader();

        reader.addEventListener('load', (event: any) => {
            this.logo = file
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
                console.log("CANT PUT MORE THAN 5 IMAGES")
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
