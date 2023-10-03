import { Injectable } from '@angular/core'; 
import { ToastrService } from 'ngx-toastr';


@Injectable({
  providedIn: 'root',
})
export class ToasterService { 
  constructor(private toast: ToastrService) {}

  openToastSuccess(title: string, message: string) {
    this.toast.success(message, title, {
      timeOut: 3000,
    });
  }

  openToastError(title: string, message: string) {
    this.toast.error(message, title, {
      timeOut: 3000,
    });
  }

  openToastInfo(title: string, message: string) {
    this.toast.info(message, title, {
      timeOut: 3000,
    });
  }

  openToastWarning(title: string, message: string) {
    this.toast.warning(message, title, {
      timeOut: 3000,
    });
  }
}
