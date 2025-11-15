import { Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { HotelService } from '../services/hotel.service';
import { SearchResponse, SearchParams } from '../models/hotel.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-search-results',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.scss'],
})
export class SearchResultsComponent implements OnDestroy {
  loading = false;
  error: string | null = null;
  response: SearchResponse | null = null;
  private sub = new Subscription();

  constructor(public route: ActivatedRoute, private hotelService: HotelService) {
    // react to query param changes
    this.sub.add(
      this.route.queryParamMap.subscribe((qp) => {
        const params: SearchParams = {
          destination: qp.get('destination') ?? undefined,
          checkIn: qp.get('checkIn') ?? undefined,
          checkOut: qp.get('checkOut') ?? undefined,
          page: qp.get('page') ? Number(qp.get('page')) : 1,
          pageSize: qp.get('pageSize') ? Number(qp.get('pageSize')) : 10,
        };
        this.load(params);
      })
    );
  }

  private load(params: SearchParams) {
    this.loading = true;
    this.error = null;
    this.response = null;
    this.sub.add(
      this.hotelService.search(params).subscribe({
        next: (r) => {
          this.response = r;
          this.loading = false;
        },
        error: (err: Error) => {
          this.error = err.message;
          this.loading = false;
        },
      })
    );
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
