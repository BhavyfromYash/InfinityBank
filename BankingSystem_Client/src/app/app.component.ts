// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-root',
//   templateUrl: './app.component.html',
//   styleUrls: ['./app.component.css']
// })
// export class AppComponent {
//   title = 'BankingSystem_Client';
// }
import { Component, OnInit } from '@angular/core';
import { SessionService } from './services/session.service';
import { HttpErrorResponse } from '@angular/common/http';

declare var bootstrap: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  popupMessage: string | undefined;

  constructor(private sessionService: SessionService) { }

  ngOnInit(): void {
    setInterval(() => {
      this.checkSessionExpiration();
    }, 180000); // Check every 3 minutes
  }

  checkSessionExpiration(): void {
    this.sessionService.checkSessionExpired().subscribe(
      response => {
        if (response.suggestion === "Your session has been expired. Login again.") {
          this.popupMessage = response.suggestion;
          this.showPopup();
        }
      },
      (error: HttpErrorResponse) => {
        if (error.status === 401) { // Handling Unauthorized status code
          this.popupMessage = "Session is not active.Login Again.";
          this.showPopup();
        }
      }
    );
  }

  private showPopup(): void {
    const modalElement = document.getElementById('sessionExpiredModal');
    if (modalElement) {
      const modal = new bootstrap.Modal(modalElement);
      modal.show();
    } else {
      console.error('Modal element not found');
    }
  }
}
