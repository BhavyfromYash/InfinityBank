import { Component } from '@angular/core';

@Component({
  selector: 'app-main-navbar',
  templateUrl: './main-navbar.component.html',
  styleUrls: ['./main-navbar.component.css']
})
export class MainNavbarComponent {
isOpen = false; toggleNav() { this.isOpen = !this.isOpen; } closeNav() { this.isOpen = false; }
}
