import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ForbiddenDate } from '../models/models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DateService {

  private addUrl = "https://localhost:7023/api/ForbiddenDate/AddForbiddenDate";
  private deleteUrl = "https://localhost:7023/api/ForbiddenDate/delete/";
  private getAllUrl = "https://localhost:7023/api/ForbiddenDate/getAllForbiddenDates";
  private getByDateUrl = "https://localhost:7023/api/ForbiddenDate/GetByDate/";
  private getByIdUrl = "https://localhost:7023/api/ForbiddenDate/GetById";

  dates: ForbiddenDate[] = []

  constructor(private http:HttpClient) {}

  getDates(): Observable<ForbiddenDate[]> {
    return this.http.get<ForbiddenDate[]>(this.getAllUrl);
  }   
}
