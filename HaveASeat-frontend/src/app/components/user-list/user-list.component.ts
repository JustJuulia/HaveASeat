import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { User, ForbiddenDate } from '../../models/models';
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

  constructor(
    private http: HttpClient,
    private router: Router,
    private dialog: MatDialog,
    private appservice: AppService
  ) {
    this.today = new Date().toISOString().split('T')[0];
  }

  ngOnInit(): void {
    this.http.get<ForbiddenDate[]>(this.getallDates).subscribe({
      next: (dates: ForbiddenDate[] | null) => {
        if (dates) {
          this.alldates = dates.map(date => new Date(date.date));
        } else {
          this.alldates = [];
        }
        this.appservice.find_today(this.today).subscribe({
          next: (checked_today: string) => {
            this.getUsers(checked_today);
          },
          error: (err) => {
            console.error('Error during finding today', err);
          }
        });
      },
      error: (err: any) => {
        console.error('Error during fetching forbidden dates', err);
      }
    });
    
  }

  getUsers(date: string): void {
    const url = `${this.getAllUsersUrl}${date}`;
    this.http.get<User[]>(url).subscribe({
      next: (users: User[]) => {
        console.log('user!1')
        this.users_list = users.map(user => `${user.name} ${user.surname}`);
      },
      error: (err) => {
        console.error('Error during fetching users', err);
      },
    });
  }
}
