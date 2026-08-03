import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth';


// Functional interceptor (Angular 17+ way — no class needed)
// Runs on EVERY HTTP request automatically

export const authInterceptor: HttpInterceptorFn = (req, next) => {

    // Get the auth service
    const authService = inject(AuthService);

    // Get the JWT token
    const token = authService.getAccessToken();

    // If we have a token, clone the request and add the Authorization header
    if (token) {
        const clonedRequest = req.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });
        // Send the modified request (with token)
        return next(clonedRequest);
    }

    // No token? Send the original request (public endpoints like GET /posts)
    return next(req);
};