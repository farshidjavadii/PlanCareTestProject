import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegistrationService, CarStatus } from '../services/Registration/registration-service';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './registration.html',
  styleUrls: ['./registration.css'],
})

export class Registration implements OnInit {


  statuses: CarStatus[] = [];
  lastUpdated: Date | null = null;

  constructor(private regService: RegistrationService, 
    private cdr: ChangeDetectorRef) {}

  ngOnInit() {

    this.regService.startConnection();

    this.regService.statuses.subscribe(data => {
      console.log("Received from SignalR: ", data);
      this.statuses = data;
      this.lastUpdated = new Date();

      this.cdr.detectChanges();
    });
  }
}
