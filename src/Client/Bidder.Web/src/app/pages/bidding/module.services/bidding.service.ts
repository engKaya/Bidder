import { HttpClient, HttpStatusCode } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject, Observable, lastValueFrom, throwError } from 'rxjs';
import { LocalStorageService } from 'src/app/bidder.common/common.services/Utilities/LocalStorageService/localstorage.service';
import { ToasterService } from 'src/app/bidder.common/common.services/Utilities/ToasterService/toaster.service';
import { CreateBidRequest } from '../module.objects/Requests/CreateBidRequest.request';
import { ResponseMessage } from 'src/app/bidder.common/common.objects/ResponseMessage.model'; 
import { environment } from 'src/environment/environment';
import { CreateBidResponse } from '../module.objects/Responses/Http/CreateBidResponse.response';

@Injectable({
  providedIn: 'root',
})
export class BiddingService {
  apiUrl = environment.bid_server;
  isDevMode = environment.isDevMode;
  IsLoadingSubject: BehaviorSubject<boolean>;
  IsLoading$: Observable<boolean>;
  constructor(
    private http: HttpClient,
    private localStorage: LocalStorageService,
    private router: Router,
    private toastr: ToasterService,
    private translate: TranslateService
  ) {
    this.IsLoadingSubject = new BehaviorSubject<boolean>(false);
    this.IsLoading$ = this.IsLoadingSubject.asObservable();
  }

  CreateBid(req : CreateBidRequest): Promise<ResponseMessage<CreateBidResponse>> {
    this.IsLoadingSubject.next(true);
    const url = `${this.apiUrl}Bid/CreateBid`; 
    return lastValueFrom(
      this.http.post<ResponseMessage<CreateBidResponse>>(url, req)
    ).then(async (response: ResponseMessage<CreateBidResponse>) => {
      if (response.StatusCode === HttpStatusCode.Ok)
        return response;
      else { 
        this.toastr.openToastError(this.translate.instant(`ERROR_CODES.GENERAL.${response.StatusCode}`), this.translate.instant(`BID.ERRORS.${response.Message}`));
        throw response;
      } 
    }).finally(() => {
      this.IsLoadingSubject.next(false);
    }).catch((error : any) => { 
      if (this.isDevMode && error.status == 500) this.handleError(error);
      throw error;
    });
  }

  private handleError(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    this.toastr.openToastError(this.translate.instant("ERROR_CODES.GENERAL_ERROR_EXP.500"), this.translate.instant("ERROR_CODES.GENERAL.500"));
    return throwError(() => {
      return errorMessage;
    });
  } 
}
