import { Component } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { User } from '../../models/models';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [HttpClientModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [UserService],
})
export class LoginComponent {
  private checkuserUrl = 'https://localhost:7023/api/Authentication/GetByEmail/';
  private addUserUrl = 'https://localhost:7023/api/Authentication/register';
  private loginUserUrl = 'https://localhost:7023/api/Authentication/Login';
  userResponse: User | null = null;

  constructor(private http: HttpClient, private router: Router) {}

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
        alert('There was an error during login. Please try again later.');
      },
    });
  }

  Check_form(email: string, password: string): boolean {
    const regex_email = /^[\w-.]+@([\w-]+\.)+[a-zA-Z]{2,}$/;
    const regex_passw = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$/;

    if (email.length === 0 || password.length === 0) {
      alert('Fields are not filled in');
      return false;
    }
    if (!regex_email.test(email)) {
      alert('The email was not entered correctly!');
      return false;
    }
    if (!regex_passw.test(password)) {
      alert(
        'The password must be at least 8 characters long, contain 1 capital letter, and a number'
      );
      return false;
    }
    return true;
  }

  SignUp(email: string, password: string): void {
    if (this.Check_form(email, password)) {
      this.http.get<User>(`${this.checkuserUrl}${email}`).subscribe({
        next: (user) => {
          if (user) {
            alert('This email is already taken.');
          }
        },
        error: (err) => {
          if (err.status === 404) {
            const userData = { email: email, password: password };
            this.http.post(this.addUserUrl, userData).subscribe({
              next: (response) => {
                alert('Registration was successful!');
                setTimeout(() => this.LoginSucces(email), 1000);
              },
              error: (registerErr) => {
                console.error('Error registering user:', registerErr);
                alert('There was an error during registration. Please try again later.');
              },
            });
          } else {
            console.error('Error checking user availability:', err);
            alert('An error occurred while checking email availability. Please try again later.');
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
                  alert('Login successful!');
                  this.LoginSucces(email);
                } else {
                  alert('Login failed. Please check your credentials.');
                }
              },
              error: (loginErr) => {
                console.error('Error during login:', loginErr);
                alert('There was an error during login. Please try again later.');
              },
            });
          } else {
            alert('The email does not exist in the database.');
          }
        },
        error: (err) => {
          if (err.status === 404) {
            alert('This email has not been registered yet.');
          } else {
            console.error('Error checking user availability:', err);
            alert('There was an error during the process. Please try again later.');
          }
        },
      });
    }
  }
}
