
import {   ActivatedRouteSnapshot,  RouterStateSnapshot,  Router, UrlTree} from "@angular/router";
import { Injectable } from "@angular/core"; 
import { Observable } from "rxjs";
import { AuthLoginService } from "src/app/pages/authentication/module.services/auth.service";
import { ToasterService } from "../Utilities/ToasterService/toaster.service";

@Injectable({
    providedIn: 'root'
})
export class AuthGuard {

  constructor(
    private authService: AuthLoginService, 
    private toastr: ToasterService, 
    private router: Router) { }

    canActivate(state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {   
      if (this.authService.isLoggedIn()) {
        return true;
      } else {
        var returnUrl = "";
        for (const iterator of state.url) {
            returnUrl += returnUrl == "" ? iterator :  "%2F" + iterator; 
        } 
        this.toastr.openToastError("Not Authorized","You are not logged in");
        this.router.navigate([`/auth/login`], { queryParams: { returnUrl: returnUrl } });
        return false;
      }
    }

}