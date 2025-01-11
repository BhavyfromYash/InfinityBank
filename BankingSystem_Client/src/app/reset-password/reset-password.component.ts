// reset-password.component.ts
// import { Component } from '@angular/core';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { ResetpasswordService } from '../services/resetpassword.service';
// import { Router } from '@angular/router';

// @Component({
//   selector: 'app-reset-password',
//   templateUrl: './reset-password.component.html',
//   styleUrls: ['./reset-password.component.css']
// })
// export class ResetPasswordComponent {
//   resetPasswordForm: FormGroup;
//   submitted = false;
//   alertMessage: string | null = null;
//   isSuccess: boolean = false;
//   isLoading: boolean = false;

//   constructor(
//     private formBuilder: FormBuilder,
//     private resetPasswordService: ResetpasswordService,
//     private router: Router
//   ) {
//     this.resetPasswordForm = this.formBuilder.group({
//       email: ['', [Validators.required, Validators.email]],
//       password: ['', [Validators.required, Validators.minLength(6)]],
//       confirmPassword: ['', Validators.required]
//     }, {
//       validator: this.passwordMatchValidator
//     });
//   }

//   // Custom validator for password match
//   passwordMatchValidator(g: FormGroup) {
//     return g.get('password')?.value === g.get('confirmPassword')?.value
//       ? null : { 'mismatch': true };
//   }

//   // Getter for easy access to form fields
//   get f() {
//     return this.resetPasswordForm.controls;
//   }

//   onSubmit() {
//     this.submitted = true;
//     this.alertMessage = null;

//     if (this.resetPasswordForm.invalid) {
//       return;
//     }

//     this.isLoading = true;
//     const { email, password } = this.resetPasswordForm.value;

//     this.resetPasswordService.resetPassword(email, password).subscribe({
//       next: () => {
//         this.isSuccess = true;
//         this.alertMessage = 'Password reset successful! Redirecting to login...';
//         setTimeout(() => {
//           this.router.navigate(['/login']);
//         }, 2000);
//       },
//       error: (error) => {
//         this.isSuccess = false;
//         this.alertMessage = error.error || 'Password reset failed. Please try again.';
//       },
//       complete: () => {
//         this.isLoading = false;
//       }
//     });
//   }
// }

import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { ResetpasswordService } from '../services/resetpassword.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {
  resetPasswordForm: FormGroup;
  submitted: boolean = false;
  alertMessage: string | null = null;
  isSuccess: boolean = false;
  isLoading: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private resetPasswordService: ResetpasswordService,
    private router: Router
  ) {
    this.resetPasswordForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8), this.passwordValidator]],
      confirmPassword: ['', [Validators.required]]
    }, {
      validator: this.passwordMatchValidator
    });
  }

  // Custom validator for password
  passwordValidator(control: AbstractControl): { [key: string]: any } | null {
    const password = control.value;
    if (!password) return null;

    const hasUpperCase = /[A-Z]/.test(password);
    const hasLowerCase = /[a-z]/.test(password);
    const hasNumeric = /[0-9]/.test(password);
    const hasSpecial = /[!@#\$%\^\&*\)\(+=._-]/.test(password);
    const validPassword = 
        password.length >= 8 && 
        hasUpperCase && 
        hasLowerCase && 
        hasNumeric && 
        hasSpecial;

    return validPassword ? null : { invalidPassword: true };
  }

  // Password match validator
  passwordMatchValidator(group: FormGroup): { mismatch: boolean } | null {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;

    return password === confirmPassword ? null : { mismatch: true };
  }

  // Get easy access to form fields
  get f() {
    return this.resetPasswordForm.controls;
  }

  // Methods to trigger validation while typing
  onPasswordChange() {
    this.f['password'].updateValueAndValidity();
  }

  onConfirmPasswordChange() {
    this.f['confirmPassword'].updateValueAndValidity();
  }

  onSubmit() {
    this.submitted = true;
    this.alertMessage = null;

    if (this.resetPasswordForm.invalid) {
      return;
    }

    this.isLoading = true;
    const { email, password } = this.resetPasswordForm.value;

    this.resetPasswordService.resetPassword(email, password).subscribe({
      next: () => {
        this.isSuccess = true;
        this.alertMessage = 'Password reset successful! Redirecting to login...';
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 2000);
      },
      error: (error) => {
        this.isSuccess = false;
        this.alertMessage = error.error || 'Password reset failed. Please try again.';
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }
}
