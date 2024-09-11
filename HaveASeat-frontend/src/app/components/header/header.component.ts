import { Component, EventEmitter, Input, OnChanges, Output, AfterViewInit, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { User } from '../../models/models';
import { NgIf } from '@angular/common';
import { Router } from '@angular/router';
import { ForbiddenDate } from '../../models/models';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [HttpClientModule, NgIf],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'], 
  providers: [UserService]
})
export class HeaderComponent implements OnChanges, AfterViewInit, OnInit {

  @Input() userId: number | null = null;
  @Input() pageSwitch: number = 0;
  @Output() dateChanged = new EventEmitter<string>();


  today: string;
  username: string = "";
  usersurname: string = "Jan Wieprzowina";
  user: User = <User>{};
  alldates: Date[] = [];

  constructor(private userService: UserService, private router: Router, private http: HttpClient, private snackBar: MatSnackBar) {
    this.today = new Date().toISOString().slice(0, 10);
  }
  private getallDates = 'https://localhost:7023/api/ForbiddenDate/getAllForbiddenDates';
  private getforbDate = 'https://localhost:7023/api/ForbiddenDate/GetByDate';

  ngOnInit(): void {
    this.http.get<ForbiddenDate[]>(this.getallDates).subscribe({
      next: (dates: ForbiddenDate[]) => {
        this.alldates = dates.map(date => 
          new Date(date.date)
        );
        const emit_date = this.nearestworkday(this.today);
        this.today = emit_date;
        this.dateChanged.emit(emit_date);
      },
      error: (err) => {
        console.error('Error during dates show', err);
      },
    });
  }
  
  ngOnChanges() {
    if (this.userId !== null) {
      console.log('Fetching user with ID:', this.userId);
      this.userService.getUserById(this.userId).subscribe({
        next: userData => {
          console.log('Received user data:', userData);
          this.user = userData;
          this.username = this.user.name;
          this.usersurname = this.user.surname
        },
        error: err => {
          console.error('Failed to fetch user data:', err);
        }
      });
    } else {
      console.warn('userId is null');
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
  nearestworkday(today: string): string {
    let today_date = new Date(today);
    while (true) {
      let day = today_date.getDay();
      let counter = 0;
      if (day !== 6 && day !== 0) { 
        this.alldates.forEach(forb_date => {
          if (today_date.getTime() === forb_date.getTime()) {
            counter++;
          }
        });
        if (counter === 0) {
          return today_date.toISOString().slice(0, 10); 
        }
      }
      today_date.setDate(today_date.getDate() + 1); 
    }
  }
  
  setCalendarRestrictions(): void {
    const today = new Date();
    const todayStr = today.toISOString().split('T')[0];
    const twoWeeksFromToday = new Date(today);
    twoWeeksFromToday.setDate(today.getDate() + 14);
    const twoWeeksFromTodayStr = twoWeeksFromToday.toISOString().split('T')[0];
    // trzy linijku up zamien na te 3 jezeli trzeba zmienic restrictions na miesiac:
    /*
    const nextMonth = new Date(today);
    nextMonth.setMonth(nextMonth.getMonth() + 1);
    const nextMonthStr = nextMonth.toISOString().split('T')[0];
    */
    const datePicker = document.getElementById('datecalendar') as HTMLInputElement;
    if (datePicker) {
      datePicker.setAttribute('min', todayStr);
      datePicker.setAttribute('max', twoWeeksFromTodayStr);
      // i ta nad zmien na
      //datePicker.setAttribute('max', nextMonthStr);
    }
  }
  

  ngAfterViewInit() {
    const acc = document.getElementById("user") as HTMLElement;
    if (acc) {
      const elements = document.getElementsByClassName("menu_accordeon");

      for (let i = 0; i < elements.length; i++) {
        const element = elements[i] as HTMLElement;
        element.addEventListener("mouseover", function () {
          this.classList.toggle("active");
          const panel = this.nextElementSibling as HTMLElement;
          if (panel) {
            if (panel.style.maxHeight) {
              panel.style.maxHeight = "";
            } else {
              panel.style.maxHeight = panel.scrollHeight + "px";
            }
          }
        });
      }
    }
    this.setCalendarRestrictions();
  }
  
  onDateChange(event: any): void {
    const selectedDate = new Date(event.target.value);
    const day = selectedDate.getDay();
    const formattedSelectedDate = selectedDate.toISOString().slice(0, 10);
    const isForbiddenDate = this.alldates.some(date => date.toISOString().slice(0, 10) === formattedSelectedDate);
  
    if (isForbiddenDate) {
      const url = `${this.getforbDate}/${formattedSelectedDate}`;
      this.http.get<ForbiddenDate>(url).subscribe({
        next: (date: ForbiddenDate) => {
          this.popup(0, date.description);
        },
        error: (err) => {
          console.error('Error during dates show', err);
        },
      });
      let emit_date = this.nearestworkday(this.today);
      event.target.value = emit_date;
      this.dateChanged.emit(emit_date);
      return;
    }
  
    if (day === 6 || day === 0) { // Check if the date is Saturday or Sunday
      this.popup(0, "Weekend");
      let emit_date = this.nearestworkday(this.today);
      event.target.value = emit_date;
      this.dateChanged.emit(emit_date);
    } else {
      this.dateChanged.emit(event.target.value);
    }
  }
  

  // ten checkifadmin wiem ze jest dziwinie bo jak admin to false ale musialam tak zrb bo
  //normalnie nie dzialalo wiec po prostu jak wywoluje to neguje ta funkcje sori
  checkifAdmin() {
    if (this.user.role == 1) {
      return true;
    }
    if (this.user.role == 0) {
      return false;
    }
    else {
      console.log(this.user.role);
      return true;
    }
  }
  GoToEdition() {
    if (this.userId !== null) {
      this.router.navigate(['editor'], { queryParams: { userId: this.userId } });
    } else {
      console.warn('User ID is null. Cannot navigate to editor.');
      this.popup(1, "Użytkownik nie istnieje, nie można przejść do edytora")
    }
  }
  goToAdminPanel() {
    if (this.user.role == 0) {
      this.router.navigate(['admin-panel'], {queryParams: {userId: this.userId}});
    }
    else {
      console.warn('wuad da heallw')
    }
  }
  goToMap() {
    this.router.navigate(['main'], {queryParams: {userId: this.userId}});
  }
  dropdownVisible: boolean = false;
  toggleDropdown(state: boolean) {
    this.dropdownVisible = state;
  }
}
