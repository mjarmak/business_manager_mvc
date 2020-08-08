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

    public saveBusiness(businessModel: BusinessDataModel): Observable<ResponseEnvelope> {
        return this.http.post<ResponseEnvelope>(this.url + '/business', businessModel)
    }
    public updateBusiness(businessModel: BusinessDataModel, id: number): Observable<ResponseEnvelope> {
        return this.http.put<ResponseEnvelope>(this.url + '/business/' + id, businessModel)
    }
    public searchBusinesses(country?: string, city?: string, openNow?: boolean, type?: string, onlyDisabled?: boolean): Observable<ResponseEnvelope> {
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

        return this.http.get<ResponseEnvelope>(this.url + path)
    }
    public getBusiness(businessId: number): Observable<ResponseEnvelope> {
        return this.http.get<ResponseEnvelope>(this.url + '/business/' + businessId)
    }

    public getBusinessTypes(): Observable<ResponseEnvelope> {
        return this.http.get<ResponseEnvelope>(this.url + '/business/types');
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

    public getDays(): Observable<ResponseEnvelope> {
        return this.http.get<ResponseEnvelope>(this.url + '/business/days');
    }

    public enableBusiness(id: number): Observable<ResponseEnvelope> {
        return this.http.get<ResponseEnvelope>(this.url + '/business/' + id + "/enable");
    }
    public disableBusiness(id: number): Observable<ResponseEnvelope> {
        return this.http.get<ResponseEnvelope>(this.url + '/business/' + id + "/disable");
    }
    public deleteBusiness(id: number): Observable<ResponseEnvelope> {
        return this.http.delete<ResponseEnvelope>(this.url + '/business/' + id);
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

    public uploadLogo(logo: File, businessId: number): Observable<ResponseEnvelope> {
        if (logo === undefined) {
            return null;
        }
        const formData = new FormData();

        formData.append('image', logo);

        return this.http.post<ResponseEnvelope>(this.url + '/business/' + businessId + '/logo', formData);
    }

    public uploadImage(image: File, businessId: number, imageId: number): Observable<ResponseEnvelope> {
        if (image === undefined) {
            return null;
        }
        const formData = new FormData();

        formData.append('image', image);
        return this.http.post<ResponseEnvelope>(this.url + '/business/' + businessId + '/photo/' + imageId, formData);
    }

    public updateLogo(logo: File, businessId: number): Observable<ResponseEnvelope> {
        if (logo === undefined) {
            return null;
        }
        const formData = new FormData();

        formData.append('image', logo);

        return this.http.put<ResponseEnvelope>(this.url + '/business/' + businessId + '/logo', formData);
    }

    public updateImage(image: File, businessId: number, imageId: number): Observable<ResponseEnvelope> {
        if (image === undefined) {
            return null;
        }
        const formData = new FormData();

        formData.append('image', image);
        return this.http.put<ResponseEnvelope>(this.url + '/business/' + businessId + '/photo/' + imageId, formData);
    }

    public getBusinessImages(businessId: number): Observable<ResponseEnvelope> {
        return this.http.get<ResponseEnvelope>(this.url + '/business/' + businessId + '/photo');
    }

    public getBusinessLogo(businessId: number): Observable<ResponseEnvelope> {
        return this.http.get<ResponseEnvelope>(this.url + '/business/' + businessId + '/logo');
    }
}
