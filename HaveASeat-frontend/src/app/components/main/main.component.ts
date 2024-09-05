import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../models/models';
import { HeaderComponent } from '../header/header.component';
import { Route } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { MapaComponent } from '../mapa/mapa.component';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [HeaderComponent, MapaComponent],
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  title = 'HaveASeat';
  selectedDate: string = '';
  userId: number | null = null;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.userId = +params['userId'];
      console.log('Received User ID:', this.userId);
    });
  }

  onDateChanged(date: string): void {
    this.selectedDate = date;
  }
}
