import { ToastrService } from "ngx-toastr";

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
