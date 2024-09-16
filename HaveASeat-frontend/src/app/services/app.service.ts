import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ForbiddenDate } from '../models/models';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  private getallDatesUrl = 'https://localhost:7023/api/ForbiddenDate/getAllForbiddenDates';

  constructor(private http: HttpClient) {}
  loadForbiddenDates(): Observable<Date[]> {
    return this.http.get<ForbiddenDate[]>(this.getallDatesUrl).pipe(
      map(dates => dates.map(date => new Date(date.date))),
      catchError(err => {
        console.error('Error loading forbidden dates:', err);
        return of([]);
      })
    );
  }

  find_today(today: string): Observable<string> {
    return this.loadForbiddenDates().pipe(
      map(alldates => {
        let todayDate = new Date(today);
        while (true) {
          const day = todayDate.getDay();
          if (day !== 6 && day !== 0) { 
            const isForbidden = alldates.some(forbDate => todayDate.getTime() === forbDate.getTime());
            if (!isForbidden) {
              return todayDate.toISOString().slice(0, 10);
            }
          }
          todayDate.setDate(todayDate.getDate() + 1);
        }
      })
    );
  }
  
}
