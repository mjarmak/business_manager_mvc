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

    displayedColumns: string[] = ['logo', 'id'];
    public dataSource = new MatTableDataSource<BusinessDataModel>();

  paginator: MatPaginator;

  public logos: Map<number, string>;

    constructor(private businessManagerService: BusinessManagerService, private alertSerice: AlertService) {
    }

  ngOnInit() {
    this.logos = new Map();
    this.businessManagerService.searchBusinesses().subscribe(result => {
      this.dataSource.paginator = this.paginator;
      this.dataSource = new MatTableDataSource<BusinessDataModel>(result.data);
    }, error => {
      this.alertSerice.error("Error loading bussinesses", error.message);
    });
  }
  getBusinessLogo(businessId: number): string {
    if (this.logos.has(businessId)) {
      return this.logos.get(businessId);
    } else {
      this.logos.set(businessId, null);
      this.businessManagerService.getBusinessLogo(businessId).subscribe(result => {
        if (result !== null && result.data !== null && result.data.value !== null) {
          this.logos.set(businessId, result.data.imageData);
        } else {
          this.logos.set(businessId, null);
        }
      }, error => {
          //this.alertSerice.warning("No logo", "No logo found for " + businessId);
          this.logos.set(businessId, null);
      });
    console.log(this.logos);
      return this.logos.get(businessId);
    }
  }
}
