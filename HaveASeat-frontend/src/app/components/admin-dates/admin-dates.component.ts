import { NgIf } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin-dates',
  standalone: true,
  imports: [HttpClientModule, NgIf, FormsModule],
  templateUrl: './admin-dates.component.html',
  styleUrls: ['./admin-dates.component.css'] // Corrected property name
})
export class AdminDatesComponent {
  private addDate = 'https://localhost:7023/api/ForbiddenDate/AddForbiddenDate';
  private deleteDate = 'https://localhost:7023/api/ForbiddenDate/delete/{date}';
  private getallDates = 'https://localhost:7023/api/ForbiddenDate/getAllForbiddenDates';

  today: string;
  pickedDate: string | null = null;
  writtendescr: string | null = null;

  constructor(private http: HttpClient, private router: Router) {
    this.today = new Date().toISOString().split('T')[0];
    this.pickedDate = this.today;
  }

  CreateForbiddenDate(): void {
    if (this.pickedDate != null) {
      const dateObj = new Date(this.pickedDate);
      
      const day = String(dateObj.getDate()).padStart(2, '0');
      const month = String(dateObj.getMonth() + 1).padStart(2, '0');
      const year = dateObj.getFullYear();
      
      const forb_date = `${day}.${month}.${year}`;
      
      alert(forb_date);
      
      let descr = this.writtendescr;
      if (descr === null) {
        descr = "Dzien wolny";
      }

      const dateData = { description: descr, date: forb_date };

      this.http.post(this.addDate, dateData).subscribe({
        next: (response) => {
          alert('Forbidden date added');
        },
        error: (registerErr) => {
          console.error('Error adding forbidden date', registerErr);
        },
      });
    }
  }
}
