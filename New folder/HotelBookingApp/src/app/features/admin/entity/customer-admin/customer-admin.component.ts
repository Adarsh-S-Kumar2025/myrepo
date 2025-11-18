import { Component } from '@angular/core';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { API_BASE_URL } from '../../../../config';

interface Customer {
  id: number | string;
  fullName: string;
  email: string;
  phoneNumber: string;
  idProofNumber: string;
  bookingIds: number[];
}

@Component({
  selector: 'app-customer-admin',
  standalone: true,
  imports: [CommonModule, NgIf, NgFor, FormsModule],
  templateUrl: './customer-admin.component.html',
  styleUrls: ['./customer-admin.component.scss']
})
export class CustomerAdminComponent {
  loading = false;
  error: string | null = null;
  customers: Customer[] = [];
  editing: Customer | null = null;
  showAdd = false;
  model: Partial<Customer> = {};

  private endpoint = `${API_BASE_URL.replace(/\/$/, '')}/api/Customer`;

  constructor(private http: HttpClient) {
    this.load();
  }

  load() {
    this.loading = true;
    this.error = null;
    this.http.get<Customer[]>(this.endpoint).subscribe({
      next: (data) => { this.customers = data || []; this.loading = false; },
      error: (e) => { this.error = e.message || 'Failed to load customers'; this.loading = false; }
    });
  }

  resetForm() {
    this.model = {};
    this.editing = null;
  }

  toggleAdd() {
    this.editing = null;
    this.model = {};
    this.showAdd = !this.showAdd;
  }

  addOrUpdate() {
    if (this.editing) {
      const id = this.editing.id;
      this.http.put(`${this.endpoint}/${id}`, this.model).subscribe({
        next: () => {
          this.resetForm();
          this.showAdd = false;
          this.load();
        },
        error: (e) => this.error = e.message || 'Update failed'
      });
    } else {
      this.http.post<Customer>(this.endpoint, this.model).subscribe({
        next: () => {
          this.resetForm();
          this.showAdd = false;
          this.load();
        },
        error: (e) => this.error = e.message || 'Create failed'
      });
    }
  }

  edit(c: Customer) {
    this.editing = c;
    this.model = { ...c };
    this.showAdd = true;
  }

  delete(c: Customer) {
    if (!confirm(`Delete ${c.fullName}?`)) return;
    this.http.delete(`${this.endpoint}/${c.id}`).subscribe({
      next: () => { if (this.editing?.id === c.id) this.resetForm(); this.load(); },
      error: (e) => this.error = e.message || 'Delete failed'
    });
  }
}
