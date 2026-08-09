import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { environment } from "../../../environments/environment.development";


@Injectable({
  providedIn: "root",
})
export class ApiBaseService {
  private apiUrl = environment.apiUrl;

    constructor(protected http: HttpClient) { }

    protected get<T>(endpoint: string, params?: HttpParams): Observable<T> {
        return this.http.get<T>(`${this.apiUrl}/${endpoint}`, { params })
            .pipe(catchError(this.handleError));
    }

    protected post<T>(endpoint: string, body: any, params?: HttpParams): Observable<T> {
        return this.http.post<T>(`${this.apiUrl}/${endpoint}`, body, { params })
            .pipe(catchError(this.handleError));
    }

    protected put<T>(endpoint: string, body: any, params?: HttpParams): Observable<T> {
        return this.http.put<T>(`${this.apiUrl}/${endpoint}`, body, { params })
            .pipe(catchError(this.handleError));
    }

    protected delete<T>(endpoint: string): Observable<T> {
        return this.http.delete<T>(`${this.apiUrl}/${endpoint}`)
            .pipe(catchError(this.handleError));
    }

    private handleError(error: any): Observable<never> {
        let message = 'Something went wrong';

        if (error.error) {
            message = error.error.error || error.error.message || message;
        } else if (error.status === 0) {
            message = 'Cannot reach server. Check your connection.';
        } else if (error.status === 401) {
            message = 'Please login again';
        } else if (error.status === 403) {
            message = 'You do not have permission';
        }

        console.error('API Error:', error);
        return throwError(() => new Error(message));
    }
}
