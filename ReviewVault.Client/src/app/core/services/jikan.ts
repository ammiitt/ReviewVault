import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, of } from 'rxjs';
import { JikanResponse } from '../models/jikan.model';

@Injectable({
    providedIn: 'root'
})
export class JikanService {

 private baseUrl = 'https://api.jikan.moe/v4';

    constructor(private http: HttpClient) { }

    // Jikan has rate limit: 3 requests per second
    // Adding small delay to avoid hitting limit

    getTopAnime(limit: number = 20): Observable<JikanResponse> {
        return this.http.get<JikanResponse>(
            `${this.baseUrl}/top/anime?limit=${limit}&filter=bypopularity`
        ).pipe(
            catchError(err => {
                console.error('Jikan Anime Error:', err);
                return of({ data: [], pagination: { last_visible_page: 0, has_next_page: false } });
            })
        );
    }

    getCurrentAnime(limit: number = 10): Observable<JikanResponse> {
        return this.http.get<JikanResponse>(
            `${this.baseUrl}/seasons/now?limit=${limit}`
        ).pipe(
            catchError(err => {
                console.error('Jikan Current Anime Error:', err);
                return of({ data: [], pagination: { last_visible_page: 0, has_next_page: false } });
            })
        );
    }

    getTopManga(limit: number = 20): Observable<JikanResponse> {
        return this.http.get<JikanResponse>(
            `${this.baseUrl}/top/manga?limit=${limit}&filter=bypopularity`
        ).pipe(
            catchError(err => {
                console.error('Jikan Manga Error:', err);
                return of({ data: [], pagination: { last_visible_page: 0, has_next_page: false } });
            })
        );
    }
}