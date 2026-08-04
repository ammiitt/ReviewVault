import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThemeToggle } from '../theme-toggle/theme-toggle';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth';
import { ThemeService } from '../../core/services/theme';
@Component({
  selector: 'app-navbar',
  imports: [CommonModule, RouterModule, ThemeToggle],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar implements OnInit {
  isLoggedIn = false;
    username = '';
    isDark = false;

    constructor(
        private authService: AuthService,
        private themeService: ThemeService
    ) { }

    ngOnInit(): void {
        // Subscribe to auth changes — auto updates navbar
        this.authService.currentUser$.subscribe(user => {
            this.isLoggedIn = user !== null;
            this.username = user?.username || '';
        });

        // Subscribe to theme changes
        this.themeService.isDarkMode$.subscribe(isDark => {
            this.isDark = isDark;
        });
    }

    logout(): void {
        this.authService.logout();
    }
}
