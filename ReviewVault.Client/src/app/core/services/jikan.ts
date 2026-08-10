import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JikanResponse } from '../models/jikan.model';

@Injectable({
    providedIn: 'root'
})
export class JikanService {

    private baseUrl = 'https://api.jikan.moe/v4';

    constructor(private http: HttpClient) { }

    // Get top rated anime
    getTopAnime(limit: number = 10): Observable<JikanResponse> {
        return this.http.get<JikanResponse>(
            `${this.baseUrl}/top/anime?limit=${limit}&filter=bypopularity`
        );
    }

    // Get currently airing anime
    getCurrentAnime(limit: number = 10): Observable<JikanResponse> {
        return this.http.get<JikanResponse>(
            `${this.baseUrl}/seasons/now?limit=${limit}`
        );
    }

    // Get top manga
    getTopManga(limit: number = 10): Observable<JikanResponse> {
        return this.http.get<JikanResponse>(
            `${this.baseUrl}/top/manga?limit=${limit}&filter=bypopularity`
        );
    }
}