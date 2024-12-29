// import { Component, OnInit } from '@angular/core';
// import { ManagerService } from '../services/manager.service';
// import { Customer } from '../Models/Customer';

// @Component({
//   selector: 'app-approval-list',
//   templateUrl: './approval-list.component.html',
//   styleUrls: ['./approval-list.component.css']
// })
// export class ApprovalListComponent implements OnInit {
//   pendingApprovals: Customer[] = [];

//   constructor(private managerService: ManagerService) { }

//   ngOnInit(): void {
//     this.loadPendingApprovals();
//   }

//   loadPendingApprovals(): void {
//     this.managerService.getPendingApprovals().subscribe(data => {
//       this.pendingApprovals = data;
//     });
//   }

//   approve(customerId: number): void {
//     this.managerService.approveCustomer(customerId).subscribe(updatedCustomer => {
//       const index = this.pendingApprovals.findIndex(c => c.cusId === customerId);
//       if (index !== -1) {
//         this.pendingApprovals[index].status = 'Approved';
//         // Remove the customer from the pending list
//         this.pendingApprovals = this.pendingApprovals.filter(c => c.cusId !== customerId);
//       }
//     });
//   }

//   reject(customerId: number): void {
//     this.managerService.rejectCustomer(customerId).subscribe(updatedCustomer => {
//       const index = this.pendingApprovals.findIndex(c => c.cusId === customerId);
//       if (index !== -1) {
//         this.pendingApprovals[index].status = 'Rejected';
//         // Remove the customer from the pending list
//         this.pendingApprovals = this.pendingApprovals.filter(c => c.cusId !== customerId);
//       }
//     });
//   }
// }

import { Component, OnInit } from '@angular/core';
import { ManagerService } from '../services/manager.service';
import { Customer } from '../Models/Customer';
import * as bootstrap from 'bootstrap';
import { Router } from '@angular/router';

@Component({
  selector: 'app-approval-list',
  templateUrl: './approval-list.component.html',
  styleUrls: ['./approval-list.component.css']
})
export class ApprovalListComponent implements OnInit {
  pendingApprovals: Customer[] = [];

  constructor(private managerService: ManagerService, private router: Router) { }

  ngOnInit(): void {
    this.loadPendingApprovals();
  }

  loadPendingApprovals(): void {
    this.managerService.getPendingApprovals().subscribe(data => {
      this.pendingApprovals = data;
    });
  }

  approve(customerId: number): void {
    this.managerService.approveCustomer(customerId).subscribe(updatedCustomer => {
      this.showApproveSuccessModal(customerId);
    });
  }

  reject(customerId: number): void {
    this.managerService.rejectCustomer(customerId).subscribe(updatedCustomer => {
      this.showRejectSuccessModal(customerId);
    });
  }

  showApproveSuccessModal(customerId: number): void {
    const modalElement = document.getElementById('approveSuccessModal');
    if (modalElement) {
      const modal = new bootstrap.Modal(modalElement);
      modal.show();
      modalElement.addEventListener('hidden.bs.modal', () => {
        this.updateCustomerStatus(customerId, 'Approved');
        this.router.navigate(['/customer-list']);
      }, { once: true });
    }
  }

  showRejectSuccessModal(customerId: number): void {
    const modalElement = document.getElementById('rejectSuccessModal');
    if (modalElement) {
      const modal = new bootstrap.Modal(modalElement);
      modal.show();
      modalElement.addEventListener('hidden.bs.modal', () => {
        this.updateCustomerStatus(customerId, 'Rejected');
        this.router.navigate(['/customer-list']);
      }, { once: true });
    }
  }

  updateCustomerStatus(customerId: number, status: string): void {
    const index = this.pendingApprovals.findIndex(c => c.cusId === customerId);
    if (index !== -1) {
      this.pendingApprovals[index].status = status;
      // Remove the customer from the pending list
      this.pendingApprovals = this.pendingApprovals.filter(c => c.cusId !== customerId);
    }
  }

  onModalClose(action: string): void {
    const modalElement = document.getElementById(action === 'approve' ? 'approveSuccessModal' : 'rejectSuccessModal');
    if (modalElement) {
      const modal = bootstrap.Modal.getInstance(modalElement);
      if (modal) {
        modal.hide();
      }
    }
  }
}
