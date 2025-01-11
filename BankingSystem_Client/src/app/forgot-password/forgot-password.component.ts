// import { Component, OnInit } from '@angular/core';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { ForgotpasswordService } from '../services/forgotpassword.service';

// @Component({
//   selector: 'app-forgot-password',
//   templateUrl: './forgot-password.component.html',
//   styleUrls: ['./forgot-password.component.css']
// })
// export class ForgotPasswordComponent implements OnInit {
//   forgotPasswordForm: FormGroup;
//   submitted = false;
//   alertMessage: string = '';
//   isSuccess: boolean = false;
//   isLoading: boolean = false;

//   constructor(
//     private formBuilder: FormBuilder,
//     private forgotPasswordService: ForgotpasswordService
//   ) {
//     this.forgotPasswordForm = this.formBuilder.group({
//       email: ['', [Validators.required, Validators.email]]
//     });
//   }

//   ngOnInit(): void {}

//   // Getter for easy access to form fields
//   get f() { 
//     return this.forgotPasswordForm.controls; 
//   }

//   onSubmit() {
//     this.submitted = true;
//     this.alertMessage = '';

//     if (this.forgotPasswordForm.invalid) {
//       return;
//     }

//     this.isLoading = true;
//     const email = this.forgotPasswordForm.get('email')?.value;

//     this.forgotPasswordService.sendResetLink(email).subscribe({
//       next: (response) => {
//         this.isSuccess = true;
//         this.alertMessage = 'Forgot Password has been done.Now Reset Password!';
//         this.submitted = false;
//         this.forgotPasswordForm.reset();
//       },
//       error: (error) => {
//         this.isSuccess = false;
//         this.alertMessage = error.error?.message || 'Failed to send reset link. Please try again.';
//       },
//       complete: () => {
//         this.isLoading = false;
//       }
//     });
//   }
// }

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ForgotpasswordService } from '../services/forgotpassword.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  forgotPasswordForm: FormGroup;
  submitted = false;
  alertMessage: string = '';
  isSuccess: boolean = false;
  isLoading: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private forgotPasswordService: ForgotpasswordService,
    private router: Router
  ) {
    this.forgotPasswordForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit(): void {}

  // Getter for easy access to form fields
  get f() { 
    return this.forgotPasswordForm.controls; 
  }

  onSubmit() {
    this.submitted = true;
    this.alertMessage = '';

    if (this.forgotPasswordForm.invalid) {
      return;
    }

    this.isLoading = true;
    const email = this.forgotPasswordForm.get('email')?.value;

    this.forgotPasswordService.sendResetLink(email).subscribe({
      next: (response) => {
        this.isSuccess = true;
        this.alertMessage = 'Forgot Password has been done. Now reset your password!';
        this.submitted = false;
        this.forgotPasswordForm.reset();
        setTimeout(() => {
          this.router.navigate(['/reset-password']);  // Navigate to reset password page after a delay
        }, 2000);  // Adjust the delay as needed
      },
      error: (error) => {
        this.isSuccess = false;
        this.alertMessage = error.error?.message || 'Failed to send reset link. Please try again.';
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }
}
