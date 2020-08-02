import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { BusinessDataModel } from '../../Model/business';
import { BusinessManagerService } from '../services/business-manager-svc';
import { AlertService } from '../services/alert-service';
import { environment } from '../../environments/environment';
import { RouterService } from '../services/router-service';


@Component({
    selector: 'app-business-list',
    templateUrl: './business-list.component.html'
})
export class BusinessListComponent implements OnInit {
  displayedColumns: string[] = ['logo', 'id', "name"];
    dataSource = new MatTableDataSource<BusinessDataModel>();
    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;
    @Input() type: string;
    @Input() city: string;
    @Input() country: string;
    @Input() openNow: boolean;

    public imagesUrl: string;

    constructor(private businessManagerService: BusinessManagerService, private alertService: AlertService) {
        this.imagesUrl = environment.business_manager_api_url + "/images/"
    }

    ngOnInit() {
        this.refresh();
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }

    refresh() {
        this.businessManagerService.searchBusinesses(this.country, this.city, this.openNow, this.type).subscribe(result => {
            this.dataSource.data = result.data;
        }, error => {
            this.alertService.error("Error loading bussinesses", error.message);
        });
    }
    public openBusiness(businessId: number) {
        RouterService.openBusiness(businessId);
    }
}
