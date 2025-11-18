import { Component } from '@angular/core';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { API_BASE_URL } from '../../../../config';

interface Employee {
  id: number | string;
  hotelId: number;
  fullName: string;
  role: string;
  email: string;
}

@Component({
  selector: 'app-employee-admin',
  standalone: true,
  imports: [CommonModule, NgIf, NgFor, FormsModule],
  templateUrl: './employee-admin.component.html',
  styleUrls: ['./employee-admin.component.scss']
})
export class EmployeeAdminComponent {
  loading = false;
  error: string | null = null;
  employees: Employee[] = [];
  editing: Employee | null = null;
  showAdd = false;
  model: Partial<Employee> = {};

  private endpoint = `${API_BASE_URL.replace(/\/$/, '')}/api/Employee`;

  constructor(private http: HttpClient) {
    this.load();
  }

  load() {
    this.loading = true;
    this.error = null;
    this.http.get<Employee[]>(this.endpoint).subscribe({
      next: data => { this.employees = data || []; this.loading = false; },
      error: e => { this.error = e.message || 'Failed to load employees'; this.loading = false; }
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
        error: e => this.error = e.message || 'Update failed'
      });
    } else {
      this.http.post<Employee>(this.endpoint, this.model).subscribe({
        next: () => {
          this.resetForm();
          this.showAdd = false;
          this.load();
        },
        error: e => this.error = e.message || 'Create failed'
      });
    }
  }

  edit(e: Employee) {
    this.editing = e;
    this.model = { ...e };
    this.showAdd = true;
  }

  delete(e: Employee) {
    if (!confirm(`Delete ${e.fullName}?`)) return;
    this.http.delete(`${this.endpoint}/${e.id}`).subscribe({
      next: () => { if (this.editing?.id === e.id) this.resetForm(); this.load(); },
      error: err => this.error = err.message || 'Delete failed'
    });
  }
}
