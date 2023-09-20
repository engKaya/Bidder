import { Injectable, OnInit } from '@angular/core'; 
import { Subject } from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PubSubService {
    
    public CloseBidDialog$ : Observable<any> = new Observable<any>();
    public CloseBidDialogSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);


    constructor() { 
        this.CloseBidDialog$ = this.CloseBidDialogSubject.asObservable();
    }  
 
}