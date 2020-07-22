import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { BusinessDataModel } from '../../Model/business';
import { MatPaginator } from '@angular/material/paginator';
import { BusinessManagerService } from '../services/business-manager-svc';
import { AlertService } from '../services/alert-service';
import { environment } from '../../environments/environment';

@Component({
    selector: 'app-business-list',
    templateUrl: './business-list.component.html'
})
export class BusinessListComponent implements OnInit {

    displayedColumns: string[] = ['logo', 'id'];
    public dataSource = new MatTableDataSource<BusinessDataModel>();

  paginator: MatPaginator;

    public imagesUrl: string;

    constructor(private businessManagerService: BusinessManagerService, private alertSerice: AlertService) {
        this.imagesUrl = environment.business_manager_api_url + "/images/"
    }

  ngOnInit() {
    this.businessManagerService.searchBusinesses().subscribe(result => {
      this.dataSource.paginator = this.paginator;
      this.dataSource = new MatTableDataSource<BusinessDataModel>(result.data);
    }, error => {
      this.alertSerice.error("Error loading bussinesses", error.message);
    });
  }
}
