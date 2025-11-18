import { Component } from '@angular/core';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { API_BASE_URL } from '../../../../config';

interface EntityRow { id: number | string; [key: string]: any }

@Component({
  selector: 'app-admin-entity',
  standalone: true,
  imports: [CommonModule, NgIf, NgFor, FormsModule],
  templateUrl: './admin-entity.component.html',
  styleUrls: ['./admin-entity.component.scss']
})
export class AdminEntityComponent {
  entity = ''; // set from route
  title = '';
  rows: EntityRow[] = [];
  editing: EntityRow | null = null;
  showAdd = false;
  loading = false;
  error: string | null = null;
  model: Partial<EntityRow> = {};

  constructor(private route: ActivatedRoute, private http: HttpClient) {
    this.route.paramMap.subscribe(pm => {
      this.entity = pm.get('entity') || '';
      this.title = this.entity ? (this.entity[0].toUpperCase() + this.entity.slice(1)) : 'Entity';
      this.load();
      this.resetForm();
    });
  }

  private apiFor(entity: string) { return entity.toLowerCase(); }

  load() {
    if (!this.entity) return;
    this.loading = true; this.error = null;
    const endpoint = `${API_BASE_URL.replace(/\/$/, '')}/api/${this.apiFor(this.entity)}`;
    this.http.get<EntityRow[]>(endpoint).subscribe({
      next: data => { this.rows = data || []; this.loading = false; },
      error: e => { this.error = e.message || 'Failed to load'; this.loading = false; }
    });
  }

  resetForm() { this.model = {}; this.editing = null; }

  toggleAdd() { this.editing = null; this.model = {}; this.showAdd = !this.showAdd; }

  addOrUpdate() {
    const endpoint = `${API_BASE_URL.replace(/\/$/, '')}/api/${this.apiFor(this.entity)}`;
    if (this.editing) {
      const id = this.editing.id;
      this.http.put(`${endpoint}/${id}`, this.model).subscribe({
        next: () => { this.resetForm(); this.showAdd = false; this.load(); },
        error: e => this.error = e.message || 'Update failed'
      });
    } else {
      this.http.post<EntityRow>(endpoint, this.model).subscribe({
        next: () => { this.resetForm(); this.showAdd = false; this.load(); },
        error: e => this.error = e.message || 'Create failed'
      });
    }
  }

  edit(row: EntityRow) { this.editing = row; this.model = { ...row }; this.showAdd = true; }

  delete(row: EntityRow) {
    if (!confirm(`Delete ${this.title} ${row.id}?`)) return;
    const endpoint = `${API_BASE_URL.replace(/\/$/, '')}/api/${this.apiFor(this.entity)}/${row.id}`;
    this.http.delete(endpoint).subscribe({
      next: () => { if (this.editing?.id === row.id) this.resetForm(); this.load(); },
      error: e => this.error = e.message || 'Delete failed'
    });
  }
}
