import { NgIf } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, SimpleChanges, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ForbiddenDate } from '../../models/models';
import { NgFor } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RightsideComponent } from '../rightside/rightside.component';
@Component({
  selector: 'app-admin-dates',
  standalone: true,
  imports: [HttpClientModule, NgIf, FormsModule, NgFor, RightsideComponent],
  templateUrl: './admin-dates.component.html',
  styleUrls: ['./admin-dates.component.scss']
})
export class AdminDatesComponent {
  private addDate = 'https://localhost:7023/api/ForbiddenDate/AddForbiddenDate/';
  private deleteDate = 'https://localhost:7023/api/ForbiddenDate/delete/{date}';
  private getallDates = 'https://localhost:7023/api/ForbiddenDate/getAllForbiddenDates';
  private updateDate= 'https://localhost:7023/api/ForbiddenDate/EditForbiddenDate';
  alldates: string[] = [];
  today: string;
  editedDate: string | null = null; 
  pickedDate: string | null = null;
  writtendescr: string | null = null;
  @Input() userId: number | null = null;
  @Input() pageSwitch: number = 2;
  isEditing = false;
  newdesc: string = '';


  constructor(private http: HttpClient, private router: Router, private snackBar: MatSnackBar) {
    this.today = new Date().toISOString().split('T')[0];
    this.pickedDate = this.today;
  }
  ngOnInit(): void {
    this.ShowAllForbiddenDates();
  }
  popup(type: number, text: string) {
    if(type == 0) {
      this.snackBar.open(text, "Zamknij", {
        duration: 3500,
        panelClass: ['success'],
        verticalPosition: 'top',
      });
    }
    if(type == 1) {
      this.snackBar.open(text, "Zamknij", {
        duration: 3500,
        panelClass: ['failure'],
        verticalPosition: 'top',
      });
    }
  }
  Change_date(date: string): void {
    this.editedDate = date; 
  }
  Save_date(date: string) {
    let descrChangeElement = document.getElementById('edited_text') as HTMLInputElement;
    let descrChange: string;

    if (descrChangeElement === null) {
        console.log('descrChangeElement not found, setting description to "Dzien wolny"');
        descrChange = "Dzien wolny";
    } else {
        if (descrChangeElement.value.trim().length === 0) {
            descrChange = "Dzien wolny";
        } else {
            descrChange = descrChangeElement.value || "Dzien wolny";
        }
    }
    if (!date.includes(' - ')) {
        console.error("Date string doesn't contain ' - '. Expected format: 'YYYY-MM-DD - description'");
        return; 
    }
    const [datePart, description] = date.split(' - ');
    const dateObj = new Date(datePart);
    if (isNaN(dateObj.getTime())) {
        console.error('Invalid date format:', datePart);
        return;
    }
    const isoDate = dateObj.toISOString().split('T')[0];
    const dateData = { description: descrChange, date: isoDate };
    console.log('Sending data:', dateData);
    this.http.post(this.updateDate, dateData).subscribe({
        next: (response) => {
            console.log('Date successfully updated:', response);
            this.popup(0, "Dzień wolny zmieniony");
            this.ShowAllForbiddenDates();
        },
        error: (error) => {
            console.error('Error changing forbidden date:', error);
            this.popup(1, "Błąd w zmienianiu dnia wolnego");
        },
    });

    this.editedDate = null;
}
ShowAllForbiddenDates(): void {
  this.http.get<ForbiddenDate[]>(this.getallDates).subscribe({
    next: (dates: ForbiddenDate[]) => {
      if (dates && dates.length > 0) {
        this.alldates = dates.map(date =>
          `${new Date(date.date).toISOString().split('T')[0]} - ${date.description}`
        );
      } else {
        this.alldates = ['No forbidden dates available.'];
      }
    },
    error: (err) => {
      console.error('Error during dates show', err);
      this.alldates = ['Error fetching forbidden dates.'];
    },
  });
}

  CreateForbiddenDate(): void {
    if (this.pickedDate != null) {
      const dateObj = new Date(this.pickedDate);
      const isoDate = dateObj.toISOString().split('T')[0];
      let descr = this.writtendescr;
      if (descr === null) {
        descr = "Dzien wolny";
      }
      const dateData = { description: descr, date: isoDate };
      this.http.post(this.addDate, dateData).subscribe({
        next: (response) => {
          this.popup(0, "Dzień wolny dodany");
          this.ShowAllForbiddenDates();
        },
        error: (error) => {
          console.error('Error adding forbidden date:', error);
          this.popup(1, "Błąd w dodawaniu dnia wolnego");
        },
      });
    }
  }
  Delete_date(date : string): void{
    const [datePart, description] =date.split(' - ');
    const delete_url = this.deleteDate.replace('{date}', datePart);
    this.http.delete(delete_url).subscribe({
      next: () => {
        this.popup(0, "Dzień wolny usunięty")
        this.ShowAllForbiddenDates();
      },
      error: (error) => {
        console.error('Error deleting forbidden date:', error);
        this.popup(1, "Błąd w usuwaniu dnia wolnego");
      },
    });
  }
  
}
