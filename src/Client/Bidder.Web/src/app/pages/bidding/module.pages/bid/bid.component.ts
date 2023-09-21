import { Component, OnInit } from '@angular/core'; 
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environment/environment';

@Component({
  selector: 'app-bid',
  templateUrl: './bid.component.html'
})
export class BidComponent implements OnInit {

  constructor( 
  ) { }
  env = environment;
  signalrConnection: signalR.HubConnection;

  ngOnInit(): void {
    this.signalrConnection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withHubProtocol(new signalR.JsonHubProtocol())
      .withUrl(this.env.bid_hub)
      .build();

    this.signalrConnection.start().then(() => {
      console.log('SignalR Connected!');      
    }).catch((error:any) => {
      console.log('SignalR connection error: ' + error);
    });
  }

  public ngOnDestroy() {
    console.log('page closed!');
  }

}
 