import { Component } from '@angular/core';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { API_BASE_URL } from '../../../../config';

interface Hotel {
  id: number | string;
  name: string;
  address: string;
  city: string;
  country: string;
  phoneNumber: string;
  roomIds: number[];
  employeeIds: number[];
  reviewIds: number[];
}

@Component({
  selector: 'app-hotel-admin',
  standalone: true,
  imports: [CommonModule, NgIf, NgFor, FormsModule],
  templateUrl: './hotel-admin.component.html',
  styleUrls: ['./hotel-admin.component.scss']
})
export class HotelAdminComponent {
  loading = false;
  error: string | null = null;
  hotels: Hotel[] = [];
  editing: Hotel | null = null;
  showAdd = false;
  model: Partial<Hotel> = {};

  private endpoint = `${API_BASE_URL.replace(/\/$/, '')}/api/Hotel`;

  constructor(private http: HttpClient) { this.load(); }

  load() {
    this.loading = true; this.error = null;
    this.http.get<Hotel[]>(this.endpoint).subscribe({
      next: data => { this.hotels = data || []; this.loading = false; },
      error: e => { this.error = e.message || 'Failed to load hotels'; this.loading = false; }
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
      this.http.post<Hotel>(this.endpoint, this.model).subscribe({
        next: () => {
          this.resetForm();
          this.showAdd = false;
          this.load();
        },
        error: e => this.error = e.message || 'Create failed'
      });
    }
  }

  edit(h: Hotel) { this.editing = h; this.model = { ...h }; this.showAdd = true; }

  delete(h: Hotel) {
    if (!confirm(`Delete hotel ${h.name}?`)) return;
    this.http.delete(`${this.endpoint}/${h.id}`).subscribe({
      next: () => { if (this.editing?.id === h.id) this.resetForm(); this.load(); },
      error: e => this.error = e.message || 'Delete failed'
    });
  }
}
