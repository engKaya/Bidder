import {
  Component,
  Output,
  EventEmitter,
  Input,
  ViewEncapsulation,
} from '@angular/core'; 
import { DialogService, DialogSize } from 'src/app/bidder.common/common.services/dialog.service';
import { AuthLoginService } from 'src/app/pages/authentication/module.services/auth.service';
import { BiddingPupComponent } from 'src/app/pages/bidding/module.components/bidding-pup/bidding-pup.component'; 


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class HeaderComponent {
  @Input() showToggle = true;
  @Input() toggleChecked = false;
  @Output() toggleMobileNav = new EventEmitter<void>();
  @Output() toggleMobileFilterNav = new EventEmitter<void>();
  @Output() toggleCollapsed = new EventEmitter<void>();

  showFiller = false;

  constructor(
    public dialog: DialogService,
    public auth: AuthLoginService  
  ) {} 

  openDialog() { 
    this.dialog.openDialog(BiddingPupComponent, DialogSize.FullLarge)
  }

  logout() {
    this.auth.logout();
  }
}
