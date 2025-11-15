import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { SearchParams, SearchResponse } from '../models/hotel.model';
import { API_BASE_URL } from '../config';

@Injectable({ providedIn: 'root' })
export class HotelService {
  private base = API_BASE_URL.replace(/\/$/, '') + '/hotels';
private url="https://localhost:7285/api/";
  constructor(private http: HttpClient) {}

  /**
   * Search hotels using backend endpoint.
   * Retries once on network failure and maps HTTP errors to friendly messages.
   */
  search(params: SearchParams): Observable<SearchResponse> {
    let httpParams = new HttpParams();
    if (params.destination) httpParams = httpParams.set('destination', params.destination);
    if (params.checkIn) httpParams = httpParams.set('checkIn', params.checkIn);
    if (params.checkOut) httpParams = httpParams.set('checkOut', params.checkOut);
    // `guests` was removed from the UI; keep SearchParams compatible but don't send guests by default
    httpParams = httpParams.set('page', String(params.page ?? 1));
    httpParams = httpParams.set('pageSize', String(params.pageSize ?? 10));

    return this.http
      .get<SearchResponse>(this.base + '/search', { params: httpParams })
      .pipe(retry(1), catchError(this.handleError));
  }
getallLaptop(): Observable<any> {
    return this.http.get(this.url + 'laptop');
}
  private handleError(error: HttpErrorResponse) {
    let message = 'An unknown error occurred';
    if (error.error instanceof ErrorEvent) {
      message = `Network error: ${error.error.message}`;
    } else {
      if (error.status === 0) {
        message = 'Unable to reach server. Please check your network.';
      } else if (error.status >= 500) {
        message = 'Server error. Please try again later.';
      } else if (error.status === 401) {
        message = 'Unauthorized. Please sign in.';
      } else if (error.status === 400) {
        message = error.error?.message || 'Bad request.';
      } else {
        message = error.error?.message || `Request failed with status ${error.status}`;
      }
    }
    return throwError(() => new Error(message));
  }
}
