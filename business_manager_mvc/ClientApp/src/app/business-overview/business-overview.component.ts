import { Component, Inject, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { AlertService } from '../services/alert-service';
import { BusinessManagerService } from '../services/business-manager-svc';
import { BusinessListComponent } from '../business-list/business-list.component';

@Component({
    selector: 'app-business-overview',
    templateUrl: './business-overview.component.html'
})
export class BusinessOverviewComponent implements OnInit, OnDestroy {

    @ViewChild(BusinessListComponent, { static: false }) businessListComponent: BusinessListComponent;
    type: string;
    city: string;
    country: string;
    openNow: string;
    role: string;
    onlyDisabled: string

    interval: any;

    constructor(private businessManagerService: BusinessManagerService) {
        this.role = localStorage.getItem("userrole");
    }

    ngOnInit() {
        this.businessManagerService.refreshBusinessTypes();

        this.interval = setInterval(() => {
            this.businessListComponent.refresh();
        }, 60000 * 15);
    }

    ngOnDestroy(): void {
        clearInterval(this.interval);
    }

    public setBusinessType(type: string) {
        this.type = type;
    }
}
