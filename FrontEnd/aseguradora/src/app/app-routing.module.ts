import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AseguradosComponent } from './component/asegurados/asegurados.component';
import { SeguroComponent } from './component/seguro/seguro.component';
import { AseguradosListaComponent } from './component/asegurados-lista/asegurados-lista.component';
import { SegurosListaComponent } from './component/seguros-lista/seguros-lista.component';
import { HomeComponent } from './component/home/home.component';
import { MasivoAseguradoComponent } from './component/masivo-asegurado/masivo-asegurado.component';
import { MasivoseguroComponent } from './component/masivoseguro/masivoseguro.component';
import { ServicioComponent } from './component/servicio/servicio.component';


const routes: Routes = [
  {path:'',component:HomeComponent},
  { path: 'Asegurado', component: AseguradosComponent },
  { path: 'Asegurado/:id', component: AseguradosComponent },
  { path: 'listaSeguro', component: SegurosListaComponent },
  {path:'listaAsegurado', component: AseguradosListaComponent},
  {path:'Seguro', component: SeguroComponent},
  {path:'Seguro/:id', component: SeguroComponent},
  {path:'masivoAsegurado', component: MasivoAseguradoComponent},
  {path:'masivoSeguro', component: MasivoseguroComponent},
  {path:'servicio', component: ServicioComponent}


];


@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports:[RouterModule]
})
export class AppRoutingModule { }
