import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, AbstractControl, ValidationErrors } from '@angular/forms';
import { AuthService } from '../../core/services/auth';
import { ToastService } from '../../core/services/toast';

@Component({
  selector: 'app-register',
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule
  ],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
    registerForm: FormGroup;
    loading = false;
    submitted = false;
    showPassword = false;
    showConfirm = false;

    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private router: Router,
        private toast: ToastService
    ) {
        this.registerForm = this.fb.group({
            username: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
            email: ['', [Validators.required, Validators.email]],
            password: ['', [
                Validators.required,
                Validators.minLength(6),
                Validators.pattern(/^(?=.*[A-Z])(?=.*[0-9])/)
            ]],
            confirmPassword: ['', [Validators.required]]
        }, {
            validators: this.passwordMatchValidator
        });
    }

    get f() { return this.registerForm.controls; }

    // Password strength checks for live indicators
    get hasMinLength(): boolean {
        return (this.f['password'].value?.length || 0) >= 6;
    }

    get hasUppercase(): boolean {
        return /[A-Z]/.test(this.f['password'].value || '');
    }

    get hasNumber(): boolean {
        return /[0-9]/.test(this.f['password'].value || '');
    }

    // Custom validator — passwords must match
    passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
        const password = control.get('password');
        const confirmPassword = control.get('confirmPassword');

        if (password && confirmPassword && password.value !== confirmPassword.value) {
            confirmPassword.setErrors({ passwordMismatch: true });
            return { passwordMismatch: true };
        }
        return null;
    }

    onSubmit(): void {
        this.submitted = true;

        if (this.registerForm.invalid) {
            this.toast.warning('Please fix the errors', 'Validation');
            return;
        }

        this.loading = true;

        // Send only what API needs (not confirmPassword)
        const request = {
            username: this.registerForm.value.username,
            email: this.registerForm.value.email,
            password: this.registerForm.value.password
        };

        this.authService.register(request).subscribe({
            next: () => {
                this.toast.success('Account created! Welcome!', 'Registered 🎉');
                this.router.navigate(['/admin/dashboard']);
            },
            error: (err) => {
                this.toast.error(err.message, 'Registration Failed');
                this.loading = false;
            }
        });
    }
}
