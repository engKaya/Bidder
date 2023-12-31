import { Injectable, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CreateBidRequest } from '../module.objects/Requests/CreateBidRequest.request';
import { ResponseMessage } from 'src/app/bidder.common/common.objects/ResponseMessage.model';
import { CreateBidResponse } from '../module.objects/Responses/Http/CreateBidResponse.response';
import { BidderRestService } from 'src/app/bidder.common/common.services/Utilities/HttpService/bidderRestService.service';

@Injectable({
  providedIn: 'root',
})
export class BiddingService {
  IsLoadingSubject: BehaviorSubject<boolean>;
  IsLoading$: Observable<boolean>;
  constructor(private restHelper: BidderRestService) {
    this.IsLoadingSubject = new BehaviorSubject<boolean>(false);
    this.IsLoading$ = this.IsLoadingSubject.asObservable();
  }

  CreateBid(
    req: CreateBidRequest
  ): Promise<ResponseMessage<CreateBidResponse>> {
    return this.restHelper.Post<CreateBidResponse, CreateBidRequest>(
      `Bid/CreateBid`,
      req
    );
  }
}
