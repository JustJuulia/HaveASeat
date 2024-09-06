import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../models/models';
import { HeaderComponent } from '../header/header.component';
import { Route } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { MapaComponent } from '../mapa/mapa.component';
import { CommonModule } from '@angular/common';
import { EditorHeaderComponent } from '../editor-header/editor-header.component';
import { EditorMapComponent} from  '../editor-map/editor-map.component';
@Component({
  selector: 'app-editor',
  standalone: true,
  imports: [HeaderComponent, MapaComponent, CommonModule,EditorHeaderComponent, EditorMapComponent],
  templateUrl: './editor.component.html',
  styleUrl: './editor.component.css'
})
export class EditorComponent {
  title = 'HaveASeat';
  selectedDate: string = '';
  userId: number | null = null;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const userIdParam = params['userId'];
      this.userId = userIdParam ? +userIdParam : null;
      console.log('Received User ID:', this.userId);
    });
  }
  onDateChanged(date: string): void {
    this.selectedDate = date;
  }
}
