import { Component, Inject, OnInit } from '@angular/core';
import { AlertService } from '../services/alert-service';

@Component({
    selector: 'app-business-overview',
    templateUrl: './business-overview.component.html'
})
export class BusinessOverviewComponent implements OnInit {

    constructor(private alertSerice: AlertService) {
    }

    ngOnInit() {
  }
}
