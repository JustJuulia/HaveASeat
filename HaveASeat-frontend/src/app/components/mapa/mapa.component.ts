import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule, NgFor, NgStyle } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Desk, Room, Cell, Reservation, User, NewReservation } from '../../models/models';
import { NgIf } from '@angular/common';
import { forkJoin } from 'rxjs';
import { HeaderComponent } from '../header/header.component';
import { UserService } from '../../services/user.service';
import { MapaService } from '../../services/mapa.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient } from '@angular/common/http';
import { ForbiddenDate } from '../../models/models';
import { provideProtractorTestingSupport } from '@angular/platform-browser';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MyDialogComponent } from '../mydialog/mydialog.component';
import { AppService } from '../../services/app.service';
@Component({
  selector: 'app-mapa',
  templateUrl: './mapa.component.html',
  styleUrls: ['./mapa.component.scss'],
  imports: [NgStyle, NgFor, HttpClientModule, NgIf, CommonModule, HeaderComponent, MatDialogModule, MatButtonModule],
  standalone: true,
  providers: [MapaService, UserService, AppService],
})
export class MapaComponent implements OnInit, OnChanges {

  roomWidth = 20;
  roomHeight = 13;
  rooms: Room[] = [];
  reservations: Reservation[] = [];
  clickedOnce = false;
  @Input() selectedDate: string = '';
  @Input() userId: number | null = null;
  private getallDates = 'https://localhost:7023/api/ForbiddenDate/getAllForbiddenDates';
  alldates: Date[] = [];

  constructor(private mapaService: MapaService, private snackBar: MatSnackBar, private http: HttpClient, private dialog: MatDialog, private appservice:AppService) { }

  ngOnInit(): void {
    this.http.get<ForbiddenDate[]>(this.getallDates).subscribe({
      next: (dates: ForbiddenDate[]) => {
        this.alldates = dates.map(date =>
          new Date(date.date)
        );
      },
      error: (err: any) => {
        console.error('Error during dates show', err);
      },
    });
    forkJoin({
      rooms: this.mapaService.getRooms(),
    }).subscribe(
      ({ rooms }) => {
        this.rooms = rooms;
        this.markDeskCells();
        const today = new Date().toISOString().slice(0, 10);
        const checked_today = this.appservice.find_today(today);
        const date = checked_today;
        this.markReserved(date);
      },
      (error: any) => {
        console.error('Error fetching data:', error);
      }
    );
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['selectedDate']) {
      this.markReserved(changes['selectedDate'].currentValue);
    }
  }

  popup(type: number, text: string) {
    if (type == 0) {
      this.snackBar.open(text, "Zamknij", {
        //duration: 3500,
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

  checkIfReserved(reservation: Reservation, cell: Cell): boolean {
    return reservation.desk.positionX == cell.positionX && reservation.desk.positionY == cell.positionY;
  }

  checkIfBelongsToUser(reservation: Reservation, cell: Cell): boolean {
    return (reservation.desk.positionX == cell.positionX && reservation.desk.positionY == cell.positionY) && reservation.user.id == this.userId;
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
  async onDeskClick(cell: Cell) {
    if (cell.isDesk && !cell.isReserved) {
      console.log('not reserved free desk')
      this.rooms.forEach(room => {
        room.cells.forEach(c => {
          c.isClicked = false;
        });
      });
    }
    if (cell.isDesk && !cell.isReserved && !cell.isUsers && await this.dialogopen("Book this seat?", 2)) {
      cell.isClicked = true;
      const desk = this.getCellsDesk(cell);

      const newReservation: NewReservation = {
        date: this.selectedDate || new Date().toJSON().slice(0, 10),
        userId: this.userId !== null ? this.userId : -1,
        deskId: desk.id
      };

      if (this.selectedDate) {
        newReservation.date = this.selectedDate;
      }

      this.mapaService.addReservation(newReservation).subscribe({
        next: response => { },
        complete: () => {
          this.markReserved(newReservation.date);
          this.popup(0, "Zarezerwowano");
          var usersReservations = this.reservations.filter(r => r.user.id == this.userId);
          if (usersReservations.length) {
            usersReservations.forEach(reservation => {
              this.mapaService.deleteReservationsById(reservation.id).subscribe({
                next: deleteResponse => {
                  this.markReserved(newReservation.date);
                },
                error: deleteError => {
                  console.error("delete failed:", deleteError);
                }
              });
            });
          }
        },
        error: error => {
          console.error("Reservation failed:", error);
        }
      });
    }
    else if (cell.isUsers && await this.dialogopen("Cancel this reservation?", 2)) {
      const reservation = this.reservations.find(r => r.user.id === this.userId && r.desk.positionX === cell.positionX && r.desk.positionY === cell.positionY);

      if (reservation) {
        this.mapaService.deleteReservationsById(reservation.id).subscribe({
          next: () => {
            this.markReserved(reservation.date);
            this.popup(0, "Anulowano rezerwację");
          },
          error: deleteError => {
            console.error("Cancelation failed:", deleteError);
          }
        });
      }
    }
    else if (cell.isReserved) {
      const reservation = this.reservations.find(r => r.desk.positionX === cell.positionX && r.desk.positionY === cell.positionY);
      let text = `Miejsce zajęte przez: ${reservation?.user.name}`;
      this.dialogopen(text, 1);

    } else {
      console.log("No action taken");
    }
  }


  getCellsDesk(cell: Cell): Desk {
    for (const room of this.rooms) {
      for (const desk of room.desks) {
        if (desk.positionX === cell.positionX && desk.positionY === cell.positionY) {
          return desk;
        }
      }
    }
    throw new Error('Desk not found for the given cell');
  }

  getDesksCell(desk: Desk): Cell {
    for (const room of this.rooms) {
      for (const cell of room.cells) {
        if (desk.positionX === cell.positionX && desk.positionY === cell.positionY) {
          return cell;
        }
      }
    }
    throw new Error('Cell not found for the given desk');
  }

  markReserved(date: string) {
    forkJoin({
      reservations: this.mapaService.loadReservations(date)
    }).subscribe(
      ({ reservations }) => {
        this.reservations = reservations;
        if (this.reservations.length) {
          this.rooms.forEach(room => {
            room.cells.forEach(cell => {
              if (this.reservations.some(reservation => this.checkIfBelongsToUser(reservation, cell))) {
                cell.isUsers = true;
                cell.isReserved = false;
                cell.isClicked = false;
              }
              else {
                cell.isReserved = this.reservations.some(reservation => this.checkIfReserved(reservation, cell));
                cell.isUsers = false;
                cell.isClicked = false;
              }
            })
          })
        } else {
          this.rooms.forEach(room => {
            room.cells.forEach(cell => {
              cell.isReserved = false;
              cell.isUsers = false;
              cell.isClicked = false;
            })
          })
        }
      },
      (error: any) => {
        console.error('Error fetching data:', error);
      }
    );
  }

  check(desk: Desk, cell: Cell): boolean {
    return desk.positionX === cell.positionX && desk.positionY === cell.positionY;
  }
  getRotationClass(chairPosition: number): string {

    switch (chairPosition) {
      case 1:
        return 'rotate-right';
      case 2:
        return 'rotate-bottom';
      case 3:
        return 'rotate-left';
      default:
        return '';
    }
  }

  markDeskCells(): void {
    if (this.rooms.length) {
      this.rooms.forEach(room => {
        room.cells.forEach(cell => {
          cell.isDesk = room.desks.some(desk => this.check(desk, cell));

          if (cell.isDesk) {
            const desk = room.desks.find(desk => this.check(desk, cell));
            if (desk) {
              cell.rotationClass = this.getRotationClass(desk.chairPosition);
            }
          } else {
          }
        });
      });
    }
  }

  getBorder(roomId: number, positionY: number, positionX: number): string {
    const room = this.rooms.find(r => r.id === roomId);
    const cell = room?.cells.find(c => c.positionX === positionX && c.positionY === positionY);
    return cell?.border ?? '';
  }

  isCellPresent(roomId: number, positionX: number, positionY: number): boolean {
    const room = this.rooms.find(r => r.id === roomId);
    return room?.cells.some(cell => cell.positionX === positionX && cell.positionY === positionY) ?? false;
  }

  getRange(n: number): number[] {
    return Array.from({ length: n }, (_, index) => index);
  }


}

//𝓯𝓻𝓮𝓪𝓴𝔂 