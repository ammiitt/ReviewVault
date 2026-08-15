import { Injectable } from '@angular/core';
import { ApiBaseService } from './api-base';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CommentResponse, CreateCommentRequest } from '../models/comment.model';

@Injectable({
  providedIn: 'root',
})
export class CommentService extends ApiBaseService {
  constructor(http: HttpClient) {
        super(http);
    }

    getByPostId(postId: number): Observable<CommentResponse[]> {
        return this.get<CommentResponse[]>(`Comment/post/${postId}`);
    }

    create(request: CreateCommentRequest): Observable<CommentResponse> {
        return this.post<CommentResponse>('Comment', request);
    }

    remove(id: number): Observable<void> {
        return this.delete<void>(`Comment/${id}`);
    }

    getCount(postId: number): Observable<number> {
        return this.get<number>(`Comment/post/${postId}/count`);
    }
}
