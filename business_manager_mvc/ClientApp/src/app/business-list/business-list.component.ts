import { Component, OnInit, ViewChild } from '@angular/core';
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

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

    public imagesUrl: string;

    constructor(private businessManagerService: BusinessManagerService, private alertSerice: AlertService) {
      this.imagesUrl = environment.business_manager_orc_url + "/images/";
    }

  ngOnInit() {
    this.businessManagerService.searchBusinesses().subscribe(result => {
      //console.log(result.data);
      this.dataSource = new MatTableDataSource<BusinessDataModel>(result.data);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    }, error => {
      this.alertSerice.error("Error loading businesses", error.message);
    });
  }
  //getBusinessLogo(businessId: number): string {
  //  if (this.logos.has(businessId)) {
  //    return this.logos.get(businessId);
  //  } else {
  //    this.logos.set(businessId, null);
  //    this.businessManagerService.getBusinessLogo(businessId).subscribe(result => {
  //      if (result !== null && result.data !== null && result.data.value !== null) {
  //        this.logos.set(businessId, result.data.imageData);
  //      } else {
  //        this.logos.set(businessId, null);
  //      }
  //    }, error => {
  //        //this.alertSerice.warning("No logo", "No logo found for " + businessId);
  //        this.logos.set(businessId, null);
  //    });
  //  console.log(this.logos);
  //    return this.logos.get(businessId);
  //  }
  //}
}
