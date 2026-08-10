import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { environment } from '../../../environments/environment.development';


// Functional interceptor (Angular 17+ way — no class needed)
// Runs on EVERY HTTP request automatically

export const authInterceptor: HttpInterceptorFn = (req, next) => {

   const authService = inject(AuthService);
    const router = inject(Router);

    // Only add JWT to OUR API requests, not external APIs
    const isOurApi = req.url.startsWith(environment.apiUrl);

    if (isOurApi) {
        const token = authService.getAccessToken();

        if (token) {
            const authReq = req.clone({
                setHeaders: {
                    Authorization: `Bearer ${token}`
                }
            });

            return next(authReq).pipe(
                catchError((error: HttpErrorResponse) => {
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
        }
    }

    // External APIs or no token — send as-is
    return next(req);
};