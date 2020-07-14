import { HttpClient } from '@angular/common/http';
import { BusinessDataModel } from "../../Model/business";
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { ImageModel } from '../../Model/image';
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
    public searchBusinesses(): Observable<BusinessDataModel[]> {
        console.log('CALL TO ' + this.url + '/business')
        return this.http.get<BusinessDataModel[]>(this.url + '/business')
    }

    public saveUser(user: UserAccountModel): Observable<UserAccountModel> {
        console.log('CALL TO ' + this.url + '/user')
        return this.http.post<UserAccountModel>(this.url + '/user', user)
    }

    public uploadImage(image: File, businessId: number): Observable<ImageModel> {
        const formData = new FormData();

        formData.append('image', image);

        console.log('CALL TO ' + this.url + '/image/business/' + businessId)
        return this.http.post<ImageModel>(this.url + '/image', formData);
    }
}
