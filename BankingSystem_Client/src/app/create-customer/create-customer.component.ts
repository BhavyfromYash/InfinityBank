import { Component } from '@angular/core';
import { CustomerCreationModel } from '../Models/CustomerCreationModel';
import { CustomerService } from '../services/customer.service';

@Component({
  selector: 'app-create-customer',
  templateUrl: './create-customer.component.html',
  styleUrls: ['./create-customer.component.css']
})
export class CreateCustomerComponent {
  customer: CustomerCreationModel = {} as CustomerCreationModel; // Initialize as an empty object


  userId: number | null = null;
  userExists: boolean = false;

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
    const userIdString = sessionStorage.getItem('UserId');
    console.log('UserId retrieved from session:', userIdString);

    if (userIdString) {
      this.userId = +userIdString;
      this.checkUserExists(this.userId);
    } else {
      console.error('UserId not found in session');
    }
  }

  checkUserExists(userId: number): void {
    this.customerService.isUserExists(userId).subscribe(
      (exists) => {
        this.userExists = exists;
        if (!exists) {
          console.error('User does not exist');
        }
      },
      (error) => {
        console.error('Error checking if user exists:', error);
      }
    );
  }

  onSubmit() {
    if (this.userId !== null && this.userExists) {
      this.customerService.createCustomer(this.userId, this.customer)
        .subscribe(response => {
          console.log('Customer created successfully!', response);
        }, error => {
          console.error('Error creating customer:', error);
        });
    } else {
      console.error('UserId is not available or user does not exist');
    }
  }
}
