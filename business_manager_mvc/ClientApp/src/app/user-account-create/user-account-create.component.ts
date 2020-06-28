import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { UserAccountModel } from '../../Model/user';

@Component({
    selector: 'app-user-account-create',
    templateUrl: './user-account-create.component.html'
})
export class FetchDataComponent {
    public forecasts: WeatherForecast[];

    public name: UserAccountModel;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        let url = environment.business_manager_api_url + 'user-account';
        console.log('CALL TO ' + url)
        http.get<WeatherForecast[]>(url).subscribe(result => {
        this.forecasts = result;
    }, error => console.error(error));
  }
}

interface WeatherForecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}
