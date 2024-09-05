import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/models';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userByIdUrl = "https://localhost:7023/api/Authentication/GetById/"
  
  constructor(private http: HttpClient) {}

  getUserById(id :number) :Observable<User> {
    id = 6
    return this.http.get<User>(`${this.userByIdUrl}${id}`);
  }

}
