import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';


// Functional interceptor (Angular 17+ way — no class needed)
// Runs on EVERY HTTP request automatically

export const authInterceptor: HttpInterceptorFn = (req, next) => {

    const authService = inject(AuthService);
    const router = inject(Router);

    const token = authService.getAccessToken();

    // Clone request with token if available
    const authReq = token
        ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
        : req;

    return next(authReq).pipe(
        catchError((error: HttpErrorResponse) => {
            // If 401 and not on login/register page — auto logout
            if (error.status === 401 &&
                !req.url.includes('Auth/login') &&
                !req.url.includes('Auth/register') &&
                !req.url.includes('Auth/refresh')) {

                authService.logout();
                router.navigate(['/login']);
            }
            return throwError(() => error);
        })
    );
};