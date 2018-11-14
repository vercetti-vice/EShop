import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { AuthGuard } from './_guards';
import {MainComponent} from "./main/main.component";
import {BrandListComponent} from "./brand/brand-list/brand-list.component";
import {BrandCreateComponent} from "./brand/brand-create/brand-create.component";
import {BrandEditComponent} from "./brand/brand-edit/brand-edit.component";
import {CategoryListComponent} from "./category/category-list/category-list.component";
import {CategoryCreateComponent} from "./category/category-create/category-create.component";
import {CategoryEditComponent} from "./category/category-edit/category-edit.component";

const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'main', component: MainComponent },
    { path: 'brand-list', component: BrandListComponent},
    { path: 'brand-create', component: BrandCreateComponent},
    { path: 'brand-edit/:id', component: BrandEditComponent},
    { path: 'category-list', component: CategoryListComponent},
    { path: 'category-create', component: CategoryCreateComponent},
    { path: 'category-edit/:id', component: CategoryEditComponent},

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);
