import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Car {
  id: number;
  make: string;
  model: string;
  plate: string;
  registrationExpiry: string;
}
export interface CarStatus {
  plate: string;
  isValid: boolean;
  expiry: string;
}


@Injectable({
  providedIn: 'root',
})
export class Cars {
  constructor(private http: HttpClient) { }
  getCars(make?: string): Observable<Car[]> {
    let url = 'https://localhost:44343/api/car'; if (make) url += `?make=${make}`;
    return this.http.get<Car[]>(url);
  }
}
