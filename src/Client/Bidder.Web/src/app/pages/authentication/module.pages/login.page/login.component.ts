import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { AuthLoginService } from '../../module.services/auth.service';
import { LoginRequest } from '../../module.objects/loginobjects.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ToasterService } from 'src/app/bidder.common/common.services/toaster.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class AppSideLoginComponent {
  IsLoading$: Observable<boolean>; 
  returnUrl: string = "";
  constructor(
    private translate: TranslateService,
    private authLoginService: AuthLoginService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToasterService
  ) {
    this.IsLoading$ =  this.authLoginService.IsLoading$;   
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }
  error: string = '';
  LoginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });
  submit() {
    if (this.LoginForm.valid) {
      var model = new LoginRequest(
        this.LoginForm.value.email,
        this.LoginForm.value.password
      ); 
      this.authLoginService
        .login(model)
        .then((response) => {
          if (response.StatusCode !== 200) {
            this.error = this.translate.instant(
              `ERROR_CODES.LOGIN.${response.StatusCode}`
            ); 
            return;
          } 
          this.router.navigate([this.returnUrl.replace('%2F', '/')]);
        })
        .catch((error) => {
          this.error = this.translate.instant(
            `ERROR_CODES.LOGIN.${error.status}`
          );
          this.toastr.openToastError(this.error,"Error")
        });
    } else {
      this.error = this.translate.instant('FORM_VALIDATIONS.FORM_ERRORS');
    }
  }
  getField(field: string): string {
    var fieldname = this.translate.instant(`LABELS.${field}`);
    return this.translate.instant('FORM_VALIDATIONS.REQUIRED', {
      field: fieldname,
    });
  }
}
