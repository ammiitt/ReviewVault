import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookmarkResponse } from '../models/bookmark.model';
import { ApiBaseService } from './api-base';

@Injectable({
  providedIn: 'root',
})
export class BookmarkService extends ApiBaseService {

    constructor(http: HttpClient) { super(http); }

    getMyBookmarks(): Observable<BookmarkResponse[]> {
        return this.get<BookmarkResponse[]>('Bookmark');
    }

    isBookmarked(postId: number): Observable<boolean> {
        return this.get<boolean>(`Bookmark/check/${postId}`);
    }

    toggleBookmark(postId: number): Observable<boolean> {
        return this.post<boolean>(`Bookmark/toggle/${postId}`, null);
    }
}
