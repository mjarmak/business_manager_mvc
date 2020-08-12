import { ToastrService } from "ngx-toastr";
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class AlertService {

    constructor(private toastr: ToastrService) {
    }

    success(title: string, message: string) {
        this.toastr.success(message, title)
    }
    error(title: string, message: string) {
        this.toastr.error(message, title);

    }
    info(title: string, message: string) {
        this.toastr.info(message, title);
    }
    warning(title: string, message: string) {
        this.toastr.warning(message, title);
    } 

}
