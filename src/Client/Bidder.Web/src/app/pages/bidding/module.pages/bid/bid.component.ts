import { Component, OnInit } from '@angular/core'; 
import { ActivatedRoute, Router } from '@angular/router';
import { SignalRService } from 'src/app/bidder.common/common.services/Utilities/SignalRService/signalr.service';
import { ToasterService } from 'src/app/bidder.common/common.services/Utilities/ToasterService/toaster.service';
import { environment } from 'src/environment/environment';
import { JoinResponse } from '../../module.objects/Responses/SignalR/JoinResponse.model';
import { HttpStatusCode } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-bid',
  templateUrl: './bid.component.html'
})
export class BidComponent implements OnInit {

  BidId: string = '';
  constructor( 
    private readonly activetedRoute : ActivatedRoute,
    private readonly route: Router,
    private readonly toast: ToasterService,
    private readonly signalr: SignalRService,
    private readonly translate: TranslateService,
  ) { }
  env = environment;
  signalrConnection: signalR.HubConnection;
  errorString: string = this.translate.instant('ERROR_CODES.GENERAL.500');
  successString: string = this.translate.instant('GENERAL.SUCCESS');
  async ngOnInit(): Promise<void> {
    this.BidId = this.activetedRoute.snapshot.paramMap.get('id')?.toString() ?? '';
    if(this.BidId === ''|| this.BidId === null || this.BidId === undefined || this.BidId.length !== 36)this.route.navigate(['/']);      
    this.signalrConnection = await this.signalr.buildConnection(this.env.bid_hub);  
    var response = await this.signalr.Invoke<JoinResponse>('Join',this.signalrConnection, this.BidId).catch((error: Error) => {
      this.toast.openToastError(this.errorString, error.message);
      return null;
    });
    if(response != null &&  response.StatusCode === HttpStatusCode.Ok){
      console.log(response.ConnectionId);
      this.toast.openToastSuccess(this.successString, this.translate.instant(`BID.SUCCESS.${response?.Message}`));
    }else if(response != null &&  response.StatusCode !== HttpStatusCode.Ok){
      this.toast.openToastError(this.errorString, this.translate.instant(`BID.ERRORS.${response?.Message}`));
      }
    else
    { 
      this.toast.openToastError(this.errorString, this.translate.instant(`BID.ERRORS.CONNECTION_FAILED`));
      this.route.navigate(['/']);
    }     
  }

  onJoinError(error: Error){ 
    this.toast.openToastError(this.errorString, error.message);
    this.route.navigate(['/']);
  }


  public ngOnDestroy() {
    console.log('page closed!');
  }

  public async buildConnection(): Promise<void> {
    
  }

}
 