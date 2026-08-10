import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TmdbResponse } from '../models/tmdb.model';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class TmdbService {

    private baseUrl = 'https://api.themoviedb.org/3';
    private apiKey = environment.tmdbApiKey;
    private imageBaseUrl = 'https://image.tmdb.org/t/p/w500';

    constructor(private http: HttpClient) { }

    // Get trending movies this week
    getTrendingMovies(): Observable<TmdbResponse> {
        return this.http.get<TmdbResponse>(
            `${this.baseUrl}/trending/movie/week?api_key=${this.apiKey}`
        );
    }

    // Get trending TV shows this week
    getTrendingTV(): Observable<TmdbResponse> {
        return this.http.get<TmdbResponse>(
            `${this.baseUrl}/trending/tv/week?api_key=${this.apiKey}`
        );
    }

    // Get popular K-Dramas (Korean TV)
    getKDramas(): Observable<TmdbResponse> {
        return this.http.get<TmdbResponse>(
            `${this.baseUrl}/discover/tv?api_key=${this.apiKey}&with_origin_country=KR&sort_by=popularity.desc`
        );
    }

    // Build full image URL from poster_path
    getImageUrl(path: string | null): string {
        if (!path) return 'https://placehold.co/300x450/6c5ce7/white?text=No+Image';
        return `${this.imageBaseUrl}${path}`;
    }
}