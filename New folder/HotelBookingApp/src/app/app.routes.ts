import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SearchResultsComponent } from './search-results/search-results.component';
import { AdminLayoutComponent } from './core/layouts/admin-layout.component';
import { EmployeeAdminComponent } from './features/admin/entity/employee-admin/employee-admin.component';
import { AdminEntityComponent } from './features/admin/entity/admin-entity/admin-entity.component';
import { RoomAdminComponent } from './features/admin/entity/room-admin/room-admin.component';
import { AdminDashboardComponent } from './features/admin/dashboard/admin-dashboard.component';
import { AvailableRoomsComponent } from './rooms/available-rooms.component';
import { RoomTypeAdminComponent } from './features/admin/entity/roomtype-admin/roomtype-admin.component';
import { HotelAdminComponent } from './features/admin/entity/hotel-admin/hotel-admin.component';
import { CustomerAdminComponent } from './features/admin/entity/customer-admin/customer-admin.component';
import { BookingAdminComponent } from './features/admin/entity/booking-admin/booking-admin.component';

export const routes: Routes = [
	{ path: '', component: HomeComponent },
	{ path: 'results', component: SearchResultsComponent },
	{ path: 'rooms', component: AvailableRoomsComponent },

	// admin area: layout with sidebar and child routes
	{
		path: 'admin',
		component: AdminLayoutComponent,
		children: [
			{ path: '', redirectTo: 'employee', pathMatch: 'full' },
			{ path: 'employee', component: EmployeeAdminComponent },
			{ path: 'room', component: RoomAdminComponent },
			{ path: 'roomtype', component: RoomTypeAdminComponent },
			{ path: 'hotel', component: HotelAdminComponent },
			{ path: 'customer', component: CustomerAdminComponent },
            { path: 'booking', component: BookingAdminComponent },
			{ path: ':entity', component: AdminEntityComponent },
		],
	},
];
