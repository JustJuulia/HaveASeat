
import { Component, Inject } from '@angular/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OnInit } from '@angular/core';
@Component({
  selector: 'app-my-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './mydialog.component.html',
  styleUrls: ['./mydialog.component.scss']
})
export class MyDialogComponent {
    constructor(@Inject(MAT_DIALOG_DATA) public data: { mycontent: string }) {}
}



