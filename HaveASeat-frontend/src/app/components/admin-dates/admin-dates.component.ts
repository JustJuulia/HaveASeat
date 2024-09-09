import { NgIf } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, SimpleChanges } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ForbiddenDate } from '../../models/models';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-admin-dates',
  standalone: true,
  imports: [HttpClientModule, NgIf, FormsModule, NgFor],
  templateUrl: './admin-dates.component.html',
  styleUrls: ['./admin-dates.component.css']
})
export class AdminDatesComponent {
  private addDate = 'https://localhost:7023/api/ForbiddenDate/AddForbiddenDate/';
  private deleteDate = 'https://localhost:7023/api/ForbiddenDate/delete/{date}';
  private getallDates = 'https://localhost:7023/api/ForbiddenDate/getAllForbiddenDates';
  alldates: string[] = [];
  today: string;
  pickedDate: string | null = null;
  writtendescr: string | null = null;

  constructor(private http: HttpClient, private router: Router) {
    this.today = new Date().toISOString().split('T')[0];
    this.pickedDate = this.today;
  }
  ngOnInit(): void {
    this.ShowAllForbiddenDates();
  }
  ShowAllForbiddenDates() :void{
    this.http.get<ForbiddenDate[]>(this.getallDates).subscribe({
      next: (dates: ForbiddenDate[]) => {
        this.alldates = dates.map(date => 
          `${new Date(date.date).toISOString().split('T')[0]} - ${date.description}`
        );
      },
      error: (err) => {
        console.error('Error during dates show', err);
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
          alert('Forbidden date added');
          this.ShowAllForbiddenDates();
        },
        error: (error) => {
          console.error('Error adding forbidden date:', error);
          alert(`Error adding forbidden date: ${error.message}`);
        },
      });
    }
  }
  Delete_date(date : string): void{
    const [datePart, description] =date.split(' - ');
    const delete_url = this.deleteDate.replace('{date}', datePart);
    this.http.delete(delete_url).subscribe({
      next: () => {
        alert('Forbidden date deleted');
        this.ShowAllForbiddenDates();
      },
      error: (error) => {
        console.error('Error deleting forbidden date:', error);
        alert(`Error deleting forbidden date: ${error.message}`);
      },
    });
  }
}
