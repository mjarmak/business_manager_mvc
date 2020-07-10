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
    private url: string;

    constructor(private http: HttpClient) {
        this.url = environment.business_manager_orc_url + '/business';
        this.business = new BusinessDataModel;
    }
    public onClickSave() {
        console.log('CALL TO ' + this.url)
        console.log('user is ' + this.business)
        this.http.post<BusinessDataModel>(this.url, this.business).subscribe(result => {
            this.business = result;
        });
    }
}
