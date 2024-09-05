import { Routes } from '@angular/router';
import { MainComponent } from './components/main/main.component';
import { LoginComponent } from './components/login/login.component';


export const routes: Routes = [
    {path: 'main', component: MainComponent},
    {path: 'login', component: LoginComponent},
    {path: '', component: LoginComponent}
];
