import { HttpClient } from '@angular/common/http';
import { BusinessDataModel } from "../../Model/business";
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { ImageModel } from '../../Model/image';
import { UserAccountModel } from '../../Model/user';
import { ResponseEnvelope } from '../../Model/respoonseEnvelope';

export class BusinessManagerService {

    private url: string;

    constructor(private http: HttpClient) {
        this.url = environment.business_manager_orc_url;
    }

    public saveBusiness(businessModel: BusinessDataModel): Observable<ResponseEnvelope> {
        console.log('CALL TO ' + this.url + '/business')
        return this.http.post<ResponseEnvelope>(this.url + '/business', businessModel)
    }
    public searchBusinesses(): Observable<ResponseEnvelope> {
        console.log('CALL TO ' + this.url + '/business')
        return this.http.get<ResponseEnvelope>(this.url + '/business')
    }

    public saveUser(user: UserAccountModel): Observable<ResponseEnvelope> {
        console.log('CALL TO ' + this.url + '/user')
        return this.http.post<ResponseEnvelope>(this.url + '/user', user)
    }

    public uploadImage(image: File, businessId: number): Observable<ResponseEnvelope> {
        const formData = new FormData();

        formData.append('image', image);

        console.log('CALL TO ' + this.url + '/image/business/' + businessId)
        return this.http.post<ResponseEnvelope>(this.url + '/image', formData);
    }

    public getBusinessImages(businessId: number): Observable<ResponseEnvelope> {

        return this.http.get<ResponseEnvelope>(this.url + 'business/' + businessId + '/image');
    }
}
