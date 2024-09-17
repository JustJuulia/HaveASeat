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
export class HeaderComponent {

  @Input() userId: number | null = null;
  @Input() pageSwitch: number = 0;
  @Output() dateChanged = new EventEmitter<string>();
  @Output() forbiddenDate = new EventEmitter<string>();


  today: string;
  username: string = "";
  usersurname: string = "";
  user: User = <User>{};
  alldates: Date[] = [];

  constructor(private userService: UserService, private router: Router, private http: HttpClient, private snackBar: MatSnackBar) {
    this.today = new Date().toISOString().slice(0, 10);
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
  
  ngOnChanges() {
    if (this.userId !== null) {
      this.userService.getUserById(this.userId).subscribe({
        next: userData => {
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
  toggleDropdown() {
    this.dropdownVisible = !this.dropdownVisible;
  }
}
