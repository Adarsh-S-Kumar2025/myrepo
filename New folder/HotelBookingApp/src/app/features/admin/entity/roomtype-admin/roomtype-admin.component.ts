import { Component } from '@angular/core';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { API_BASE_URL } from '../../../../config';

interface RoomType {
  id: number | string;
  typeName: string;
  description: string;
  capacity: number;
  roomIds: number[];
}

@Component({
  selector: 'app-roomtype-admin',
  standalone: true,
  imports: [CommonModule, NgIf, NgFor, FormsModule],
  templateUrl: './roomtype-admin.component.html',
  styleUrls: ['./roomtype-admin.component.scss']
})
export class RoomTypeAdminComponent {
  loading = false;
  error: string | null = null;
  roomTypes: RoomType[] = [];
  editing: RoomType | null = null;
  showAdd = false;
  model: Partial<RoomType> = {};

  private endpoint = `${API_BASE_URL.replace(/\/$/, '')}/api/RoomType`;

  constructor(private http: HttpClient) {
    this.load();
  }

  load() {
    this.loading = true; this.error = null;
    this.http.get<RoomType[]>(this.endpoint).subscribe({
      next: data => { this.roomTypes = data || []; this.loading = false; },
      error: e => { this.error = e.message || 'Failed to load room types'; this.loading = false; }
    });
  }

  resetForm() { this.model = {}; this.editing = null; }

  toggleAdd() { this.editing = null; this.model = {}; this.showAdd = !this.showAdd; }

  addOrUpdate() {
    if (this.editing) {
      const id = this.editing.id;
      this.http.put(`${this.endpoint}/${id}`, this.model).subscribe({
        next: () => {
          // Ensure UI reflects latest from server
          this.resetForm();
          this.showAdd = false;
          this.load();
        },
        error: e => this.error = e.message || 'Update failed'
      });
    } else {
      this.http.post<RoomType>(this.endpoint, this.model).subscribe({
        next: () => {
          // Some APIs return 204/empty body; reload to get the new list
          this.resetForm();
          this.showAdd = false;
          this.load();
        },
        error: e => this.error = e.message || 'Create failed'
      });
    }
  }

  edit(rt: RoomType) { this.editing = rt; this.model = { ...rt }; this.showAdd = true; }

  delete(rt: RoomType) {
    if (!confirm(`Delete type ${rt.typeName}?`)) return;
    this.http.delete(`${this.endpoint}/${rt.id}`).subscribe({
      next: () => { this.roomTypes = this.roomTypes.filter(x => x.id !== rt.id); if (this.editing?.id === rt.id) this.resetForm(); },
      error: e => this.error = e.message || 'Delete failed'
    });
  }
}
