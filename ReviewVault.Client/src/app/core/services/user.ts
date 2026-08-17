import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserProfile, UpdateProfileRequest, ChangePasswordRequest } from '../models/user.model';
import { ApiBaseService } from './api-base';

@Injectable({
  providedIn: 'root',
})
export class UserService extends ApiBaseService {

    constructor(http: HttpClient) { super(http); }

    getProfile(): Observable<UserProfile> {
        return this.get<UserProfile>('User/profile');
    }

    updateProfile(request: UpdateProfileRequest): Observable<any> {
        return this.put<any>('User/profile', request);
    }

    changePassword(request: ChangePasswordRequest): Observable<any> {
        return this.put<any>('User/change-password', request);
    }
  }
