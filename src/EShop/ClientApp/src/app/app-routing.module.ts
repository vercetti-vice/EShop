// ====================================================


// ====================================================

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from "./components/login/login.component";
import { HomeComponent } from "./components/home/home.component";
import { CustomersComponent } from "./components/customers/customers.component";
import { ProductsComponent } from "./components/products/products.component";
import { OrdersComponent } from "./components/orders/orders.component";
import { SettingsComponent } from "./components/settings/settings.component";
import { AboutComponent } from "./components/about/about.component";
import { NotFoundComponent } from "./components/not-found/not-found.component";
import { AuthService } from './services/auth.service';
import { AuthGuard } from './services/auth-guard.service';
import {BrandManagmentComponent} from './components/brands/brand-managment/brand-managment.component';
import {BrandEditorComponent} from './components/brands/brand-editor/brand-editor.component';
import {BrandInfoComponent} from './components/brands/brand-info/brand-info.component';
import {CategoryManagmentComponent} from './components/categories/category-managment/category-managment.component';
import {CategoryInfoComponent} from './components/categories/category-info/category-info.component';
import {CategoryEditorComponent} from './components/categories/category-editor/category-editor.component';


const routes: Routes = [
  { path: "", component: HomeComponent, canActivate: [AuthGuard], data: { title: "Дом" } },
  { path: "login", component: LoginComponent, data: { title: "Вход" } },
  { path: "customers", component: CustomersComponent, canActivate: [AuthGuard], data: { title: "Customers" } },
  { path: "products", component: ProductsComponent, canActivate: [AuthGuard], data: { title: "Products" } },
  { path: "orders", component: OrdersComponent, canActivate: [AuthGuard], data: { title: "Заказы" } },
  { path: "settings", component: SettingsComponent, canActivate: [AuthGuard], data: { title: "Настройки" } },
  { path: "about", component: AboutComponent, data: { title: "О нас" } },
  { path: "home", redirectTo: "/", pathMatch: "full" },
  // { path: "**", component: NotFoundComponent, data: { title: "Страница не найдена" } },
  { path: 'brand-list', component: BrandManagmentComponent, data: { title: "Бренды" } },
  { path: 'brand-info', component: BrandInfoComponent, data: { title: "Бренд" } },
  { path: 'brand-edit/:id', component: BrandEditorComponent, data: { title: "Изменение бренда" } },
  { path: 'category-list', component: CategoryManagmentComponent, data: { title: "Категории" } },
  { path: 'category-info', component: CategoryInfoComponent, data: { title: "Категории" } },
  { path: 'category-edit/:id', component: CategoryEditorComponent, data: { title: "Изменение категории" } },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthService, AuthGuard]
})
export class AppRoutingModule { }
