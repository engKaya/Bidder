import { Component } from '@angular/core';
import { ToasterService } from 'src/app/bidder.common/common.services/toaster.service';
import { BiddingService } from '../../module.services/bidding.service';
import { AbstractControl, AsyncValidatorFn, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms'; 
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { CreateBidRequest } from '../../module.objects/Requests/CreateBidRequest.request';

@Component({
  selector: 'app-bidding-pup',
  templateUrl: './bidding-pup.component.html'
})
export class BiddingPupComponent {
  constructor(
    private readonly toast: ToasterService,
    private readonly bidService: BiddingService,
    private readonly translate: TranslateService,
  ) {}
  labelPosition : 'before' | 'after' = 'before';
  role: boolean = false;
  form: FormGroup = new FormGroup({
    Title: new FormControl('', Validators.required),
    Description: new FormControl('', Validators.required),
    MinPrice: new FormControl(null, [
      Validators.required, 
    ]),
    HasIncreaseRest: new FormControl(false, Validators.required),
    MinPriceIncrease: new FormControl('', [ 
      Validators.pattern('^[0-9]+(\.[0-9]{1,2})?$'), 
    ]), 
  }); 
  get f() {
    return this.form.controls;
  }

  submit() { 
    if(this.form.invalid){
      this.toast.openToastError(this.translate.instant('FORM_VALIDATIONS.ERROR'),this.translate.instant('FORM_VALIDATIONS.FORM_ERRORS'));
      return;
    }
 
    var obj = new CreateBidRequest(
      this.form.value.Title,
      this.form.value.Description,
      Number.parseFloat(this.form.value.MinPrice),
      this.form.value.HasIncreaseRest,
      Number.parseFloat(this.form.value.MinPriceIncrease) 
    );

    this.bidService.CreateBid(obj).then((response) => {
      console.log(response);
    }).catch((error) => {});

  }
    
  getField(field: string): string {
    var fieldname = this.translate.instant(`LABELS.${field}`);
    return this.translate.instant('FORM_VALIDATIONS.REQUIRED', {
      field: fieldname,
    });
  } 
}
