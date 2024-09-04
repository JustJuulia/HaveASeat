import { Component } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { UserService } from '../../services/user.service';
import { User } from '../../models/models';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [HttpClientModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [UserService]
})
export class LoginComponent {

  private checkuserUrl = "https://localhost:7023/api/Authentication/GetByEmail/";
  private AddUser = "https://localhost:7023/api/Authentication/register";

  constructor(private http: HttpClient) {}

  SignUp(email: string, password: string): void {
    const regex_email = /^[\w-\.]+@([\w-]+\.)+[a-zA-Z]{2,}$/;
    const regex_passw = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/;
    if (email.length === 0 || password.length === 0) {
      alert('Pola nie zostały wypełnione');
      return;
    }
    if (!regex_email.test(email)) {
      alert('Mail nie został poprawnie wpisany!');
      return;
    }
    if (!regex_passw.test(password)) {
      alert('Hasło musi wynosić min. 8 znaków, 1 dużą literę alfabetyczną oraz liczbę');
      return;
    }
    this.http.get<User>(`${this.checkuserUrl}${email}`).subscribe({
      next: (user) => {
        if (user) {
          alert('Ten email jest już zajęty.');
        }
      },
      error: (err) => {
        if (err.status === 404){

        let userData = { email: email, password: password };
        this.http.post(this.AddUser, userData).subscribe({
          next: (response) => {
            alert('Rejestracja zakończona sukcesem!');
          },
          error: (registerErr) => {
            console.error('Błąd podczas rejestracji użytkownika:', registerErr);
            alert('Wystąpił błąd podczas rejestracji. Spróbuj ponownie później.');
          }
        });
      } else {
          console.error('Błąd sprawdzania dostępności użytkownika:', err);
        }
      }
    });

  }
}
