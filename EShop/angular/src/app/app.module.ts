﻿import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule }    from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

// used to create fake backend
//import { fakeBackendProvider } from './_helpers';

import { AppComponent }  from './app.component';
import { routing }        from './app.routing';

import { AlertComponent } from './_directives';
import { AuthGuard } from './_guards';
import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import {AlertService, AuthenticationService, BrandService, UserService} from './_services';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { RegisterComponent } from './register';
import { MainComponent } from './main/main.component';;
import { BrandListComponent } from './brand/brand-list/brand-list.component';
import { BrandCreateComponent } from './brand/brand-create/brand-create.component';
import { BrandEditComponent } from './brand/brand-edit/brand-edit.component';
import { ColorListComponent } from './color/color-list/color-list.component';
import { ColorCreateComponent } from './color/color-create/color-create.component';
import { ColorEditComponent } from './color/color-edit/color-edit.component';;
import { CategoryCreateComponent } from './category/category-create/category-create.component';
import { CategoryListComponent } from './category/category-list/category-list.component';
import { CategoryEditComponent } from './category/category-edit/category-edit.component';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        routing
    ],
    declarations: [
        AppComponent,
        AlertComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent,
        MainComponent,
        BrandListComponent,
        BrandCreateComponent,
        BrandEditComponent ,
        ColorListComponent ,
        ColorCreateComponent ,
        ColorEditComponent ,
        CategoryCreateComponent ,
        CategoryListComponent ,
        CategoryEditComponent],
    providers: [
        AuthGuard,
        AlertService,
        AuthenticationService,
        BrandService,
        UserService,
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

        // provider used to create fake backend
        //fakeBackendProvider
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }
