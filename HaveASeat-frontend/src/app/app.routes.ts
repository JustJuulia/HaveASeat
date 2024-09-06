import { Routes } from '@angular/router';
import { MainComponent } from './components/main/main.component';
import { LoginComponent } from './components/login/login.component';
import { EditorComponent } from './components/editor/editor.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';

export const routes: Routes = [
    {path: 'main', component: MainComponent},
    {path: 'login', component: LoginComponent},
    {path: '', component: LoginComponent},
    {path: 'editor', component: EditorComponent},
    {path: 'admin-panel', component: AdminPanelComponent},
];
