import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../models/models';
import { HeaderComponent } from '../header/header.component';
import { Route } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { MapaComponent } from '../mapa/mapa.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminDatesComponent } from '../admin-dates/admin-dates.component';
import { UserListComponent } from '../user-list/user-list.component';
import { HttpClientModule } from '@angular/common/http';
import { RightsideComponent } from '../rightside/rightside.component';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [HeaderComponent, MapaComponent, CommonModule, UserListComponent, RightsideComponent],
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  title = 'HaveASeat';
  selectedDate: string = '';
  forbiddenDate: string = "";
  userId: number | null = null;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const userIdParam = params['userId'];
      this.userId = userIdParam ? +userIdParam : null;
    });
  }
  onDateChanged(date: string): void {
    this.selectedDate = date;
  }
  onForbiddenDate(date: string): void{
    this.forbiddenDate = date
  }
}
