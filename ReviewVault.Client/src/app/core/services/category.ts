import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CategoryResponse } from '../models/category.model';
import { ApiBaseService } from './api-base';

@Injectable({
    providedIn: 'root'
})
export class CategoryService extends ApiBaseService {

    constructor(http: HttpClient) {
        super(http);
    }

    // Get all categories
    // Calls: GET /api/Category
    getAll(): Observable<CategoryResponse[]> {
        return this.get<CategoryResponse[]>('Category');
    }

    // Create new category (admin only)
    // Calls: POST /api/Category
    create(name: string): Observable<CategoryResponse> {
        return this.post<CategoryResponse>('Category', JSON.stringify(name));
    }
}