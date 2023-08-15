import { ChangeDetectorRef, Component } from '@angular/core';
import { Observable, lastValueFrom, of } from 'rxjs';
import { AuthLoginService } from 'src/app/modules/auth/services/auth-login.service'; 

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: [],
})
export class NavbarComponent {
  IsLogged: boolean = false;
  userName: string = '';

  constructor(
    private authLoginService: AuthLoginService, 
    private ref: ChangeDetectorRef,
    
  ) { 
    this.authLoginService.UserName$.subscribe((userName: string) => {
      this.userName = userName;
      this.ref.detectChanges();
    });
 
  }

  ngOnInit(): void { 
  }

  cartCount : number = 0; 
}
