import { Component, Output } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { UserService } from './services/user.service';
import { AppService } from './services/app.service';
import { MainComponent } from './components/main/main.component';
import { LoginComponent } from "./components/login/login.component";
import { FormsModule } from '@angular/forms';
import { AdminDatesComponent } from './components/admin-dates/admin-dates.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HttpClientModule, MainComponent, RouterLink, RouterLinkActive, LoginComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  
})
export class AppComponent {
  
}
