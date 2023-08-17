import { Component } from '@angular/core';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class AppDashboardComponent {

  constructor() { 
  }

  ngOnInit() {
    console.log('Dashboard');
   }
}
