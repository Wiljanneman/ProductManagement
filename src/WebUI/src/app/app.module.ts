import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthService } from './modules/auth/auth.service';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BaseUrlInterceptor } from './interceptors/baseUrl.interceptor';
import { HeaderInterceptor } from './interceptors/header.interceptor';
import { ErrorHandlingService } from './shared/services/errorhandlingservice';
import { ProgressBarService } from './shared/services/progress-bar.service';
import { ProgressBarComponent } from './shared/components/progress-bar/progress-bar.component';

@NgModule({
  declarations: [AppComponent, ProgressBarComponent],
  imports: [BrowserModule, AppRoutingModule, SharedModule, BrowserAnimationsModule,HttpClientModule],
  providers: [        
    AuthService,
    ProgressBarService,
    ErrorHandlingService,
    { provide: MAT_DATE_LOCALE, useValue: 'en-ZA' },
    { provide: HTTP_INTERCEPTORS, useClass: BaseUrlInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: HeaderInterceptor, multi: true }],
  bootstrap: [AppComponent],
})
export class AppModule {}
