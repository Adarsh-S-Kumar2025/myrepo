import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { HotelService } from '../services/hotel.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  destination = '';
  checkIn = '';
  checkOut = '';
  loading = false;
  error: string | null = null;
  availableCount: number | null = null;

  popular = [
    { name: 'Paris', img: 'https://images.unsplash.com/photo-1502602898657-3e91760cbb34?q=80&w=800&auto=format&fit=crop&crop=entropy' },
    { name: 'Tokyo', img: 'https://images.unsplash.com/photo-1549693578-d683be217e58?q=80&w=800&auto=format&fit=crop&crop=entropy' },
    { name: 'Santorini', img: 'https://images.unsplash.com/photo-1507525428034-b723cf961d3e?q=80&w=800&auto=format&fit=crop&crop=entropy' },
    { name: 'Sydney', img: 'https://images.unsplash.com/photo-1506973035872-a4ec16b8e8d9?q=80&w=800&auto=format&fit=crop&crop=entropy' },
    { name: 'Rome', img: 'https://images.unsplash.com/photo-1508057198894-247b23fe5ade?q=80&w=800&auto=format&fit=crop&crop=entropy' },
  ];

  constructor(private router: Router, private hotelService: HotelService) {}

  search() {
    this.error = null;
    this.availableCount = null;
    const params: any = {};
    if (this.destination) params.location = this.destination; // map UI field to API expected 'location'
    if (this.checkIn) params.checkIn = this.checkIn;
    if (this.checkOut) params.checkOut = this.checkOut;
    this.loading = true;
    this.hotelService.available(params).subscribe({
      next: (res) => {
        this.loading = false;
        this.availableCount = res.total;
        // After checking availability, go to results page with same filters
        this.router.navigate(['/results'], { queryParams: params });
      },
      error: (err: any) => {
        this.loading = false;
        this.error = err.message || 'Failed to check availability';
      },
    });
  }
}
