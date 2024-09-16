import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from '../../models/models';
import { ForbiddenDate } from '../../models/models';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MyDialogComponent } from '../mydialog/mydialog.component';
import { AppService } from '../../services/app.service';
@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [HttpClientModule, NgIf, NgFor, MatDialogModule, MatButtonModule],
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss'],
  providers: [AppService]
})
export class UserListComponent implements OnInit {
  today: string;
  users_list: string[] = [];
  alldates: Date[] = [];

  private getAllUsersUrl = 'https://localhost:7023/api/Reservation/getAllUsersWithReservationByDay/';
  private getallDates = 'https://localhost:7023/api/ForbiddenDate/getAllForbiddenDates';

  constructor(private http: HttpClient, private router: Router, private dialog: MatDialog, private appservice: AppService) {
    this.today = new Date().toISOString().split('T')[0];
  }
  ngOnInit(): void {
    this.http.get<ForbiddenDate[]>(this.getallDates).subscribe({
      next: (dates: ForbiddenDate[]) => {
        this.alldates = dates.map(date => 
          new Date(date.date)
        );
        const today = new Date().toISOString().slice(0, 10);
        const checked_today = this.appservice.find_today(today);
        this.getUsers(checked_today);
      },
      error: (err: any) => {
        console.error('Error during fetching forbidden dates', err);
      },
    });
  }
  getUsers(date: string): void {
    const checked_today = this.appservice.find_today(date);
    const url = `${this.getAllUsersUrl}${checked_today}`;
    this.http.get<User[]>(url).subscribe({
      next: (users: User[]) => {
        this.users_list = users.map(user => `${user.name} ${user.surname}`);
      },
      error: (err) => {
        console.error('Error during fetching users', err);
      },
    });
  }
}