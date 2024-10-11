import { isDevMode, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { metaReducers, ROOT_REDUCERS } from './store';
import { effects } from './store/effects';

import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    AdminLayoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    StoreModule.forRoot(ROOT_REDUCERS, { metaReducers }),
    EffectsModule.forRoot(effects),
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: !isDevMode() }),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
