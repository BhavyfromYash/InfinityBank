// // import { Component, OnInit } from '@angular/core';
// // import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// // import { Router } from '@angular/router';

// // @Component({
// //   selector: 'app-login',
// //   templateUrl: './login.component.html',
// //   styleUrls: ['./login.component.css']
// // })
// // export class LoginComponent implements OnInit {

// //   loginForm: FormGroup;
// //   submitted = false;
// //   showPassword = false;

// //   constructor(
// //     private formBuilder: FormBuilder,
// //     private router: Router
// //   ) {
// //     this.loginForm = this.formBuilder.group({
// //       email: ['', [Validators.required, Validators.email]],
// //       password: ['', [Validators.required, Validators.minLength(8)]]
// //     });
// //   }

// //   ngOnInit(): void { }

// //   // Getter for easy access to form fields
// //   get f() {
// //     return this.loginForm.controls;
// //   }

// //   togglePasswordVisibility(): void {
// //     this.showPassword = !this.showPassword;
// //   }

// //   onSubmit(): void {
// //     this.submitted = true;

// //     if (this.loginForm.invalid) {
// //       return;
// //     }

// //     // Here you would typically call your authentication service
// //     console.log('Login Details:', this.loginForm.value);

// //     // Mock successful login
// //     alert('Login Successful!');
// //     this.router.navigate(['/home']);
// //   }

// // }

// // import { Component } from '@angular/core';
// // import { User } from '../Models/User';
// // import { Router } from '@angular/router';
// // import { UserService } from '../services/user.service';

// // @Component({
// //   selector: 'app-login',
// //   templateUrl: './login.component.html',
// //   styleUrls: ['./login.component.css']
// // })
// // export class LoginComponent {
// //   currentuser: User = { userId: 0, name: '', email: '', password: '', confirmPassword: '' ,userRole:'', OTP:0};

// //   constructor(private _service: UserService, private router: Router) { }

// //   isLoggedIn: string = 'true'

// //   Role: string = ''
// //   onSubmit(form: any) {
// //     let loginuser = form.value;
// //     this._service.Login(loginuser).subscribe((res: any) => {
// //       if (res.status == 200) {
// //         localStorage.setItem('logged', this.isLoggedIn)
// //         sessionStorage.setItem('loginToken', res.body.token)
// //         console.log("token is .....")
// //         console.log(res.body.token)
// //         sessionStorage.setItem("userrole", res.body.userRole)
// //         this.Role = sessionStorage.getItem("userrole") || ''
// //         if (this.Role === 'Customer') {
// //           alert("login success!");
// //           this.router.navigate(['customerDashboard'])
// //         }
// //         else if (this.Role === 'Manager') {
// //           alert("login success!");
// //           this.router.navigate(['managerDashboard'])
// //         }
// //         else
// //           alert("Sorry You are not appropriate User")

// //       }
// //     }, (err: any) => {
// //       alert("There was a problem logging" + err.message);

// //     });
// //   }

// // }

// import { Component, OnInit } from '@angular/core';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { Router } from '@angular/router';
// import { User } from '../Models/User';
// import { UserService } from '../services/user.service';

// @Component({
//   selector: 'app-login',
//   templateUrl: './login.component.html',
//   styleUrls: ['./login.component.css']
// })
// export class LoginComponent implements OnInit {
//   loginForm: FormGroup;
//   submitted = false;
//   showPassword = false;
//   currentuser: User = { userId: 0, name: '', email: '', password: '', confirmPassword: '', userRole: '', OTP: 0 };

//   constructor(
//     private formBuilder: FormBuilder,
//     private router: Router,
//     private _service: UserService
//   ) {
//     this.loginForm = this.formBuilder.group({
//       email: ['', [Validators.required, Validators.email]],
//       password: ['', [Validators.required, Validators.minLength(8)]]
//     });
//   }

//   ngOnInit(): void { }

//   // Getter for easy access to form fields
//   get f() {
//     return this.loginForm.controls;
//   }

//   togglePasswordVisibility(): void {
//     this.showPassword = !this.showPassword;
//   }

//   onSubmit(): void {
//     this.submitted = true;

//     if (this.loginForm.invalid) {
//       return;
//     }

//     let loginuser = this.loginForm.value;
//     this._service.Login(loginuser).subscribe((res: any) => {
//       if (res.status === 200) {
//         localStorage.setItem('logged', 'true');
//         sessionStorage.setItem('loginToken', res.body.token);
//         console.log("Token is:", res.body.token);
//         sessionStorage.setItem("userrole", res.body.userRole);
//         const role = sessionStorage.getItem("userrole");

//         if (role === 'Customer') {
//           alert("Login success!");
//           this.router.navigate(['customerdashboard']);
//         } else if (role === 'Manager') {
//           alert("Login success!");
//           this.router.navigate(['managerdashboard']);
//         } else {
//           alert("Sorry, you are not an appropriate user.");
//         }
//       }
//     }, (err: any) => {
//       alert("There was a problem logging in: " + err.message);
//     });
//   }
// }


import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../Models/User';
import { UserService } from '../services/user.service';
import * as bootstrap from 'bootstrap';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  submitted = false;
  showPassword = false;
  currentuser: User = { userId: 0, name: '', email: '', password: '', confirmPassword: '', userRole: '', OTP: 0 };

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private _service: UserService
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  ngOnInit(): void { }

  // Getter for easy access to form fields
  get f() {
    return this.loginForm.controls;
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    let loginuser = this.loginForm.value;
    this._service.Login(loginuser).subscribe((res: any) => {
      if (res.status === 200) {
        localStorage.setItem('logged', 'true');
        sessionStorage.setItem('loginToken', res.body.token);
        console.log("Token is:", res.body.token);
        sessionStorage.setItem("userrole", res.body.userRole);
        const role = sessionStorage.getItem("userrole");

        // Show success modal
        this.showSuccessModal();

        // Delay the redirection to allow the modal to be seen
        setTimeout(() => {
          this.hideSuccessModal();
          if (role === 'Customer') {
            this.router.navigate(['customerdashboard']);
          } else if (role === 'Manager') {
            this.router.navigate(['managerdashboard']);
          } else {
            alert("Sorry, you are not an appropriate user.");
          }
        }, 2000); // 2-second delay
      }
    }, (err: any) => {
      alert("There was a problem logging in: " + err.message);
    });
  }

  showSuccessModal(): void {
    const modalElement = document.getElementById('successModal');
    if (modalElement) {
      const modal = new bootstrap.Modal(modalElement);
      modal.show();
    }
  }

  hideSuccessModal(): void {
    const modalElement = document.getElementById('successModal');
    if (modalElement) {
      const modal = bootstrap.Modal.getInstance(modalElement);
      if (modal) {
        modal.hide();
      }
    }
  }
}