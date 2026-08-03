import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ThemeService {

    
    private darkModeSubject = new BehaviorSubject<boolean>(false);
    isDarkMode$ = this.darkModeSubject.asObservable();

    constructor() {
        // Check if user saved a theme preference before
        const saved = localStorage.getItem('theme');
        if (saved) {
            this.setTheme(saved === 'dark');
        } else {
            // No preference? Check their OS dark mode setting
            const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
            this.setTheme(prefersDark);
        }
    }

    // Toggle: dark → light, light → dark
    toggleTheme(): void {
        this.setTheme(!this.darkModeSubject.value);
    }

    private setTheme(isDark: boolean): void {
        this.darkModeSubject.next(isDark);                         // notify subscribers
        localStorage.setItem('theme', isDark ? 'dark' : 'light'); // remember choice

        // Bootstrap 5.3 magic — this one line changes ALL bootstrap colors
        if (isDark) {
            document.body.setAttribute('data-bs-theme', 'dark');
        } else {
            document.body.setAttribute('data-bs-theme', 'light');
        }
    }
}