import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as $ from 'jquery';
import 'bootstrap';
import { AccountService } from '../services/account.service';

@Component({
    selector: 'app-applyaccount',
    templateUrl: './applyaccount.component.html',
    styleUrls: ['./applyaccount.component.css']
})
export class ApplyaccountComponent implements OnInit {
    applyAccountForm: FormGroup = this.formBuilder.group({});
    submitted = false;
    errorMessage: string = '';

    constructor(private formBuilder: FormBuilder, private accountService: AccountService) { }

    ngOnInit() {
        this.applyAccountForm = this.formBuilder.group({
            HolderName: ['', Validators.required],
            AccountNumber: ['', Validators.required],
            CusId: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
            AccountType: ['', Validators.required],
            IFSC: ['', Validators.required],
            BranchName: ['', Validators.required],
            BranchAddress: ['', Validators.required],
            BranchPhoneNo: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
            BranchEmailId: ['', [Validators.required, Validators.email]],
            Balance: ['', Validators.required],
            AccCreationDate: ['', Validators.required]
        });
    }

    get f() { return this.applyAccountForm.controls; }

    onSubmit() {
        this.submitted = true;

        if (this.applyAccountForm.invalid) {
            return;
        }

        this.accountService.createAccount(this.applyAccountForm.value).subscribe(
            response => {
                $('#successModal').modal('show');
            },
            error => {
                if (error.error && error.error.message) {
                    this.errorMessage = error.error.message;
                } else {
                    this.errorMessage = "Account registration failed. Please try again.";
                }
                $('#errorModal').modal('show');
            }
        );
    }
}
