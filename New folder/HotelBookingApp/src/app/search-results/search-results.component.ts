import { Component, OnDestroy } from '@angular/core';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { HotelService } from '../services/hotel.service';
import { SearchResponse, SearchParams } from '../models/hotel.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-search-results',
  standalone: true,
  imports: [CommonModule, NgIf, NgFor, RouterModule, FormsModule],
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.scss'],
})
export class SearchResultsComponent implements OnDestroy {
  loading = false;
  error: string | null = null;
  response: SearchResponse | null = null;
  private sub = new Subscription();
  // UI state
  sortMode: 'priceAsc' | 'priceDesc' | 'rating' = 'priceAsc';
  priceMax = 5000; // slider upper bound

  constructor(public route: ActivatedRoute, private hotelService: HotelService) {
    // react to query param changes
    this.sub.add(
      this.route.queryParamMap.subscribe((qp) => {
        const params: SearchParams & { location?: string } = {
          // support both query param keys
          location: qp.get('location') ?? qp.get('destination') ?? undefined,
          checkIn: qp.get('checkIn') ?? undefined,
          checkOut: qp.get('checkOut') ?? undefined,
          page: qp.get('page') ? Number(qp.get('page')) : 1,
          pageSize: qp.get('pageSize') ? Number(qp.get('pageSize')) : 10,
        };
        this.load(params);
      })
    );
  }

  private withRandomThumb(url: string | undefined, seed: string): string {
    return url || `https://picsum.photos/seed/${encodeURIComponent(seed)}/480/260`;
  }

  private load(params: SearchParams & { location?: string }) {
    this.loading = true;
    this.error = null;
    this.response = null;
    this.sub.add(
      this.hotelService.available(params).subscribe({
        next: (r) => {
          // ensure each hotel has a thumbnail
          this.response = {
            ...r,
            hotels: r.hotels.map((h, i) => ({
              ...h,
              thumbnailUrl: this.withRandomThumb(h.thumbnailUrl, String(h.id || i))
            }))
          };
          this.loading = false;
        },
        error: (err: Error) => {
          // Normalize common messages; show 'Bad request.' explicitly for 400 cases
          this.error = err.message === 'Bad request.' ? 'Bad request. Please adjust your search inputs.' : err.message;
          this.loading = false;
        },
      })
    );
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  get displayedHotels() {
    if (!this.response) return [];
    let hotels = this.response.hotels.filter(h => (h.pricePerNight ?? 0) <= this.priceMax);
    switch (this.sortMode) {
      case 'priceAsc':
        hotels = hotels.sort((a,b)=>(a.pricePerNight ?? 0)-(b.pricePerNight ?? 0));
        break;
      case 'priceDesc':
        hotels = hotels.sort((a,b)=>(b.pricePerNight ?? 0)-(a.pricePerNight ?? 0));
        break;
      case 'rating':
        hotels = hotels.sort((a,b)=>(b.rating ?? 0)-(a.rating ?? 0));
        break;
    }
    return hotels;
  }
}
