export interface Hotel {
  id: string;
  name: string;
  location: string;
  rating?: number;
  pricePerNight?: number;
  thumbnailUrl?: string;
  amenities?: string[];
  reviewScore?: number;
}

export interface SearchParams {
  destination?: string;
  checkIn?: string; // ISO date
  checkOut?: string; // ISO date
  page?: number;
  pageSize?: number;
}

export interface SearchResponse {
  total: number;
  page: number;
  pageSize: number;
  hotels: Hotel[];
}
