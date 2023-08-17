import { Injectable } from '@angular/core'; 
import { ToastrService } from 'ngx-toastr';


@Injectable({
  providedIn: 'root',
})
export class ToasterService { 
  constructor(private toast: ToastrService) {}

  openToastSuccess(title: string, message: string) {
    this.toast.success(title, message, {
      timeOut: 3000,
    });
  }

  openToastError(title: string, message: string) {
    this.toast.error(title, message, {
      timeOut: 3000,
    });
  }

  openToastInfo(title: string, message: string) {
    this.toast.info(title, message, {
      timeOut: 3000,
    });
  }

  openToastWarning(title: string, message: string) {
    this.toast.warning(title, message, {
      timeOut: 3000,
    });
  }
}
