import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LikeInfo } from '../models/like.model';
import { ApiBaseService } from './api-base';

@Injectable({
  providedIn: 'root',
})
export class LikeService extends ApiBaseService {

    constructor(http: HttpClient) { super(http); }

    getLikeInfo(postId: number): Observable<LikeInfo> {
        return this.get<LikeInfo>(`Like/post/${postId}`);
    }

    toggleLike(postId: number): Observable<LikeInfo> {
        return this.post<LikeInfo>(`Like/toggle/${postId}`, null);
    }
  }
