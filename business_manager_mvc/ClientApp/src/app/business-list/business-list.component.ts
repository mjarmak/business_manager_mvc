import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { BusinessDataModel } from '../../Model/business';
import { MatPaginator } from '@angular/material/paginator';
import { BusinessManagerService } from '../services/business-manager-svc';
import { AlertService } from '../services/alert-service';

@Component({
    selector: 'app-business-list',
    templateUrl: './business-list.component.html'
})
export class BusinessListComponent implements OnInit {

    //displayedColumns: string[] = ['id', 'name', 'workHours'];
    businesses: BusinessDataModel[];
    //public dataSource = new MatTableDataSource<BusinessDataModel>();

    //paginator: MatPaginator;

    constructor(private businessManagerService: BusinessManagerService, private alertSerice: AlertService) {
    }

    ngOnInit() {
        this.businessManagerService.searchBusinesses().subscribe(result => {
            this.businesses = result.data;
            //this.dataSource.paginator = this.paginator;
            //this.dataSource = new MatTableDataSource<BusinessDataModel>(result);
        }, error => {
            this.alertSerice.error("Error loading bussinesses", error.message);
        });
  }
}
