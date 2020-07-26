import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { BusinessDataModel } from '../../Model/business';
import { BusinessManagerService } from '../services/business-manager-svc';
import { AlertService } from '../services/alert-service';
import { environment } from '../../environments/environment';
import { MatSort } from '@angular/material/sort';


@Component({
    selector: 'app-business-list',
    templateUrl: './business-list.component.html'
})
export class BusinessListComponent implements OnInit {
  displayedColumns: string[] = ['logo', 'id', "name"];
  dataSource = new MatTableDataSource<BusinessDataModel>();
  sort: MatSort;
    paginator: MatPaginator;
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
    }

    refresh() {
        this.businessManagerService.searchBusinesses(this.country, this.city, this.openNow, this.type).subscribe(result => {
            this.dataSource.data = result.data;
            this.dataSource.sort = this.sort;
            this.dataSource.paginator = this.paginator;
        }, error => {
            this.alertService.error("Error loading bussinesses", error.message);
        });
    }
}
