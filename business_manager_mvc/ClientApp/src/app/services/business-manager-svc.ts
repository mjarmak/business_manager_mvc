import { HttpClient } from '@angular/common/http';
import { BusinessDataModel } from "../../Model/business";
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { UserAccountModel } from '../../Model/user';
import { ResponseEnvelope } from '../../Model/responseEnvelope';
import { AlertService } from './alert-service';
import { OnInit } from '@angular/core';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class BusinessManagerService {

    private url: string;

    public businessTypes: string[];

    public days: string[];

    constructor(private http: HttpClient, private alertService: AlertService) {
        this.url = environment.business_manager_api_url;
    }

    public getGeocoding(address: string): Observable<any> {
        return this.http.get<any>("https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyArzIrvHH6ei9-x5FLY66LwBn0uhv8LWBs")
    }

    public saveBusiness(businessModel: BusinessDataModel): Observable<ResponseEnvelope<any>> {
        return this.http.post<ResponseEnvelope<any>>(this.url + '/business', businessModel)
    }
    public updateBusiness(businessModel: BusinessDataModel, id: number): Observable<ResponseEnvelope<any>> {
        return this.http.put<ResponseEnvelope<any>>(this.url + '/business/' + id, businessModel)
    }
    public searchBusinesses(country?: string, city?: string, openNow?: boolean, type?: string, onlyDisabled?: boolean): Observable<ResponseEnvelope<any>> {
        let path = '/business/search?'
        if (country) {
            path = path + "&country=" + country
        }
        if (city) {
            path = path + "&city=" + city
        }
        if (openNow) {
            path = path + "&openNow=" + openNow
        }
        if (type) {
            path = path + "&type=" + type
        }
        if (onlyDisabled) {
            path = path + "&onlyDisabled=" + onlyDisabled
        }

        return this.http.get<ResponseEnvelope<BusinessDataModel>>(this.url + path)
    }
    public getBusiness(businessId: number): Observable<ResponseEnvelope<BusinessDataModel>> {
        return this.http.get<ResponseEnvelope<BusinessDataModel>>(this.url + '/business/' + businessId)
    }

    public getBusinessTypes(): Observable<ResponseEnvelope<any>> {
        return this.http.get<ResponseEnvelope<any>>(this.url + '/business/types');
    }

    public refreshBusinessTypes(): void {
        if (this.businessTypes === undefined) {
            this.businessTypes = []
            this.getBusinessTypes().subscribe(result => {
                this.businessTypes = result.data;
            }, error => {
                this.businessTypes = undefined;
                this.alertService.error("Error loading bussiness types", error.message);
            });
        }
    }

    public getDays(): Observable<ResponseEnvelope<any>> {
        return this.http.get<ResponseEnvelope<any>>(this.url + '/business/days');
    }

    public enableBusiness(id: number): Observable<ResponseEnvelope<any>> {
        return this.http.get<ResponseEnvelope<any>>(this.url + '/business/' + id + "/enable");
    }
    public disableBusiness(id: number): Observable<ResponseEnvelope<any>> {
        return this.http.get<ResponseEnvelope<any>>(this.url + '/business/' + id + "/disable");
    }
    public deleteBusiness(id: number): Observable<ResponseEnvelope<any>> {
        return this.http.delete<ResponseEnvelope<any>>(this.url + '/business/' + id);
    }

    public refreshDays(): void {
        if (this.days === undefined) {
            this.days = []
            this.getDays().subscribe(result => {
                this.days = result.data;
            }, error => {
                this.days = undefined;
                this.alertService.error("Error loading days", error.message);
            });
        }
    }

    public uploadLogo(logo: File, businessId: number): Observable<ResponseEnvelope<any>> {
        if (logo === undefined) {
            return null;
        }
        const formData = new FormData();

        formData.append('image', logo);

        return this.http.post<ResponseEnvelope<any>>(this.url + '/business/' + businessId + '/logo', formData);
    }

    public uploadImage(image: File, businessId: number, imageId: number): Observable<ResponseEnvelope<any>> {
        if (image === undefined) {
            return null;
        }
        const formData = new FormData();

        formData.append('image', image);
        return this.http.post<ResponseEnvelope<any>>(this.url + '/business/' + businessId + '/photo/' + imageId, formData);
    }

    public updateLogo(logo: File, businessId: number): Observable<ResponseEnvelope<any>> {
        if (logo === undefined) {
            return null;
        }
        const formData = new FormData();

        formData.append('image', logo);

        return this.http.put<ResponseEnvelope<any>>(this.url + '/business/' + businessId + '/logo', formData);
    }

    public updateImage(image: File, businessId: number, imageId: number): Observable<ResponseEnvelope<any>> {
        if (image === undefined) {
            return null;
        }
        const formData = new FormData();

        formData.append('image', image);
        return this.http.put<ResponseEnvelope<any>>(this.url + '/business/' + businessId + '/photo/' + imageId, formData);
    }

    public getBusinessImages(businessId: number): Observable<ResponseEnvelope<any>> {
        return this.http.get<ResponseEnvelope<any>>(this.url + '/business/' + businessId + '/photo');
    }

    public getBusinessLogo(businessId: number): Observable<ResponseEnvelope<any>> {
        return this.http.get<ResponseEnvelope<any>>(this.url + '/business/' + businessId + '/logo');
    }
}
