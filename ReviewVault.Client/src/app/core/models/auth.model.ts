export interface LoginRequest {
    email: string;
    password: string;
}

export interface RegisterRequest {
    username: string;
    email: string;
    password: string;
}

export interface RefreshTokenRequest {
    token: string;
}

export interface AuthResponse {
    accessToken: string;
    refreshToken: string;
    username: string;
    role: string;
    accessTokenExpiresAt: string;
    refreshTokenExpiresAt: string;
}