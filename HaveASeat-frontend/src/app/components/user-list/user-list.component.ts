import { Component, Input, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { AppService } from '../../services/app.service';
import { User, ForbiddenDate } from '../../models/models';
import { NgIf, NgFor } from '@angular/common';
import { MyDialogComponent } from '../mydialog/mydialog.component';
import { MapaService } from '../../services/mapa.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { delay } from 'rxjs';
@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [HttpClientModule, MatDialogModule, MatButtonModule, NgIf, NgFor, MyDialogComponent],
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss'],
  providers: [AppService, MapaService]
})
export class UserListComponent implements OnInit {
  today: string;
  users_list: { name: string, reservationId: number }[] = [];
  alldates: Date[] = [];

  @Input() this_user: string | null = null;
  this_user_reservationId: number | null = null;

  private getAllUsersUrl = 'https://localhost:7023/api/Reservation/getAllUsersWithReservationByDay/';
  private getallDatesUrl = 'https://localhost:7023/api/ForbiddenDate/getAllForbiddenDates';

  constructor(
    private http: HttpClient,
    private router: Router,
    private dialog: MatDialog,
    private appservice: AppService,
    private mapaservice: MapaService,
    private snackBar: MatSnackBar
  ) {
    this.today = new Date().toISOString().split('T')[0];
  }

  ngOnInit(): void {
    this.http.get<ForbiddenDate[]>(this.getallDatesUrl).subscribe({
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
            console.error('Error finding today\'s reservations', err);
          }
        });
      },
      error: (err: any) => {
        console.error('Error fetching forbidden dates', err);
      }
    });
  }
  isThisUserReserved(): boolean {
    return this.this_user !== null && this.this_user_reservationId !== null;
  }
  getUsers(date: string): void {
    const url = `${this.getAllUsersUrl}${date}`;
    this.http.get<User[]>(url).subscribe({
      next: (users: User[]) => {
        this.users_list = users.map(user => ({
          name: `${user.name} ${user.surname}`,
          reservationId: user.reservationId 
        }));
        const currentUser = users.find(user => `${user.name} ${user.surname}` === this.this_user);
        if (currentUser) {
          this.this_user_reservationId = currentUser.reservationId;
        }
      },
      error: (err) => {
        console.error('Error fetching users with reservations', err);
      },
    });
  }
  dialogopen(content: string, buttons: number): Promise<boolean> {
    const dialogRef = this.dialog.open(MyDialogComponent, {
      data: { buttonamount: buttons, mycontent: `${content}` }
    });
    return dialogRef.afterClosed().toPromise().then(result => {
      if (result === 'confirmed') {
        return true
      } else if (result === 'cancelled') {
        return false
      }
      else {
        return false
      }
    });
  }
  popup(type: number, text: string) {
    if (type == 0) {
      this.snackBar.open(text, "Zamknij", {
        duration: 2500,
        panelClass: ['snackbar'],
        verticalPosition: 'top',
      });
    }
    if (type == 1) {
      this.snackBar.open(text, "Zamknij", {
        duration: 3500,
        panelClass: ['snackbar'],
        verticalPosition: 'top',
      });
    }
    
  }
  async Cancel_Reservation(reservation_id: number | null){
    if(reservation_id == null){
      console.log('mi bomboclaaat')
    }
    else{
      if(await this.dialogopen("Cancel this reservation?", 2)){
        this.mapaservice.deleteReservationsById(reservation_id).subscribe({
          next: () => {
            this.popup(0, "Anulowano rezerwację");
            setTimeout(() => {
              window.location.reload();
            }, 1000);
          },
          error: deleteError => {
            console.error("Cancelation failed:", deleteError);
          }
        });
      }
    }
  }
}
