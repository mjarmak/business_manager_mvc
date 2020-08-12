import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { BusinessDataModel, WorkHoursData } from '../../Model/business';
import { BusinessManagerService } from '../services/business-manager-svc';
import { AlertService } from '../services/alert-service';
import { environment } from '../../environments/environment';
import { RouterService } from '../services/router-service';


@Component({
    selector: 'app-business-list',
    templateUrl: './business-list.component.html'
})
export class BusinessListComponent implements OnInit {
    @Input() displayedColumns: string[];
    dataSource = new MatTableDataSource<BusinessDataModel>();
    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;
    @Input() type: string;
    @Input() city: string;
    @Input() country: string;
    @Input() openNow: boolean;
    @Input() onlyDisabled: boolean;

    readonly days = ['MONDAY', 'TUESDAY', 'WEDNESDAY', 'THURSDAY', 'FRIDAY', 'SATURDAY', 'SUNDAY'];


    public imagesUrl: string;

    constructor(private businessManagerService: BusinessManagerService, private alertService: AlertService) {
        this.imagesUrl = environment.business_manager_api_url + "/images/"
    }

    ngOnInit() {
        if (!this.displayedColumns) {
            this.displayedColumns = ['logo', 'id', "name"];
        }
        this.refresh();
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    refresh() {
        this.businessManagerService.searchBusinesses(this.country, this.city, this.openNow, this.type, this.onlyDisabled).subscribe(result => {
            this.dataSource.data = result.data;
        }, error => {
            this.alertService.error("Error loading bussinesses", error.error.data);
        });
    }
    public openBusiness(businessId: number) {
        RouterService.openBusiness(businessId);
    }

    public enableBusiness(id: number) {
        return this.businessManagerService.enableBusiness(id).subscribe(result => {
            this.refresh();
        }, error => {
            this.alertService.error("Error updating business " + id, error.error.data);
        });
    }
    public disableBusiness(id: number) {
        return this.businessManagerService.disableBusiness(id).subscribe(result => {
            this.refresh();
        }, error => {
            this.alertService.error("Error updating business " + id, error.error.data);
        });
    }
    public deleteBusiness(id: number) {
        return this.businessManagerService.deleteBusiness(id).subscribe(result => {
            this.refresh();
        }, error => {
            this.alertService.error("Error updating business " + id, error.error.data);
        });
    }
    public isOpenNow(workhours: WorkHoursData[]): boolean {
        const date = new Date();
        const day = this.days[date.getDay() - 1];
        const hour = date.getHours();
        const minute = date.getMinutes();

        const workhour = workhours.find(w => w.day == day);

        if (workhour) {
            const workhourTimeFrom = workhour.hourFrom + workhour.minuteFrom / 60;
            const workhourTimeTo = workhour.hourTo + workhour.minuteTo / 60;
            const timeCurrent = hour + minute / 60;
            return (workhourTimeFrom < timeCurrent && workhourTimeTo > timeCurrent);
        } else {
            return false;
        }
    }
}
