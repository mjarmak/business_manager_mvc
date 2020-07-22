import { HttpClient } from '@angular/common/http';
import { BusinessDataModel } from "../../Model/business";
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { UserAccountModel } from '../../Model/user';
import { ResponseEnvelope } from '../../Model/responseEnvelope';
import { AlertService } from './alert-service';
import { OnInit } from '@angular/core';

export class BusinessManagerService {

    private url: string;

    public businessTypes: string[];

    constructor(private http: HttpClient, private alertService: AlertService) {
        this.url = environment.business_manager_api_url;
    }

    public openHomePage() {
        window.open("/", "_self");
    }

    public openLoginPage() {
        window.open("/login", "_self");
    }

  public openBusiness(businessId: number) {
    window.open("/business-detail/" + businessId, "_self")
  }

    public saveBusiness(businessModel: BusinessDataModel): Observable<ResponseEnvelope> {
        console.log('CALL TO ' + this.url + '/business')
        return this.http.post<ResponseEnvelope>(this.url + '/business', businessModel)
  }
  public searchBusinesses(): Observable<ResponseEnvelope> {
    console.log('CALL TO ' + this.url + '/business')
    return this.http.get<ResponseEnvelope>(this.url + '/business')
  }
  public getBusiness(businessId: number): Observable<ResponseEnvelope> {
    console.log('CALL TO ' + this.url + '/business/' + businessId)
    return this.http.get<ResponseEnvelope>(this.url + '/business/' + businessId)
    }

    public getBusinessTypes(): Observable<ResponseEnvelope> {
      console.log('CALL TO ' + this.url + '/business/types')
      return this.http.get<ResponseEnvelope>(this.url + '/business/types');
    }
    public refreshBusinessTypes(): void {
        if (this.businessTypes === undefined) {
            this.businessTypes = []
            console.log("k " + this.businessTypes);
            this.getBusinessTypes().subscribe(result => {
                this.businessTypes = result.data;
            }, error => {
                this.businessTypes = undefined;
                console.log("nope " + this.businessTypes);
                this.alertService.error("Error loading bussiness types", error.message);
            });
        }
    }

    public saveUser(user: UserAccountModel): Observable<ResponseEnvelope> {
        console.log('CALL TO ' + this.url + '/user')
        return this.http.post<ResponseEnvelope>(this.url + '/user', user)
    }

    public uploadLogo(logo: File, businessId: number): Observable<ResponseEnvelope> {
        if (logo === undefined) {
            return null;
        }
        const formData = new FormData();

        formData.append('image', logo);

        console.log('CALL TO ' + this.url + '/business/' + businessId + '/logo')
        return this.http.post<ResponseEnvelope>(this.url + '/business/' + businessId + '/logo', formData);
    }

    public uploadImage(image: File, businessId: number, imageId: number): Observable<ResponseEnvelope> {
        if (image === undefined) {
            return null;
        }
        const formData = new FormData();

        formData.append('image', image);
        console.log('CALL TO ' + this.url + '/business/' + businessId + '/photo/' + imageId)
        return this.http.post<ResponseEnvelope>(this.url + '/business/' + businessId + '/photo/' + imageId, formData);
    }

    public getBusinessImages(businessId: number): Observable<ResponseEnvelope> {
        console.log('CALL TO ' + this.url + '/business/' + businessId + '/photo')
        return this.http.get<ResponseEnvelope>(this.url + '/business/' + businessId + '/photo');
    }

    public getBusinessLogo(businessId: number): Observable<ResponseEnvelope> {
        console.log('CALL TO ' + this.url + '/business/' + businessId + '/logo')
        return this.http.get<ResponseEnvelope>(this.url + '/business/' + businessId + '/logo');
    }
}
