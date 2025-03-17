import { HttpClient, HttpStatusCode } from '@angular/common/http';
import { Injectable, OnInit, Type } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject, Observable, lastValueFrom, throwError } from 'rxjs';
import { ResponseMessage } from 'src/app/bidder.common/common.objects/ResponseMessage.model';
import { ToasterService } from 'src/app/bidder.common/common.services/Utilities/ToasterService/toaster.service';
import { CreateBidResponse } from 'src/app/pages/bidding/module.objects/Responses/Http/CreateBidResponse.response';
import { environment } from 'src/environment/environment';

@Injectable({
  providedIn: 'root',
})
export class BidderRestService {
  apiUrl = environment.api_gateway;
  isDevMode = environment.isDevMode;
  IsLoadingSubject: BehaviorSubject<boolean>;
  IsLoading$: Observable<boolean>;
  constructor(
    private http: HttpClient,
    private toastr: ToasterService,
    private translate: TranslateService
  ) {
    this.IsLoadingSubject = new BehaviorSubject<boolean>(false);
    this.IsLoading$ = this.IsLoadingSubject.asObservable();
  }

  /**
   * Generic Get method to be used for all post requests
   * @type TypeResponse : the type of the response
   * @param url         : the url of the request
   * @param data        : the request body
   * @param IsLoadingSubject : the loading subject to be updated
   * @param callback    : the callback function to be called on success
   * @param onFinally   : the function to be called on finally
   * @param onCatch     : the function to be called on catch
   * @returns           : Promise<ResponseMessage<@type TypeResponse>>
   */
  public Get<Type>(
    url: string,
    IsLoadingSubject?: BehaviorSubject<boolean>,
    callback?: (response: ResponseMessage<Type>) => void,
    onFinally?: () => void,
    onCatch?: (error: any) => void
  ): Promise<ResponseMessage<Type>> {
    if (IsLoadingSubject) IsLoadingSubject.next(true);
    else this.IsLoadingSubject.next(true);
    return lastValueFrom(
      this.http.get<ResponseMessage<Type>>(this.apiUrl + url)
    )
      .then(async (response: ResponseMessage<Type>) => {
        if (response.StatusCode === HttpStatusCode.Ok) {
          if (callback) callback(response);
          return response;
        } else {
          this.toastr.openToastError(
            this.translate.instant(
              `ERROR_CODES.GENERAL.${response.StatusCode}`
            ),
            this.translate.instant(`BID.ERRORS.${response.Message}`)
          );
          throw response;
        }
      })
      .finally(() => {
        if (onFinally) onFinally();
        if (IsLoadingSubject) IsLoadingSubject.next(false);
        else this.IsLoadingSubject.next(false);
      })
      .catch((error: any) => {
        if (onCatch) onCatch(error);
        if (this.isDevMode && error.status == 500) this.handleError(error);
        throw error;
      });
  }
  /**
   * Generic Post method to be used for all post requests
   * @type TypeResponse : the type of the response
   * @type TypeRequest  : the type of the request body
   * @param url         : the url of the request
   * @param data        : the request body
   * @param IsLoadingSubject : the loading subject to be updated
   * @param callback    : the callback function to be called on success
   * @param onFinally   : the function to be called on finally
   * @param onCatch     : the function to be called on catch
   * @returns           : Promise<ResponseMessage<@type TypeResponse>>
   */
  public Post<TypeResponse, TypeRequest>(
    url: string,
    data: TypeRequest,
    IsLoadingSubject?: BehaviorSubject<boolean>,
    callback?: (response: ResponseMessage<TypeResponse>) => void,
    onFinally?: () => void,
    onCatch?: (error: any) => void
  ): Promise<ResponseMessage<TypeResponse>> {
    if (IsLoadingSubject) IsLoadingSubject.next(true);
    else this.IsLoadingSubject.next(true);
    return lastValueFrom(
      this.http.post<ResponseMessage<TypeResponse>>(this.apiUrl + url, data)
    )
      .then(async (response: ResponseMessage<TypeResponse>) => {
        if (response.StatusCode == HttpStatusCode.Ok) {
          if (callback) callback(response);
          return response;
        } else {
          this.toastr.openToastError(
            this.translate.instant(
              `ERROR_CODES.GENERAL.${response.StatusCode}`
            ),
            this.translate.instant(`BID.ERRORS.${response.Message}`)
          );
          throw response;
        }
      })
      .finally(() => {
        if (onFinally) onFinally();
        if (IsLoadingSubject) IsLoadingSubject.next(false);
        else this.IsLoadingSubject.next(false);
      })
      .catch((error: any) => {
        if (onCatch) onCatch(error);
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
    this.toastr.openToastError(
      this.translate.instant('ERROR_CODES.GENERAL_ERROR_EXP.500'),
      this.translate.instant('ERROR_CODES.GENERAL.500')
    );
    return throwError(() => {
      return errorMessage;
    });
  }
}
