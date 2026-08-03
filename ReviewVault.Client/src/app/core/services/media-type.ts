import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MediaTypeResponse } from '../models/category.model';
import { ApiBaseService } from './api-base';

@Injectable({
    providedIn: 'root'
})
export class MediaTypeService extends ApiBaseService {

    constructor(http: HttpClient) {
        super(http);
    }

    // Get all active media types
    // Calls: GET /api/MediaType
    getAll(): Observable<MediaTypeResponse[]> {
        return this.get<MediaTypeResponse[]>('MediaType');
    }

    // Create new media type (admin only)
    // Calls: POST /api/MediaType?name=Manga&description=Japanese comics
    create(name: string, description?: string): Observable<MediaTypeResponse> {
        let params = new HttpParams().set('name', name);
        if (description) params = params.set('description', description);
        return this.post<MediaTypeResponse>('MediaType', null, params);
    }
}