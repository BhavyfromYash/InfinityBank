import { Component } from '@angular/core';
import { Customer } from '../Models/Customer';
import { ManagerService } from '../services/manager.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent {
  customers: Customer[] = []; constructor(private managerService: ManagerService) { } ngOnInit(): void {
    this.managerService.getCustomers().subscribe(data => { this.customers = data; });

  }
}
