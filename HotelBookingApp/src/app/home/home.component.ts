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
  // guests removed from UI and query params

  popular = [
    { name: 'Paris', img: 'https://images.unsplash.com/photo-1502602898657-3e91760cbb34?q=80&w=800&auto=format&fit=crop&crop=entropy' },
    { name: 'Tokyo', img: 'https://images.unsplash.com/photo-1549693578-d683be217e58?q=80&w=800&auto=format&fit=crop&crop=entropy' },
    { name: 'Santorini', img: 'https://images.unsplash.com/photo-1507525428034-b723cf961d3e?q=80&w=800&auto=format&fit=crop&crop=entropy' },
    { name: 'New York', img: 'https://images.unsplash.com/photo-1549921296-3f7f9b02f0b9?q=80&w=800&auto=format&fit=crop&crop=entropy' },
    { name: 'Sydney', img: 'https://images.unsplash.com/photo-1506973035872-a4ec16b8e8d9?q=80&w=800&auto=format&fit=crop&crop=entropy' },
    { name: 'Rome', img: 'https://images.unsplash.com/photo-1508057198894-247b23fe5ade?q=80&w=800&auto=format&fit=crop&crop=entropy' },
  ];

  constructor(private router: Router,private hotelService :HotelService) {}
ngOnInit(){
    this.hotelService.getallLaptop().subscribe({
        next(value) {
            console.log(value);
        },
        error(err) {
            console.error('Error fetching laptops', err);
        },
    })
}
  search() {
    const query: any = {};
    if (this.destination) query.destination = this.destination;
    if (this.checkIn) query.checkIn = this.checkIn;
    if (this.checkOut) query.checkOut = this.checkOut;

    // navigate to results page with query params (production-ready: route-driven)
    this.router.navigate(['/results'], { queryParams: query });
  }
}
