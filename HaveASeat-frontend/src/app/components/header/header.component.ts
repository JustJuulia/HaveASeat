import { Component, EventEmitter, Input, OnChanges, Output, AfterViewInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { HttpClientModule } from '@angular/common/http';
import { User } from '../../models/models';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [HttpClientModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'], // corrected 'styleUrl' to 'styleUrls'
  providers: [UserService]
})
export class HeaderComponent implements OnChanges, AfterViewInit {

  @Output() dateChanged: EventEmitter<string> = new EventEmitter<string>();
  @Input() userId!: number;

  today: string;
  username: string = "";
  user: User = <User>{};

  constructor(private userService: UserService) {
    this.today = new Date().toISOString().slice(0, 10);
  }

  ngOnChanges() {
    this.userService.getUserById(this.userId).subscribe({
      next: userData => {
        this.user = userData;
      },
      complete: () => {
        this.username = this.user.email.split('@')[0];
      }
    });
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
}
