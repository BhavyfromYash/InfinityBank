// import { Component, Input } from '@angular/core';

// @Component({
//   selector: 'app-sessionexpireddialog',
//   templateUrl: './sessionexpireddialog.component.html',
//   styleUrls: ['./sessionexpireddialog.component.css']
// })
// export class SessionexpireddialogComponent {
//   @Input() message: string | undefined;
// }
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sessionexpireddialog',
  templateUrl: './sessionexpireddialog.component.html',
  styleUrls: ['./sessionexpireddialog.component.css']
})
export class SessionExpiredPopupComponent {
  @Input() message: string | undefined;

  constructor(private router: Router) { }

  redirectToLogin(): void {
    // sessionStorage.removeItem("userRole");
    // sessionStorage.removeItem("loginToken");
    this.router.navigate(['/login']);
  }



}
