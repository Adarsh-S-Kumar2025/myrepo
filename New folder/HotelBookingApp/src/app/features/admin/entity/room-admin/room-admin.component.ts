import { Component } from '@angular/core';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { API_BASE_URL } from '../../../../config';

interface Room {
  id: number | string;
  roomNumber: string;
  hotelId: number;
  roomTypeId: number;
  status: string;
  pricePerNight: number;
  bookingIds: number[];
}

@Component({
  selector: 'app-room-admin',
  standalone: true,
  imports: [CommonModule, NgIf, NgFor, FormsModule],
  templateUrl: './room-admin.component.html',
  styleUrls: ['./room-admin.component.scss']
})
export class RoomAdminComponent {
  loading = false;
  error: string | null = null;
  rooms: Room[] = [];
  editing: Room | null = null;
  showAdd = false;
  model: Partial<Room> = {};

  private endpoint = `${API_BASE_URL.replace(/\/$/, '')}/api/Room`;

  constructor(private http: HttpClient) {
    this.load();
  }

  load() {
    this.loading = true; this.error = null;
    this.http.get<Room[]>(this.endpoint).subscribe({
      next: data => { this.rooms = data || []; this.loading = false; },
      error: e => { this.error = e.message || 'Failed to load rooms'; this.loading = false; }
    });
  }

  resetForm() { this.model = {}; this.editing = null; }

  toggleAdd() { this.editing = null; this.model = {}; this.showAdd = !this.showAdd; }

  addOrUpdate() {
    if (this.editing) {
      const id = this.editing.id;
      this.http.put(`${this.endpoint}/${id}`, this.model).subscribe({
        next: () => {
          this.resetForm();
          this.showAdd = false;
          this.load();
        },
        error: e => this.error = e.message || 'Update failed'
      });
    } else {
      this.http.post<Room>(this.endpoint, this.model).subscribe({
        next: () => {
          this.resetForm();
          this.showAdd = false;
          this.load();
        },
        error: e => this.error = e.message || 'Create failed'
      });
    }
  }

  edit(r: Room) { this.editing = r; this.model = { ...r }; this.showAdd = true; }

  delete(r: Room) {
    if (!confirm(`Delete room ${r.roomNumber}?`)) return;
    this.http.delete(`${this.endpoint}/${r.id}`).subscribe({
      next: () => { if (this.editing?.id === r.id) this.resetForm(); this.load(); },
      error: e => this.error = e.message || 'Delete failed'
    });
  }
}
