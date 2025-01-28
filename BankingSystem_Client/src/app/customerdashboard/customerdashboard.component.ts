import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-customer-dashboard',
    templateUrl: './customerdashboard.component.html',
    styleUrls: ['./customerdashboard.component.css']
})
export class CustomerDashboardComponent {
    isDropdownOpen = false;
    isSidebarOpen = false;

    toggleDropdown() {
        this.isDropdownOpen = !this.isDropdownOpen;
    }

    toggleSidebar() {
        this.isSidebarOpen = !this.isSidebarOpen;
    }

}
