import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { BusinessDataModel } from '../../Model/business';

@Component({
    selector: 'app-business-create',
    templateUrl: './business-create.component.html'
})
export class BusinessCreateComponent {

    public business: BusinessDataModel;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        let url = environment.business_manager_api_url + 'business';
        this.business = new BusinessDataModel;
        console.log('CALL TO ' + url)
        console.log('user is ' + this.business)
    }

}
