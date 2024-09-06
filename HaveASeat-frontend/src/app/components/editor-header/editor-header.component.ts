import { Component, EventEmitter, Input, OnChanges, Output, AfterViewInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { HttpClientModule } from '@angular/common/http';
import { User } from '../../models/models';
import { NgIf } from '@angular/common';
import { Router } from '@angular/router';
import { EditorComponent } from '../editor/editor.component';

@Component({
  selector: 'app-editor-header',
  standalone: true,
  imports: [HttpClientModule, NgIf],
  templateUrl: './editor-header.component.html',
  styleUrls: ['./editor-header.component.css'], 
  providers: [UserService]
})
export class EditorHeaderComponent implements OnChanges, AfterViewInit {

  @Input() userId: number | null = null;
  @Output() dateChanged = new EventEmitter<string>();


  today: string;
  username: string = "";
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
          this.username = this.user.email.split('@')[0];
        },
        error: err => {
          console.error('Failed to fetch user data:', err);
        }
      });
    } else {
      console.warn('userId is null');
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
  dropdownVisible: boolean = false;
  toggleDropdown(state: boolean) {
    this.dropdownVisible = state;
  }
}
