import { Component, EventEmitter, Input, Output } from '@angular/core';
import { OnInit } from '@angular/core';

@Component({
  standalone:true,
  selector: 'app-dialog',
  templateUrl: './dialogue-component.component.html',
  styleUrls: ['./dialogue-component.component.css']
})
export class DialogComponent{
    @Input() title: string = '';
    private resolve!: (value: boolean) => void;
  
    openDialog(message: string): Promise<boolean> {
      this.title = message;
      return new Promise((resolve) => {
        this.resolve = resolve;
        const dialogElement = document.querySelector('dialog');
        if (dialogElement) {
          (dialogElement as HTMLDialogElement).showModal();
        }
      });
    }
  
    onConfirm() {
      this.closeDialog(true);
    }
  
    onClose() {
      this.closeDialog(false);
    }
  
    private closeDialog(result: boolean) {
      const dialogElement = document.querySelector('dialog');
      if (dialogElement) {
        (dialogElement as HTMLDialogElement).close();
      }
      this.resolve(result);
    }
  }