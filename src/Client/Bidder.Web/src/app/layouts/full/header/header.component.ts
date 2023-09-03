import {
  Component,
  Output,
  EventEmitter,
  Input,
  ViewEncapsulation,
} from '@angular/core'; 
import { DialogService, DialogSize } from 'src/app/bidder.common/common.services/dialog.service';
import { BiddingPupComponent } from 'src/app/pages/bidding/bidding-pup/bidding-pup.component'; 


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

  constructor(public dialog: DialogService) {} 

  openDialog() { 
    this.dialog.openDialog(BiddingPupComponent, DialogSize.FullLarge)
  }
}
