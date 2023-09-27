import { Component, OnInit } from '@angular/core'; 
import { ActivatedRoute, Router } from '@angular/router';
import { SignalRService } from 'src/app/bidder.common/common.services/Utilities/SignalRService/signalr.service';
import { ToasterService } from 'src/app/bidder.common/common.services/Utilities/ToasterService/toaster.service';
import { environment } from 'src/environment/environment';
import { JoinResponse } from '../../module.objects/Responses/SignalR/JoinResponse.model';
import { HttpStatusCode } from '@angular/common/http';

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
    private readonly signalr: SignalRService
  ) { }
  env = environment;
  signalrConnection: signalR.HubConnection;

  async ngOnInit(): Promise<void> {
    this.BidId = this.activetedRoute.snapshot.paramMap.get('id')?.toString() ?? '';
    if(this.BidId === ''|| this.BidId === null || this.BidId === undefined || this.BidId.length !== 36){
      this.toast.openToastError('Error','Bid is empty!');
      this.route.navigate(['/']);
    }  
    this.signalrConnection = await this.signalr.buildConnection(this.env.bid_hub);  
    var response = await this.signalr.Invoke<JoinResponse>('Join',this.signalrConnection, this.BidId).catch((error: Error) => {
      console.log(error.message);
      return null;
    });
    if(response != null &&  response.StatusCode === HttpStatusCode.Ok){
      console.log(response.ConnectionId);
      this.toast.openToastSuccess('Success', response.Message);
    }else
    {
      this.toast.openToastError('Error', "Error on Join");
      this.route.navigate(['/']);
    }     
  }

  onJoinError(error: Error){ 
    this.toast.openToastError('Error', error.message);
    this.route.navigate(['/']);
  }


  public ngOnDestroy() {
    console.log('page closed!');
  }

  public async buildConnection(): Promise<void> {
    
  }

}
 