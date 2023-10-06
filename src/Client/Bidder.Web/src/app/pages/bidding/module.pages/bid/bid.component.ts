import { Component, OnInit } from '@angular/core'; 
import { ActivatedRoute, Router } from '@angular/router';
import { SignalRService } from 'src/app/bidder.common/common.services/Utilities/SignalRService/signalr.service';
import { ToasterService } from 'src/app/bidder.common/common.services/Utilities/ToasterService/toaster.service';
import { environment } from 'src/environment/environment';
import { JoinResponse } from '../../module.objects/Responses/SignalR/JoinResponse.model';
import { HttpStatusCode } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { AuthLoginService } from 'src/app/pages/authentication/module.services/auth.service';
import { HubConnectionState } from '@microsoft/signalr';

@Component({
  selector: 'app-bid',
  templateUrl: './bid.component.html'
})
export class BidComponent implements OnInit {
  env = environment;
  signalrConnection: signalR.HubConnection;

  BidId: string = '';
  ConnectionId: string  = '';

  errorString: string = this.translate.instant('ERROR_CODES.GENERAL.500');
  successString: string = this.translate.instant('GENERAL.SUCCESS');

  constructor( 
    private readonly activetedRoute : ActivatedRoute,
    private readonly route: Router,
    private readonly toast: ToasterService,
    private readonly signalr: SignalRService,
    private readonly translate: TranslateService,
    private readonly auth: AuthLoginService
  ) { }

  async ngOnInit(): Promise<void> {
    this.BidId = this.activetedRoute.snapshot.paramMap.get('id')?.toString() ?? '';
    if(this.BidId === ''|| this.BidId === null || this.BidId === undefined || this.BidId.length !== 36)this.route.navigate(['/']);

    await this.buildConnection();    
    await this.Join(); 
    this.ReceiveMessage(); 
  }

  onJoinError(error: Error){ 
    this.toast.openToastError(this.errorString, error.message);
    this.route.navigate(['/']);
  }


  public ngOnDestroy() {
    console.log('page closed!');
  }

  private async buildConnection(): Promise<void> { 
    this.signalrConnection = await this.signalr.buildConnection(this.env.bid_hub);   
    this.ConnectionId = this.signalrConnection.connectionId ?? '';
    return Promise.resolve();
  }

  private async Join(): Promise<void> {
    var response = await this.signalr.Invoke<JoinResponse>('Join',this.signalrConnection, this.BidId).catch((error: Error) => {
      this.toast.openToastError(this.errorString, error.message);
      return null;
    });
    if(response != null &&  response.StatusCode === HttpStatusCode.Ok)
      this.toast.openToastSuccess(this.successString, this.translate.instant(`BID.SUCCESS.${response?.Message}`));
    else if(response != null &&  response.StatusCode !== HttpStatusCode.Ok){
      this.toast.openToastError(this.errorString, this.translate.instant(`BID.ERRORS.${response?.Message}`));
      this.route.navigate(['/']);
      }
    else
    { 
      this.toast.openToastError(this.errorString, this.translate.instant(`BID.ERRORS.CONNECTION_FAILED`));
      this.route.navigate(['/']);
    }     
  }

  public async SendMessage(mes: string): Promise<void> { 
    this.signalr.Invoke('SendMessage',this.signalrConnection, this.BidId.toLocaleLowerCase(), this.ConnectionId, mes).catch((error: Error) => {
      this.toast.openToastError(this.errorString, error.message);
    });
  }

  public async ReceiveMessage(): Promise<void> { 
    this.signalr.OnMethod(this.signalrConnection, 'ReceiveMessage', (message: string) => {
      console.log(message);
    })
  }
}
 