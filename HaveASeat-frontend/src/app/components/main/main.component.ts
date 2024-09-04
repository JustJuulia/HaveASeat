import { Component } from '@angular/core';
import { MapaComponent } from '../mapa/mapa.component';
import { HeaderComponent } from '../header/header.component';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [HeaderComponent, MapaComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {
  title = 'HaveASeat';
  selectedDate: string = '';
  userId :number = 1;

  onDateChanged(date: string): void {
    this.selectedDate = date;
  }
}
