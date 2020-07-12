import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { BusinessDataModel } from '../../Model/business';
import { BusinessManagerService } from '../services/business-manager-svc';

@Component({
    selector: 'app-business-create',
    templateUrl: './business-create.component.html'
})
export class BusinessCreateComponent {

    public business: BusinessDataModel;
    public logo: File;
    public images: File[];

    constructor(private businessManagerService: BusinessManagerService) {
        this.business = new BusinessDataModel;
    }
    public onClickSave() {
        console.log('business is ' + this.business)
        this.businessManagerService.saveBusiness(this.business).subscribe(result => {
            this.business = result;
        });

        this.businessManagerService.uploadImage(this.logo).subscribe(
            (res) => {
            },
            (err) => {

            })
        this.images.forEach(
            (image : File) => {
                this.businessManagerService.uploadImage(image).subscribe(
                    (res) => {
                    },
                    (err) => {

                    })
            }
        )
    }
    processFile(imageInput: any) {
        const file: File = imageInput.files[0];
        const reader = new FileReader();

        reader.addEventListener('load', (event: any) => {
            if (this.images.length < 5) {
                this.images.push(file);
            } else {
                console.log("CANT PUT MORE THAN 5 IMAGES")
            }
        });
        reader.readAsDataURL(file);
    }
}
