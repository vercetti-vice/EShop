import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { AuthGuard } from './_guards';
import {MainComponent} from "./main/main.component";
import {BrandListComponent} from "./brand/brand-list/brand-list.component";
import {BrandCreateComponent} from "./brand/brand-create/brand-create.component";
import {BrandEditComponent} from "./brand/brand-edit/brand-edit.component";

const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'main', component: MainComponent },
    { path: 'brand-list', component: BrandListComponent},
    { path: 'brand-create', component: BrandCreateComponent},
    { path: 'brand-edit/:id', component: BrandEditComponent},

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);
