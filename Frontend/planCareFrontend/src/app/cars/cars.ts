import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Cars as CarService, Car } from '../services/car/cars';
import { MatTableModule } from '@angular/material/table';
import { Observable, of, switchMap } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-cars',
  imports: [CommonModule, MatTableModule],
  templateUrl: './cars.html',
  styleUrl: './cars.css',
})
export class Cars implements OnInit {
  cars$: Observable<Car[]> = of([]);
  displayedColumns: string[] = ['id', 'make', 'model', 'plate', 'registrationExpiry'];
  constructor(private carService: CarService, private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.cars$ = this.route.queryParams.pipe(
      // ⬅️ استفاده از switchMap برای مدیریت تغییرات پارامتر کوئری
      switchMap(params => {
        const makeFilter = params['make']; // ⬅️ گرفتن پارامتر 'make'

        // ⬅️ ارسال پارامتر به متد سرویس
        return this.carService.getCars(makeFilter);
      })
    );
  }

}
