import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { SearchParams, SearchResponse, Hotel } from '../models/hotel.model';
export interface Room { id: number; roomNumber: string; hotelId: number; roomTypeId: number; status: string; pricePerNight: number; bookingIds: number[] }
import { API_BASE_URL } from '../config';

@Injectable({ providedIn: 'root' })
export class HotelService {
  private base = API_BASE_URL.replace(/\/$/, '') + '/hotels';
  private api = API_BASE_URL.replace(/\/$/, '') + '/api';
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
  /**
   * Check available rooms via /api/Hotel/available.
   * Backend returns a raw array of { hotelId, name, location, price, rating }.
   * We map it into our existing SearchResponse shape for UI reuse.
   */
  available(params: SearchParams & { location?: string }): Observable<SearchResponse> {
    let httpParams = new HttpParams();
    // The endpoint expects 'location' instead of 'destination'
    const loc = (params.location || params.destination);
    if (loc) httpParams = httpParams.set('location', loc);
    if (params.checkIn) httpParams = httpParams.set('checkIn', params.checkIn);
    if (params.checkOut) httpParams = httpParams.set('checkOut', params.checkOut);
    httpParams = httpParams.set('page', String(params.page ?? 1));
    httpParams = httpParams.set('pageSize', String(params.pageSize ?? 10));

    interface RawAvailableHotel { hotelId: number; name: string; location: string; price: number; rating: number; }

    return this.http
      .get<RawAvailableHotel[]>(`${this.api}/Hotel/available`, { params: httpParams })
      .pipe(
        retry(1),
        // map raw array into SearchResponse
        (source => new Observable<SearchResponse>(observer => {
          const sub = source.subscribe({
            next: (raw) => {
              const hotels: Hotel[] = raw.map(r => ({
                id: String(r.hotelId),
                name: r.name,
                location: r.location,
                pricePerNight: r.price,
                rating: r.rating
              }));
              observer.next({ total: hotels.length, page: params.page ?? 1, pageSize: params.pageSize ?? hotels.length, hotels });
              observer.complete();
            },
            error: (e) => observer.error(e),
            complete: () => {/* noop */}
          });
          return () => sub.unsubscribe();
        })),
        catchError(this.handleError)
      );
  }

  /** Fetch available rooms for a hotel by id and date range */
  roomsAvailable(hotelId: number | string, checkIn: string, checkOut: string): Observable<Room[]> {
    let httpParams = new HttpParams()
      .set('hotelId', String(hotelId))
      .set('checkIn', checkIn)
      .set('checkOut', checkOut);
    return this.http
      .get<Room[]>(`${this.api}/Room/available`, { params: httpParams })
      .pipe(retry(1), catchError(this.handleError));
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
