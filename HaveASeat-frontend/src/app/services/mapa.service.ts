import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Room, Desk, Reservation, NewReservation } from '../models/models';
import { Cell } from '../models/models';
@Injectable({
  providedIn: 'root',
})
export class MapaService {

  private apiUrl = 'https://localhost:7023/api/Map/GetAllMap';
  private reservationApi = 'https://localhost:7023/api/Reservation/getByDay/';
  private addReservationUrl = "https://localhost:7023/api/Reservation/newReservation";
  private deleteReservationUrl = "https://localhost:7023/api/Reservation/delete/";

  roomWidth = 20;
  roomHeight = 13;
  rooms: Room[] = [];
  clickedOnce = false;
  cellx: number = 0;
  celly: number = 0;
  previousCell: Cell | null = null;
  currentCell: Cell | null = null;
  addedDesks: Desk[] = [];
  removedDesks: Desk[] = [];
  currentRotation: number = 0;

  mapa: Room[] = []
  desks: Desk[] = []
  reservations: Reservation[] = [];

  constructor(private http: HttpClient) {

    this.http.get<Room[]>(this.apiUrl).subscribe({
      next: (dane: Room[]) => {
        this.mapa = dane;
      },
      error: (error) => {
        console.error('Błąd podczas pobierania danych:', error);
      }
    });
  }

  date = new Date();
  public loadReservations(date: string | undefined): Observable<Reservation[]> {
    const url = `${this.reservationApi}${date}`
    return this.http.get<Reservation[]>(url);

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

  check(desk: Desk, cell: Cell): boolean {
    return desk.positionX === cell.positionX && desk.positionY === cell.positionY;
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

  addReservation(reservation: NewReservation): Observable<NewReservation> {
    return this.http.post<NewReservation>(this.addReservationUrl, reservation);
  }

  getRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(this.apiUrl);
  }
  getDesks(): Observable<Desk[]> {
    return this.http.get<Desk[]>(this.apiUrl);
  }
  deleteReservationsById(userId: number): Observable<void> {
    return this.http.delete<void>(`${this.deleteReservationUrl}${userId}`);
  }
}


