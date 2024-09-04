import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Room, Desk, Reservation, NewReservation } from '../models/models';

@Injectable({
  providedIn: 'root',
})
export class MapaService {

  private apiUrl = 'https://localhost:7023/api/Map/GetAllMap';
  private reservationApi = 'https://localhost:7023/api/Reservation/getByDay/';
  private addReservationUrl = "https://localhost:7023/api/Reservation/newReservation";
  private deleteReservationUrl = "https://localhost:7023/api/Reservation/delete/";


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


