import { HttpClient } from '@angular/common/http';
import { BusinessDataModel } from "../../Model/business";
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { ImageModel } from '../../Model/image';
import { Component } from '@angular/core';
import { UserAccountModel } from '../../Model/user';

export class BusinessManagerService {

    private url: string;

    constructor(private http: HttpClient) {
        this.url = environment.business_manager_orc_url;
    }

    public saveBusiness(businessModel: BusinessDataModel): Observable<BusinessDataModel> {
        console.log('CALL TO ' + this.url + '/business')
        return this.http.post<BusinessDataModel>(this.url + '/business', businessModel)
    }

    public saveUser(user: UserAccountModel): Observable<UserAccountModel> {
        console.log('CALL TO ' + this.url + '/user')
        return this.http.post<UserAccountModel>(this.url + '/user', user)
    }

    public uploadImage(image: File): Observable<ImageModel> {
        const formData = new FormData();

        formData.append('image', image);

        console.log('CALL TO ' + this.url + '/image')
        return this.http.post<ImageModel>(this.url + '/image', formData);
    }
}
