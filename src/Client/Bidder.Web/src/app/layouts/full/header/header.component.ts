import {
  Component,
  Output,
  EventEmitter,
  Input,
  ViewEncapsulation,
} from '@angular/core'; 
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { PubSubService } from 'src/app/bidder.common/common.services/PubSubService/PubSub.service';
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


  CloseBidDialog : Observable<boolean>;
  showFiller = false;
  bidDialogRef: MatDialogRef<BiddingPupComponent> | undefined;
  constructor(
    public dialog: DialogService,
    public auth: AuthLoginService,
    public pubsub: PubSubService
  ) { 
    this.CloseBidDialog = this.pubsub.CloseBidDialog$;
    this.CloseBidDialog.subscribe((response) => { 
        this.bidDialogRef?.close();
    });
  } 

  

  openDialog() { 
    this.bidDialogRef =  this.dialog.openDialog(BiddingPupComponent, DialogSize.FullLarge)
  }

  logout() {
    this.auth.logout();
  }
}
