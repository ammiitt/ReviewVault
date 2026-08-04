import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
    loginForm: FormGroup;
    loading = false;
    error = '';
    submitted = false;
    showPassword = false;

    constructor(
        private fb: FormBuilder,        // helps create forms easily
        private authService: AuthService,
        private router: Router
    ) {
        // Create form with validation rules
        this.loginForm = this.fb.group({
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required]]
        });
    }

    // Shortcut to access form fields in template
    get f() { return this.loginForm.controls; }

    onSubmit(): void {
        this.submitted = true;

        // Stop if form is invalid
        if (this.loginForm.invalid) return;

        this.loading = true;
        this.error = '';

        this.authService.login(this.loginForm.value).subscribe({
            next: () => {
                // Login success → go to admin dashboard
                this.router.navigate(['/admin/dashboard']);
            },
            error: (err) => {
                this.error = err.message;
                this.loading = false;
            }
        });
    }
}
