import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ForbiddenDate } from '../models/models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  constructor(private http:HttpClient) {}
  alldates: Date[] = [];
  private getAllUsersUrl = 'https://localhost:7023/api/Reservation/getAllUsersWithReservationByDay/';
  private getallDates = 'https://localhost:7023/api/ForbiddenDate/getAllForbiddenDates';
  find_today(today: string): string {
    let today_date = new Date(today);
    while (true) {
      let day = today_date.getDay();
      let counter = 0;
      if (day !== 6 && day !== 0) { 
        this.alldates.forEach((forb_date: Date) => {
          if (today_date.getTime() === forb_date.getTime()) {
            counter++;
          }
        });
        if (counter === 0) {
          return today_date.toISOString().slice(0, 10); 
        }
      }
      today_date.setDate(today_date.getDate() + 1);
    }
  }
}
