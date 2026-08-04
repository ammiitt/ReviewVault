import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ThemeService } from '../../core/services/theme';


@Component({
  selector: 'app-theme-toggle',
  imports: [CommonModule],
  templateUrl: './theme-toggle.html',
  styleUrl: './theme-toggle.css',
})
export class ThemeToggle implements OnInit {
   isDark = false;

    constructor(private themeService: ThemeService) { }

    ngOnInit(): void {
        // Subscribe to theme changes
        this.themeService.isDarkMode$.subscribe(isDark => {
            this.isDark = isDark;
        });
    }

    toggleTheme(): void {
        this.themeService.toggleTheme();
    }
}
