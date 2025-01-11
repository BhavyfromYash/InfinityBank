// import { Component, OnInit } from '@angular/core';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// @Component({
//   selector: 'app-register',
//   templateUrl: './register.component.html',
//   styleUrls: ['./register.component.css']
// })
// export class RegisterComponent implements OnInit {
//   registerForm: FormGroup;
//   submitted = false;
//   showOTPField = false;

//   constructor(private formBuilder: FormBuilder) {
//     this.registerForm = this.formBuilder.group({
//       name: ['', [Validators.required, Validators.minLength(3)]],
//       email: ['', [Validators.required, Validators.email]],
//       password: ['', [
//         Validators.required,
//         Validators.minLength(8),
//         Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)
//       ]],
//       confirmPassword: ['', Validators.required],
//       userRole: ['', Validators.required],
//       OTP: [null]
//     }, {
//       validator: this.passwordMatchValidator
//     });
//   }

//   ngOnInit(): void { }

//   // Custom validator for password matching
//   passwordMatchValidator(group: FormGroup) {
//     const password = group.get('password')?.value;
//     const confirmPassword = group.get('confirmPassword')?.value;

//     if (password && confirmPassword && password !== confirmPassword) {
//       group.get('confirmPassword')?.setErrors({ passwordMismatch: true });
//       return { passwordMismatch: true };
//     }
//     return null;
//   }

//   // Helper method to check for form control errors
//   getErrorMessage(controlName: string): string {
//     const control = this.registerForm.get(controlName);

//     if (control?.errors) {
//       if (control.errors['required']) return `${controlName} is required`;
//       if (control.errors['email']) return 'Invalid email address';
//       if (control.errors['minlength']) return `${controlName} must be at least ${control.errors['minlength'].requiredLength} characters`;
//       if (control.errors['pattern']) return 'Password must contain at least one uppercase letter, one lowercase letter, one number and one special character';
//       if (control.errors['passwordMismatch']) return 'Passwords do not match';
//     }
//     return '';
//   }

//   // Method to send OTP
//   sendOTP() {
//     if (this.registerForm.get('email')?.valid) {
//       // Implement OTP sending logic here
//       this.showOTPField = true;
//       // Mock OTP sent success message
//       console.log('OTP sent to email');
//     }
//   }

//   // Form submission handler
//   onSubmit() {
//     this.submitted = true;

//     if (this.registerForm.valid) {
//       console.log('Form submitted:', this.registerForm.value);
//       // Implement your registration logic here
//     } else {
//       // Mark all fields as touched to trigger validation messages
//       Object.keys(this.registerForm.controls).forEach(key => {
//         const control = this.registerForm.get(key);
//         control?.markAsTouched();
//       });
//     }
//   }
// }


// import { Component, OnInit } from '@angular/core';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { RegisterService } from '../services/register.service';
// import { Modal } from 'bootstrap';

// @Component({
//   selector: 'app-register',
//   templateUrl: './register.component.html',
//   styleUrls: ['./register.component.css']
// })
// export class RegisterComponent implements OnInit {
//   registerForm: FormGroup;
//   submitted = false;
//   showPassword = false;

//   constructor(private formBuilder: FormBuilder, private registerService: RegisterService) {
//     this.registerForm = this.formBuilder.group({
//       name: ['', [Validators.required, Validators.minLength(3)]],
//       email: ['', [Validators.required, Validators.email]],
//       password: ['', [
//         Validators.required,
//         Validators.minLength(8),
//         Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)
//       ]],
//       confirmPassword: ['', Validators.required],
//       userRole: ['', Validators.required]
//     }, {
//       validator: this.passwordMatchValidator
//     });
//   }

//   ngOnInit(): void { }

//   // Custom validator for password matching
//   passwordMatchValidator(group: FormGroup) {
//     const password = group.get('password')?.value;
//     const confirmPassword = group.get('confirmPassword')?.value;

//     if (password && confirmPassword && password !== confirmPassword) {
//       group.get('confirmPassword')?.setErrors({ passwordMismatch: true });
//       return { passwordMismatch: true };
//     }
//     return null;
//   }

//   // Helper method to check for form control errors
//   getErrorMessage(controlName: string): string {
//     const control = this.registerForm.get(controlName);

//     if (control?.errors) {
//       if (control.errors['required']) return `${controlName} is required`;
//       if (control.errors['email']) return 'Invalid email address';
//       if (control.errors['minlength']) return `${controlName} must be at least ${control.errors['minlength'].requiredLength} characters`;
//       if (control.errors['pattern']) return 'Password must contain at least one uppercase letter, one lowercase letter, one number and one special character';
//       if (control.errors['passwordMismatch']) return 'Passwords do not match';
//     }
//     return '';
//   }

//   // Method to toggle password visibility
//   togglePasswordVisibility(): void {
//     this.showPassword = !this.showPassword;
//   }

//   // // Form submission handler
//   // onSubmit() {
//   //   this.submitted = true;

//   //   if (this.registerForm.valid) {
//   //     this.registerService.register(this.registerForm.value).subscribe(response => {
//   //       console.log('Registration successful', response);
//   //       // Show success modal
//   //       const modalElement = document.getElementById('successModal');
//   //       if (modalElement) {
//   //         const modal = new Modal(modalElement);
//   //         modal.show();
//   //       }
//   //     }, error => {
//   //       console.error('Registration failed', error);
//   //     });
//   //   } else {
//   //     // Mark all fields as touched to trigger validation messages
//   //     Object.keys(this.registerForm.controls).forEach(key => {
//   //       const control = this.registerForm.get(key);
//   //       control?.markAsTouched();
//   //     });
//   //   }
//   //}

//   onSubmit() {
//     this.submitted = true;

//     if (this.registerForm.valid) {
//       const email = this.registerForm.get('email')?.value;

//       this.registerService.checkUserExists(email).subscribe(response => {
//         if (response.exists) {
//           // Show error modal or message
//           const modalElement = document.getElementById('errorModal');
//           if (modalElement) {
//             const modal = new Modal(modalElement);
//             modal.show();
//           }
//         } else {
//           this.registerService.register(this.registerForm.value).subscribe(registerResponse => {
//             console.log('Registration successful', registerResponse);
//             // Show success modal
//             const modalElement = document.getElementById('successModal');
//             if (modalElement) {
//               const modal = new Modal(modalElement);
//               modal.show();
//             }
//           }, registerError => {
//             console.error('Registration failed', registerError);
//           });
//         }
//       });
//     } else {
//       // Mark all fields as touched to trigger validation messages
//       Object.keys(this.registerForm.controls).forEach(key => {
//         const control = this.registerForm.get(key);
//         control?.markAsTouched();
//       });
//     }
//   }

// }

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegisterService } from '../services/register.service';
import { Modal } from 'bootstrap';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;
  showPassword = false;

  constructor(private formBuilder: FormBuilder, private registerService: RegisterService) {
    this.registerForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)
      ]],
      confirmPassword: ['', Validators.required],
      userRole: ['', Validators.required]
    }, {
      validator: this.passwordMatchValidator
    });
  }

  ngOnInit(): void { }

  // Custom validator for password matching
  passwordMatchValidator(group: FormGroup) {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;

    if (password && confirmPassword && password !== confirmPassword) {
      group.get('confirmPassword')?.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    }
    return null;
  }

  // Helper method to check for form control errors
  getErrorMessage(controlName: string): string {
    const control = this.registerForm.get(controlName);

    if (control?.errors) {
      if (control.errors['required']) return `${controlName} is required`;
      if (control.errors['email']) return 'Invalid email address';
      if (control.errors['minlength']) return `${controlName} must be at least ${control.errors['minlength'].requiredLength} characters`;
      if (control.errors['pattern']) return 'Password must contain at least one uppercase letter, one lowercase letter, one number and one special character';
      if (control.errors['passwordMismatch']) return 'Passwords do not match';
    }
    return '';
  }

  // Method to toggle password visibility
  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  // Form submission handler
  onSubmit() {
    this.submitted = true;

    if (this.registerForm.valid) {
      const email = this.registerForm.get('email')?.value;

      this.registerService.checkUserExists(email).subscribe(response => {
        if (response.exists) {
          // Show error modal
          const modalElement = document.getElementById('errorModal');
          if (modalElement) {
            const modal = new Modal(modalElement);
            modal.show();
          }
        } else {
          this.registerService.register(this.registerForm.value).subscribe(registerResponse => {
            console.log('Registration successful', registerResponse);
            // Show success modal
            const modalElement = document.getElementById('successModal');
            if (modalElement) {
              const modal = new Modal(modalElement);
              modal.show();
            }
          }, registerError => {
            console.error('Registration failed', registerError);
          });
        }
      });
    } else {
      // Mark all fields as touched to trigger validation messages
      Object.keys(this.registerForm.controls).forEach(key => {
        const control = this.registerForm.get(key);
        control?.markAsTouched();
      });
    }
  }
}
