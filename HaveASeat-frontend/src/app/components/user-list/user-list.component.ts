import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from '../../models/models';
import { ForbiddenDate } from '../../models/models';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MyDialogComponent } from '../mydialog/mydialog.component';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [HttpClientModule, NgIf, NgFor, MatDialogModule, MatButtonModule],
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  today: string;
  users_list: string[] = [];
  alldates: Date[] = [];

  private getAllUsersUrl = 'https://localhost:7023/api/Reservation/getAllUsersWithReservationByDay/';
  private getallDates = 'https://localhost:7023/api/ForbiddenDate/getAllForbiddenDates';

  constructor(private http: HttpClient, private router: Router, private dialog: MatDialog) {
    this.today = new Date().toISOString().split('T')[0];
  }

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

  ngOnInit(): void {
    this.http.get<ForbiddenDate[]>(this.getallDates).subscribe({
      next: (dates: ForbiddenDate[]) => {
        this.alldates = dates.map(date => 
          new Date(date.date)
        );
        const today = new Date().toISOString().slice(0, 10);
        const checked_today = this.find_today(today);
        this.getUsers(checked_today);
      },
      error: (err: any) => {
        console.error('Error during fetching forbidden dates', err);
      },
    });
  }
  openDialog(): void {
    this.dialog.open(MyDialogComponent, {
      data: { mycontent: 'Super test!!!!' } 
    });
  }
  getUsers(date: string): void {
    const checked_today = this.find_today(date);
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
