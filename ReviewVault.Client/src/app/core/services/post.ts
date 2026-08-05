import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
    PostResponse,
    PostListResponse,
    CreatePostRequest,
    UpdatePostRequest
} from '../models/post.model';
import { ApiBaseService } from './api-base';

@Injectable({
    providedIn: 'root'
})
export class PostService extends ApiBaseService {

    constructor(http: HttpClient) {
        super(http);
    }

    // Get all published posts with pagination
    // Calls: GET /api/Post?page=1&pageSize=10
    getAllPublished(page: number = 1, pageSize: number = 10): Observable<PostListResponse> {
        const params = new HttpParams()
            .set('page', page)
            .set('pageSize', pageSize);
        return this.get<PostListResponse>('Post', params);
    }

    // Get single post by URL-friendly slug
    // Calls: GET /api/Post/attack-on-titan-review
    getBySlug(slug: string): Observable<PostResponse> {
        return this.get<PostResponse>(`Post/${slug}`);
    }

    // Create new blog post (admin only)
    // Calls: POST /api/Post
    create(request: CreatePostRequest): Observable<PostResponse> {
        return this.post<PostResponse>('Post', request);
    }
    
    // Get single post by ID (for admin edit page)
// Calls: GET /api/Post/id/5
    getById(id: number): Observable<PostResponse>   {
    return this.get<PostResponse>(`Post/id/${id}`);
    }

    // Update existing post
    // Calls: PUT /api/Post/5
    update(id: number, request: UpdatePostRequest): Observable<PostResponse> {
        return this.put<PostResponse>(`Post/${id}`, request);
    }

    // Delete a post
    // Calls: DELETE /api/Post/5
    remove(id: number): Observable<void> {
        return this.delete<void>(`Post/${id}`);
    }
}