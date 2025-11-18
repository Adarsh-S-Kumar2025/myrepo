import { Component } from '@angular/core';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { HotelService, Room } from '../services/hotel.service';

@Component({
  selector: 'app-available-rooms',
  standalone: true,
  imports: [CommonModule, NgIf, NgFor],
  templateUrl: './available-rooms.component.html',
  styleUrls: ['./available-rooms.component.scss']
})
export class AvailableRoomsComponent {
  loading = false;
  error: string | null = null;
  rooms: Room[] = [];
  hotelId: string | null = null;
  checkIn: string | null = null;
  checkOut: string | null = null;

  constructor(private route: ActivatedRoute, private hotelService: HotelService) {
    this.route.queryParamMap.subscribe(qp => {
      this.hotelId = qp.get('hotelId');
      this.checkIn = qp.get('checkIn');
      this.checkOut = qp.get('checkOut');
      if (this.hotelId && this.checkIn && this.checkOut) {
        this.load();
      }
    });
  }

  load() {
    if (!this.hotelId || !this.checkIn || !this.checkOut) return;
    this.loading = true;
    this.error = null;
    this.rooms = [];
    this.hotelService.roomsAvailable(this.hotelId, this.checkIn, this.checkOut).subscribe({
      next: (r) => { this.rooms = r; this.loading = false; },
      error: (e: any) => { this.error = e.message || 'Failed to load rooms'; this.loading = false; }
    });
  }
}
