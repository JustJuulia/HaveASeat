import { Component } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { User } from '../../models/models';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [HttpClientModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [UserService],
})
export class LoginComponent {
  private checkuserUrl = 'https://localhost:7023/api/Authentication/GetByEmail/';
  private addUserUrl = 'https://localhost:7023/api/Authentication/register';
  private loginUserUrl = 'https://localhost:7023/api/Authentication/Login';
  userResponse: User | null = null;

  constructor(private http: HttpClient, private router: Router, private snackBar: MatSnackBar) {}

  LoginSucces(email: string) {
    this.http.get<User>(`${this.checkuserUrl}${email}`).subscribe({
      next: (user) => {
        console.log('API Response:', user);
        if (user) {
          const userId = user.id;
          this.router.navigate(['main'], { queryParams: { userId } });
        }
      },
      error: (err) => {
        console.error('Error during login:', err);
        this.popup(1, "Błąd w logowaniu, proszę spróbować później");
      },
    });
  }

  popup(type: number, text: string) {
    if(type == 0) {
      this.snackBar.open(text, "Zamknij", {
        duration: 3500,
        panelClass: ['success'],
        verticalPosition: 'top',
      });
    }
    if(type == 1) {
      this.snackBar.open(text, "Zamknij", {
        duration: 3500,
        panelClass: ['failure'],
        verticalPosition: 'top',
      });
    }
  }

  Check_form(email: string, password: string): boolean {
    const regex_email = /^[\w-.]+@([\w-]+\.)+[a-zA-Z]{2,}$/;
    const regex_passw = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$/;

    if (email.length === 0 || password.length === 0) {
      this.popup(1, "Pola nie są wypełnione")
      return false;
    }
    if (!regex_email.test(email)) {
      this.popup(1, "Nieprawidłowy email")
      return false;
    }
    if (!regex_passw.test(password)) {
      this.popup(1, "Hasło musi zawierać 8 znaków, w tym wielką literę i cyfrę")
      return false;
    }
    return true;
  }

  SignUp(email: string, password: string, name: string, surname: string): void {
    if (this.Check_form(email, password)) {
      this.http.get<User>(`${this.checkuserUrl}${email}`).subscribe({
        next: (user) => {
          if (user) {
            this.popup(1, "Ten email jest już zajęty")
          }
        },
        error: (err) => {
          if (err.status === 404) {
            const userData = { email: email, password: password, name: name, surname: surname };
            this.http.post(this.addUserUrl, userData).subscribe({
              next: (response) => {
                this.popup(0, "Rejestracja się powiodła")
                this.LoginSucces(email);
              },
              error: (registerErr) => {
                console.error('Error registering user:', registerErr);
                this.popup(1, "Błąd w rejestracji, proszę spróbować później")
              },
            });
          } else {
            console.error('Error checking user availability:', err);
            this.popup(1, "Wystąpił błąd w sprawdzaniu dostępności emaila")
          }
        },
      });
    }
  }

  SignIn(email: string, password: string): void {
    if (this.Check_form(email, password)) {
      this.http.get<User>(`${this.checkuserUrl}${email}`).subscribe({
        next: (user) => {
          if (user) {
            const userData = { email: email, password: password };
            this.http.post(this.loginUserUrl, userData).subscribe({
              next: (check_for_success) => {
                if (check_for_success) {
                  this.popup(0, "Logowanie się powiodło");
                  this.LoginSucces(email);
                } else {
                  this.popup(1, "Logowanie się nie powiodło")
                }
              },
              error: (loginErr) => {
                console.error('Error during login:', loginErr);
                this.popup(1, "Wystąpił błąd w logowaniu")
              },
            });
          } else {
            this.popup(1, "Nie ma konta powiązanego z tym emailem")
          }
        },
        error: (err) => {
          if (err.status === 404) {
            this.popup(1, "Nie ma konta powiązanego z tym emailem")
          } else {
            console.error('Error checking user availability:', err);
            this.popup(1, "Wystąpił błąd w sprawdzaniu dostępności użytkownika");
          }
        },
      });
    }
  }
}
