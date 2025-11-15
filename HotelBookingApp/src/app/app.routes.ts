import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SearchResultsComponent } from './search-results/search-results.component';
import { AdminDashboardComponent } from './features/admin/admin-dashboard.component';
import { AdminLoginComponent } from './features/admin/admin-login.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
	{ path: '', component: HomeComponent },
	{ path: 'results', component: SearchResultsComponent },
	{ path: 'admin/login', component: AdminLoginComponent },
	{ path: 'admin', component: AdminDashboardComponent, canActivate: [AuthGuard] },
];
