import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule, CurrencyPipe, NgFor, NgStyle } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Desk, Room, Cell, Reservation, AddDesk } from '../../models/models';
import { NgIf } from '@angular/common';
import { forkJoin } from 'rxjs';
import { HeaderComponent } from '../header/header.component';
import { UserService } from '../../services/user.service';
import { MapaService } from '../../services/mapa.service';

@Component({
  selector: 'app-editor-map',
  templateUrl: './editor-map.component.html',
  styleUrl: './editor-map.component.css',
  imports: [NgStyle, NgFor, HttpClientModule, NgIf, CommonModule, HeaderComponent],
  standalone: true,
  providers: [MapaService, UserService],
})
export class EditorMapComponent implements OnInit, OnChanges {
  roomWidth = 20;
  roomHeight = 13;
  rooms: Room[] = [];
  reservations: Reservation[] = [];
  clickedOnce = false;
  cellx: number = 0;
  celly: number = 0;
  previousCell: Cell | null = null;
  currentCell: Cell | null = null;
  addedDesks: Desk[] = [];
  removedDesks: Desk[] = [];
  currentRotation: number = 0;
  @Input() selectedDate: string = '';
  @Input() userId: number | null = null;

  constructor(private mapaService: MapaService, private http: HttpClient) { }
  private deleteDesk_url = 'https://localhost:7023/api/Map/DeleteDesk';
  private addDesk_url = 'https://localhost:7023/api/Map/AddNewDesk';
  ngOnInit(): void {
    forkJoin({
      rooms: this.mapaService.getRooms(),
    }).subscribe(
      ({ rooms }) => {
        this.rooms = rooms;
        this.markDeskCells();
      },
      (error: any) => {
        console.error('Error fetching data:', error);
      }
    );
  }

  ngOnChanges(changes: SimpleChanges): void {
  }

  onDeskClick(cell: Cell) {
    if (this.previousCell != null) {
      this.previousCell.isClicked = false;
    }
    this.currentCell = cell;
    this.cellx = cell.positionX;
    this.celly = cell.positionY;
    cell.isClicked = true;
    this.previousCell = cell;
  }

  addDesk() {
    if (this.currentCell != null) {
      this.currentCell.isDesk = true;
      if (!this.currentCell.isDeleted) {
        this.currentCell.isNew = true;
      }
      this.currentCell.rotationClass = this.getRotationClass(this.currentRotation);
      const deskData = { positionX: this.currentCell.positionX, positionY: this.currentCell.positionY, chairPosition: this.currentRotation };

      this.http.post(this.addDesk_url, deskData).subscribe({
        next: (response) => {
          alert('added desk');
        },
        error: (registerErr) => {
          console.error('Error while adding', registerErr);
          alert('error while adding desk oopsie');
        },
      });
      this.currentCell.isDeleted = false;
    }
  }

  removeDesk() {
    if (this.currentCell != null && this.currentCell.isDesk) {
      this.currentCell.isDesk = false;
      if (!this.currentCell.isNew) {
        const deleteData = {
          positionX: this.currentCell.positionX,
          positionY: this.currentCell.positionY,
          chairPosition: 0
        };
        const httpOptions = {
          headers: { 'Content-Type': 'application/json' },
          body: deleteData
        };
        this.http.delete(this.deleteDesk_url, httpOptions).subscribe({
          next: () => {
            alert('Desk deleted successfully');
            if (this.currentCell != null) {
              this.currentCell.isDeleted = true;
            }
          },
          error: (error) => {
            console.error('Error deleting desk:', error);
            alert(`Error deleting desk: ${error.message}`);
          },
        });
      } else {
        this.currentCell.isNew = false;
      }
    }
  }

  rotate(rotation: number) {
    this.currentRotation = rotation;
    console.log(this.currentRotation);
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


}