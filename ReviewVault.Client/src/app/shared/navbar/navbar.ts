import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThemeToggle } from '../theme-toggle/theme-toggle';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../core/services/auth';
import { ThemeService } from '../../core/services/theme';
import { ToastService } from '../../core/services/toast';
@Component({
  selector: 'app-navbar',
  imports: [CommonModule, RouterModule, ThemeToggle],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar implements OnInit {
    isLoggedIn = false;
    isAdmin = false;
    username = '';
    isDark = false;
    

    constructor(
        private authService: AuthService,
        private themeService: ThemeService,
        private router: Router,
        private toastr : ToastService
    ) { }

    ngOnInit(): void {
        // Subscribe to auth changes — auto updates navbar
        this.authService.currentUser$.subscribe(user => {
            this.isLoggedIn = user !== null;
            this.username = user?.username || '';
            this.isAdmin = user?.role === 'Admin';
        });

        // Subscribe to theme changes
        this.themeService.isDarkMode$.subscribe(isDark => {
            this.isDark = isDark;
        });
    }

    logout(): void {
        this.authService.logout();
        this.toastr.success('Logged out successfully', 'Goodbye! 👋');
        this.router.navigate(['/']);
    }
}
