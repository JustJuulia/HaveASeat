import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule, NgFor, NgStyle } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Desk, Room, Cell, Reservation, User, NewReservation } from '../../models/models';
import { NgIf } from '@angular/common';
import { DialogComponent } from '../dialogue-component/dialogue-component.component';
import { forkJoin } from 'rxjs';
import { HeaderComponent } from '../header/header.component';
import { UserService } from '../../services/user.service';
import { MapaService } from '../../services/mapa.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { provideProtractorTestingSupport } from '@angular/platform-browser';
import { ViewChild } from '@angular/core';

@Component({
  selector: 'app-mapa',
  templateUrl: './mapa.component.html',
  styleUrls: ['./mapa.component.scss'],
  imports: [NgStyle, NgFor, HttpClientModule, NgIf, CommonModule, HeaderComponent, DialogComponent],
  standalone: true,
  providers: [MapaService, UserService],
})
export class MapaComponent implements OnInit, OnChanges {

  @ViewChild('dialog') dialogComponent!: DialogComponent;
  roomWidth = 20;
  roomHeight = 13;
  rooms: Room[] = [];
  reservations: Reservation[] = [];
  clickedOnce = false;
  @Input() selectedDate: string = ''; 
  @Input() userId: number | null = null;

  constructor(private mapaService: MapaService, private snackBar: MatSnackBar) {}

  showDialog = false;
  actionSeat = 0;

  async openDialog(type: number): Promise<boolean> {
    let message = '';
    if (type === 1) {
      message = 'Book this seat?';
    } else if (type === 0) {
      message = 'Cancel this seat?';
    }
  
    try {
      const result = await this.dialogComponent.openDialog(message);
      return result;
    } catch (error) {
      console.error('Dialog error:', error);
      return false;
    }
  }

  handleConfirm() {
    console.log('Confirmed');
    this.showDialog = false;
  }

  handleClose() {
    console.log('Closed');
    this.showDialog = false;
  }

  ngOnInit(): void {
    forkJoin({
      rooms: this.mapaService.getRooms(),
    }).subscribe(
      ({ rooms }) => {
        this.rooms = rooms;
        this.markDeskCells();
        const today = new Date();
        const date = today.toJSON().slice(0, 10);
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
    if(type == 0) {
      this.snackBar.open(text, "Zamknij", {
        duration: 3500,
        panelClass: ['success']
      });
    }
    if(type == 1) {
      this.snackBar.open(text, "Zamknij", {
        duration: 3500,
        panelClass: ['failure']
      });
    }
  }

  checkIfReserved(reservation: Reservation, cell: Cell): boolean {
    return reservation.desk.positionX == cell.positionX && reservation.desk.positionY == cell.positionY;
  }

  checkIfBelongsToUser(reservation: Reservation, cell: Cell): boolean {
    return (reservation.desk.positionX == cell.positionX && reservation.desk.positionY == cell.positionY) && reservation.user.id == this.userId;
  }

  async onDeskClick(cell: Cell) {
    if (cell.isDesk && !cell.isReserved) {
      this.rooms.forEach(room => {
        room.cells.forEach(c => {
          c.isClicked = false;
        });
      });
    }
    if (cell.isUsers == false && !cell.isReserved && await this.openDialog(1)) {
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
        next: response => {},
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
    else if (cell.isUsers && await this.openDialog(0)) {
      const reservation = this.reservations.find(r => r.user.id == this.userId);
      if (reservation) {
        this.mapaService.deleteReservationsById(reservation.id).subscribe({
          complete: () => {
            this.markReserved(reservation.date);
            this.popup(0, "Anulowano rezerwację");
          },
          error: deleteError => {
            console.error("delete failed:", deleteError);
          }
        });
      }   
    }
    else if(cell.isReserved) {
      const reservation = this.reservations.find(r => r.desk.positionX == cell.positionX && r.desk.positionY == cell.positionY);
      this.popup(0, `Biurko zarezerwowane przez ${reservation?.user.name}`);
    }
    else {
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

  getDesksCell(desk :Desk) :Cell {
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
        if(this.reservations.length) {
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