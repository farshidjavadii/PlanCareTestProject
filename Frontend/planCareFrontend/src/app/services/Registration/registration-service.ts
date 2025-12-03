// src/app/services/registration.service.ts
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';

export interface CarStatus { plate:string; isValid:boolean; registrationExpiry:string; }

@Injectable({providedIn:'root'})
export class RegistrationService {
  private hubConnection!: signalR.HubConnection;
  public statuses = new BehaviorSubject<CarStatus[]>([]);

  startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:8080/registrationHub')
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().then(()=>console.log('Connected'))
      .catch(err=>console.error(err));

    this.hubConnection.on('UpdateRegistration', (data: CarStatus[])=>{
      this.statuses.next(data);
    });
  }
}
