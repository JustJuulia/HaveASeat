import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from '../../models/models';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [HttpClientModule, NgIf, NgFor],
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  today: string;
  users_list: string[] = [];

  private getAllUsersUrl = 'https://localhost:7023/api/Reservation/getAllUsersWithReservationByDay/';

  constructor(private http: HttpClient, private router: Router) {
    this.today = new Date().toISOString().split('T')[0];
  }

  ngOnInit(): void {
    this.getUsers(this.today);
  }

  getUsers(date: string): void {
    const url = `${this.getAllUsersUrl}${date}`;
    this.http.get<User[]>(url).subscribe({
      next: (users: User[]) => {
        this.users_list = users.map(user => `${user.name} ${user.surname}`);
      },
      error: (err) => {
        console.error('Error during dates show', err);
      },
    });
  }
}
