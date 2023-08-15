import { ChangeDetectorRef, Component } from '@angular/core'; 
import { Observable } from 'rxjs';
import { environment } from 'src/enviroment/enviroment';
import { PaginatedViewModel } from 'src/app/common-objects/PaginatedViewModel.model'; 

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent {
   
 
 
  constructor(  
    private ref : ChangeDetectorRef
  ) { 
  }

  ngOnInit() { 
  } 
}
