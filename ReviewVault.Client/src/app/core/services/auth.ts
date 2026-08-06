import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { ApiBaseService } from './api-base';
import {
    AuthResponse,
    LoginRequest,
    RegisterRequest,
    RefreshTokenRequest
} from '../models/auth.model';


@Injectable({
    providedIn: 'root'
})
export class AuthService extends ApiBaseService {

   
    private currentUserSubject = new BehaviorSubject<AuthResponse | null>(null);
    currentUser$ = this.currentUserSubject.asObservable();

   constructor(http: HttpClient) {
    super(http);
    this.checkStoredAuth();
}

private checkStoredAuth(): void {
    const stored = localStorage.getItem('auth');
    if (!stored) return;

    const auth: AuthResponse = JSON.parse(stored);

    // Check if refresh token is expired (7 days)
    const refreshExpiry = new Date(auth.refreshTokenExpiresAt);
    if (refreshExpiry <= new Date()) {
        // Refresh token expired — full logout
        localStorage.removeItem('auth');
        this.currentUserSubject.next(null);
        return;
    }

    // Check if access token is expired (30 min)
    const accessExpiry = new Date(auth.accessTokenExpiresAt);
    if (accessExpiry <= new Date()) {
        // Access token expired but refresh token valid — try refresh
        this.refreshToken().subscribe({
            next: (newAuth) => {
                // New tokens saved automatically via tap() in refreshToken()
            },
            error: () => {
                // Refresh failed — full logout
                localStorage.removeItem('auth');
                this.currentUserSubject.next(null);
            }
        });
        return;
    }

    // Both tokens valid — restore session
    this.currentUserSubject.next(auth);
}
    
    register(request: RegisterRequest): Observable<AuthResponse> {
        return this.post<AuthResponse>('Auth/register', request)
            .pipe(tap(response => this.storeAuth(response)));
    }

    
    login(request: LoginRequest): Observable<AuthResponse> {
        return this.post<AuthResponse>('Auth/login', request)
            .pipe(tap(response => this.storeAuth(response)));
    }

    // Use refresh token to get new access token
    refreshToken(): Observable<AuthResponse> {
        const auth = this.getStoredAuth();
        const request: RefreshTokenRequest = { token: auth?.refreshToken || '' };
        return this.post<AuthResponse>('Auth/refresh', request)
            .pipe(tap(response => this.storeAuth(response)));
    }

    // Logout — revoke token on server, clear browser, notify subscribers
    logout(): void {
        const auth = this.getStoredAuth();
        if (auth) {
            this.post('Auth/revoke', { token: auth.refreshToken }).subscribe();
        }
        localStorage.removeItem('auth');
        this.currentUserSubject.next(null); // "Hey everyone, user logged out!"
    }

    // Get JWT token for API calls
    getAccessToken(): string | null {
        return this.getStoredAuth()?.accessToken || null;
    }

    // Check if user is logged in and token not expired
    isLoggedIn(): boolean {
        const auth = this.getStoredAuth();
        if (!auth) return false;
        return new Date(auth.accessTokenExpiresAt) > new Date();
    }

    // Check if user is admin
    isAdmin(): boolean {
        return this.getStoredAuth()?.role === 'Admin';
    }

    // Get username for display
    getUsername(): string {
        return this.getStoredAuth()?.username || '';
    }

    // Save tokens to browser + notify all subscribers
    private storeAuth(response: AuthResponse): void {
        localStorage.setItem('auth', JSON.stringify(response));
        this.currentUserSubject.next(response);
    }

    // Read tokens from browser storage
    private getStoredAuth(): AuthResponse | null {
        const stored = localStorage.getItem('auth');
        return stored ? JSON.parse(stored) : null;
    }
}