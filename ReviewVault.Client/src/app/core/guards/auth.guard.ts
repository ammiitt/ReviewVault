import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth';


// Functional guard (Angular 17+ way)
// Protects admin routes

export const authGuard: CanActivateFn = (route, state) => {

    const authService = inject(AuthService);
    const router = inject(Router);

    // Check: is user logged in AND is admin?
    if (authService.isLoggedIn() && authService.isAdmin()) {
        return true;    // ✅ Allow access
    }

    // ❌ Not logged in or not admin → redirect to login
    router.navigate(['/login']);
    return false;

};

export const adminGuard: CanActivateFn = (route, state) => {

    const authService = inject(AuthService);
    const router = inject(Router);

    if (authService.isLoggedIn() && authService.isAdmin()) {
        return true;
    }

    if (authService.isLoggedIn()) {
        // Logged in but not admin → go home
        router.navigate(['/']);
    } else {
        // Not logged in → go to login
        router.navigate(['/login']);
    }
    return false;
};