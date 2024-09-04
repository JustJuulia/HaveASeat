import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import { UserService } from '../../services/user.service';
import { HttpClientModule } from '@angular/common/http';
import { User } from '../../models/models';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [HttpClientModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
  providers: [UserService]
})
export class HeaderComponent implements OnChanges{

  @Output() dateChanged: EventEmitter<string> = new EventEmitter<string>();
  @Input() userId!: number;

  today: string;
  username :string = "";
  user :User = <User>{};

  constructor(private userService : UserService) {
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


  onDateChange(event: any): void {
    const selectedDate = event.target.value;
    const day = new Date(selectedDate).getDay(); 
    if (day === 6 || day === 0) {
      alert('W weekendy firma jest zamknieta');
      event.target.value = this.today;
      this.dateChanged.emit(this.today);
  }
  else if (day != 6 && day != 0){
    this.dateChanged.emit(selectedDate);
  }
}
}