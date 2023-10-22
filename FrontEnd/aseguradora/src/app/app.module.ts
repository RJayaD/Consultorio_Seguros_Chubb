import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {  HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AseguradosListaComponent } from './component/asegurados-lista/asegurados-lista.component';
import { SegurosListaComponent } from './component/seguros-lista/seguros-lista.component';
import { HomeComponent } from './component/home/home.component';
import { SeguroComponent } from './component/seguro/seguro.component';
import { MasivoAseguradoComponent } from './component/masivo-asegurado/masivo-asegurado.component';
import { TxtService } from './services/txt.services.service';
import { MasivoseguroComponent } from './component/masivoseguro/masivoseguro.component';
import { AseguradosComponent } from './component/asegurados/asegurados.component';
import { ServicioComponent } from './component/servicio/servicio.component';



@NgModule({
  declarations: [
    AppComponent,
    AseguradosListaComponent,
    SegurosListaComponent,
    SeguroComponent,
    AseguradosComponent,
    HomeComponent,
    MasivoAseguradoComponent,
    MasivoseguroComponent,
    ServicioComponent
    ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [TxtService],
  bootstrap: [AppComponent]
})
export class AppModule { }
