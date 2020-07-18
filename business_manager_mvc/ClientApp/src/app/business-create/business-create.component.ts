import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
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
    }

    public onClickSave() {
      this.businessManagerService.saveBusiness(this.business).subscribe(result => {
        console.log(result.data.value)
        this.business = result.data.value;
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
          this.images.forEach(
            (image: File) => {
              console.log("addding imaage")
              this.businessManagerService.uploadImage(image, this.business.id).subscribe(
                result => {
                },
                error => {
                  this.alertSerice.error("Error adding image", error.message);
                })
            }
          )
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
}
