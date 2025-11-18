import { Component } from '@angular/core';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { API_BASE_URL } from '../../../../config';

interface Booking {
  id: number | string;
  customerId: number;
  roomId: number;
  checkInDate: string; // ISO string
  checkOutDate: string; // ISO string
  status: string;
  totalAmount: number;
  paymentId: number;
}

@Component({
  selector: 'app-booking-admin',
  standalone: true,
  imports: [CommonModule, NgIf, NgFor, FormsModule],
  templateUrl: './booking-admin.component.html',
  styleUrls: ['./booking-admin.component.scss']
})
export class BookingAdminComponent {
  loading = false;
  error: string | null = null;
  bookings: Booking[] = [];

  private endpoint = `${API_BASE_URL.replace(/\/$/, '')}/api/Booking`;

  constructor(private http: HttpClient) { this.load(); }

  load() {
    this.loading = true; this.error = null;
    this.http.get<Booking[]>(this.endpoint).subscribe({
      next: data => { this.bookings = data || []; this.loading = false; },
      error: e => { this.error = e.message || 'Failed to load bookings'; this.loading = false; }
    });
  }
}
