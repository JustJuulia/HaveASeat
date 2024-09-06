import { Component, EventEmitter, Input, OnChanges, Output, AfterViewInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { HttpClientModule } from '@angular/common/http';
import { User } from '../../models/models';
import { NgIf } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [HttpClientModule, NgIf],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'], 
  providers: [UserService]
})
export class HeaderComponent implements OnChanges, AfterViewInit {

  @Input() userId: number | null = null;
  @Output() dateChanged = new EventEmitter<string>();


  today: string;
  username: string = "";
  usersurname: string = "Jan Wieprzowina";
  user: User = <User>{};

  constructor(private userService: UserService, private router: Router) {
    this.today = new Date().toISOString().slice(0, 10);
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
  
  setCalendarRestrictions(): void {
    const today = new Date();
    const todayStr = today.toISOString().split('T')[0];
    const nextMonth = new Date(today);
    nextMonth.setMonth(nextMonth.getMonth() + 1);
    const nextMonthStr = nextMonth.toISOString().split('T')[0];
    const datePicker = document.getElementById('datecalendar') as HTMLInputElement;
    if (datePicker) {
      datePicker.setAttribute('min', todayStr);
      datePicker.setAttribute('max', nextMonthStr);
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
    const selectedDate = event.target.value;
    const day = new Date(selectedDate).getDay();
    if (day === 6 || day === 0) {
      alert('W weekendy firma jest zamknieta');
      event.target.value = this.today;
      this.dateChanged.emit(this.today);
    }
    
    else if (day != 6 && day != 0) {
      this.dateChanged.emit(selectedDate);
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
      return false;
    }
  }
  GoToEdition() {
    if (this.userId !== null) {
      this.router.navigate(['editor'], { queryParams: { userId: this.userId } });
    } else {
      console.warn('User ID is null. Cannot navigate to editor.');
      alert('User ID is missing. Unable to proceed to the editor.');
    }
  }
  
  dropdownVisible: boolean = false;
  toggleDropdown(state: boolean) {
    this.dropdownVisible = state;
  }
}
