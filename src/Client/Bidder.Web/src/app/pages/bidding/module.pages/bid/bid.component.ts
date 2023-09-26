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

  async ngOnInit(): Promise<void> {
    this.signalrConnection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withHubProtocol(new signalR.JsonHubProtocol())
      .withUrl(this.env.bid_hub)
      .build();

    await this.signalrConnection.start().then((res) => {
      console.log('SignalR Connected!' + res);   
      this.signalrConnection.invoke("Join").then(() => {
  
      }).catch((error:any) => {
  
      });    
    }).catch((error:any) => {
      console.log('SignalR connection error: ' + error);
    });
  }

  public ngOnDestroy() {
    console.log('page closed!');
  }

}
 