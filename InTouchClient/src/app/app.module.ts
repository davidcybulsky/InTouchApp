import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ENVIRONMENT_TOKEN } from './core/tokens/environment.token';
import { EnvironmentDev } from 'src/environment/environment.dev';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: ENVIRONMENT_TOKEN, 
      useClass: EnvironmentDev
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
