import { Injectable, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CreateBidRequest } from '../module.objects/Requests/CreateBidRequest.request';
import { ResponseMessage } from 'src/app/bidder.common/common.objects/ResponseMessage.model';
import { CreateBidResponse } from '../module.objects/Responses/Http/CreateBidResponse.response';
import { BidderRestService } from 'src/app/bidder.common/common.services/Utilities/HttpService/bidderRestService.service';
import { environment } from 'src/environment/environment';

@Injectable({
  providedIn: 'root',
})
export class BiddingService {
  IsLoadingSubject: BehaviorSubject<boolean>;
  IsLoading$: Observable<boolean>;
  bidServer: string;
  constructor(private restHelper: BidderRestService) {
    this.bidServer = environment.bid_server;
    this.IsLoadingSubject = new BehaviorSubject<boolean>(false);
    this.IsLoading$ = this.IsLoadingSubject.asObservable();
  }

  CreateBid(
    req: CreateBidRequest
  ): Promise<ResponseMessage<CreateBidResponse>> {
    return this.restHelper.Post<CreateBidResponse, CreateBidRequest>(
      `${this.bidServer}Bid/CreateBid`,
      req
    );
  }
}
