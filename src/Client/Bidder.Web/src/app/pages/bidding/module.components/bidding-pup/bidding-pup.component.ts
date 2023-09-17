import { Component } from '@angular/core';
import { ToasterService } from 'src/app/bidder.common/common.services/toaster.service';
import { BiddingService } from '../../module.services/bidding.service';
import { FormControl, FormGroup, Validators } from '@angular/forms'; 
import { TranslateService } from '@ngx-translate/core';

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

  form: FormGroup = new FormGroup({
    Title: new FormControl('', Validators.required),
    Description: new FormControl('', Validators.required),
    MinPrice: new FormControl(null, [
      Validators.required,
      Validators.pattern('^[0-9]*$'),
    ]),
    HasIncreaseRest: new FormControl(false, Validators.required),
    MinPriceIncrease: new FormControl('', [ 
      Validators.pattern('^[0-9]*$'),
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

    console.log(this.form.value);
  }
    
  getField(field: string): string {
    var fieldname = this.translate.instant(`LABELS.${field}`);
    return this.translate.instant('FORM_VALIDATIONS.REQUIRED', {
      field: fieldname,
    });
  }
}
