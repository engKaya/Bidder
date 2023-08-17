import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'Modernize Angular Admin Tempplate';

  constructor(
    translate: TranslateService 
  ) {
      translate.addLangs(['en', 'tr']);
      translate.setDefaultLang('tr');
      const browserLang = translate.getBrowserLang() || 'tr'; 
      translate.use((browserLang.match(/en|tr/) ? browserLang : 'tr'));
    }
}
