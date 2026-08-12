import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Footer } from './shared/footer/footer';
import { Navbar } from './shared/navbar/navbar';
import { ThemeService } from './core/services/theme';
import { Toaster } from './shared/toast/toast';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar, Footer, Toaster],      
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  constructor(private themeService: ThemeService) { }

    ngOnInit(): void {
        // ThemeService constructor handles initial theme setup
        // Just injecting it here ensures it runs on app start
    }
  protected readonly title = signal('ReviewVault.Client');
}
