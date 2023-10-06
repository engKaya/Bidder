import { Injectable } from '@angular/core';
import { Params } from '@angular/router';
import * as signalR from '@microsoft/signalr';
import { Arg } from '@microsoft/signalr/dist/esm/Utils';
import { AuthLoginService } from 'src/app/pages/authentication/module.services/auth.service';
import { environment } from 'src/environment/environment';
import { ToasterService } from '../ToasterService/toaster.service';
import { Exception } from 'sass';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  env = environment;

  constructor(
    private readonly authservice: AuthLoginService,
    private readonly toast: ToasterService
    ) {}

  public async buildConnection(
    hubUrl: string,
    callBackFunction: Function | null= null
  ): Promise<signalR.HubConnection> {
    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withHubProtocol(new signalR.JsonHubProtocol())
      .withAutomaticReconnect()
      .withUrl(hubUrl, {
        accessTokenFactory: () => {
          return this.authservice.getToken();
        },
        transport: signalR.HttpTransportType.WebSockets,
      })
      .build();
    await connection
      .start()
      .then((res) => {
        if (this.env.isDevMode) console.log('SignalR Connected!' + res);
        if (callBackFunction !== null) callBackFunction();
      })
      .catch((error: any) => {
        if (this.env.isDevMode)
          console.log('SignalR connection error: ' + error);
      });
    return connection;
  }

  public async Invoke<T>(methodname: string,connection: signalR.HubConnection,  ...args: any) : Promise<T> {
    return await connection.invoke<T>(methodname,...args).catch((error: Error) => { 
        if (this.env.isDevMode) this.toast.openToastError(`Error on ${methodname}`, JSON.stringify(error)); 
        throw error;
    }) as T;
  }

  
  public async InvokeWithoutType(methodname: string,connection: signalR.HubConnection, ...args: any) : Promise<void> {
        return await connection.invoke(methodname, args).catch((error: Error) => {
            if (this.env.isDevMode) this.toast.openToastError(`Error on ${methodname}`, JSON.stringify(error));
            throw error;
        });
  }

  public async OnMethod(connection: signalR.HubConnection, methodname: string, callBackFunction : (...args:any[]) => any) : Promise<void> {
    await connection.on(methodname,callBackFunction);
  }

  
}
