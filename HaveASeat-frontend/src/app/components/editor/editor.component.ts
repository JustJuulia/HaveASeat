import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../models/models';
import { Route } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { MapaComponent } from '../mapa/mapa.component';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '../header/header.component';
import { EditorMapComponent} from  '../editor-map/editor-map.component';
import { HttpClientModule } from '@angular/common/http';
import { RightsideComponent } from '../rightside/rightside.component';
@Component({
  selector: 'app-editor',
  standalone: true,
  imports: [HeaderComponent, MapaComponent, CommonModule,HeaderComponent, EditorMapComponent, RightsideComponent],
  templateUrl: './editor.component.html',
  styleUrl: './editor.component.scss'
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
    });
  }
  onDateChanged(date: string): void {
    this.selectedDate = date;
  }
}
